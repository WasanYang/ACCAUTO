# CLAUDE.md — ACCAUTO Project Context

## ภาพรวม
**ACCOUNTANROBOT** (AccAutoKey) คือ Windows launcher app สำหรับระบบบัญชี Express สำหรับบริษัทลูกค้าของ หจก. Cma การบัญชีฯ
แอปนี้ไม่ได้ทำบัญชีเอง — มันแค่เปิดไฟล์ `.exe` ของโปรแกรมบัญชีแยกแต่ละตัว (HP.exe, RR.exe ฯลฯ) ที่อยู่ใน Google Drive หรือ folder ที่กำหนด

## Tech Stack
- **C# Windows Forms** — .NET Framework 4.7.2
- **Visual Studio 2019** (solution เปิดได้ถึง VS 2019 เท่านั้น — sln version = 16)
- **Inno Setup** — สร้าง single-file installer (`setup.iss`)
- **XML Serialization** — `XmlSerializer` อ่าน config ทุกไฟล์ (ไม่มี database)

## โครงสร้างโฟลเดอร์

```
ACCAUTO/
├── .gitignore                     # ignore bin/, obj/, .vs/, *.user, installer/
├── CLAUDE.md                      # ไฟล์นี้
├── README.md
├── setup.iss                      # Inno Setup script — สร้าง installer
└── Project/
    └── AccAutoKey/
        ├── AccAutoKey.csproj
        ├── App.config             # config หลัก (form_title, resource_path)
        ├── Form1.cs               # main form — โหลด JobPage/AboutPage ลงใน panel
        ├── config/                # XML config ต้นทาง (copy ไป bin/ ตอน build)
        │   ├── page1.xml–page5.xml   # รายการโปรแกรมในแต่ละ tab
        │   ├── about.xml             # ข้อมูลบริษัท/ลูกค้า
        │   └── copyRight.xml         # ข้อความลิขสิทธิ์
        ├── Models/
        │   ├── FileConfig.cs      # Id, Name, Show, FileName
        │   ├── AboutModel.cs      # Address, Description, StartDate, EndDate, Location, Record, SN
        │   └── CopyRightModel.cs  # Title
        ├── Page/
        │   ├── JobPage.cs         # UserControl หลัก — รับ pageNumber สร้างปุ่มจาก XML
        │   ├── AboutPage.cs       # แสดงข้อมูลบริษัท + resource_path
        │   ├── CopyRight.cs       # หน้าลิขสิทธิ์
        │   └── NewPage1-5.cs      # *** DEAD CODE *** ยังอยู่แต่ไม่ได้ใช้แล้ว
        ├── Share/
        │   └── JobService.cs      # logic เปิดโปรแกรม (GetResourcePath, startProcess)
        └── Properties/
            └── AssemblyInfo.cs    # version จริง — แก้ตรงนี้ที่เดียว (6.0.0.0)
```

## Architecture Flow

```
Form1 (main window)
  └── [tab click] → โหลด JobPage(pageNumber) ลงใน panelControl
        └── JobPage.LoadPage()
              └── อ่าน config/page{n}.xml → สร้างปุ่มชื่อโปรแกรม
                    └── [เริ่ม] → JobService.startProcess(FileConfig)
                          └── GetResourcePath() + Path.Combine + File.Exists()
                                └── Process.Start(path) → Application.Exit()
```

## Config System

### App.config (แก้ต่อการ deploy)
```xml
<add key="form_title"    value="ACCOUNTANROBOT V.6 To Express"/>
<add key="resource_path" value="C:\Users\wy\Desktop\work\pylnn"/>
```
- `resource_path` — path ไปยัง folder ที่มีไฟล์ exe โปรแกรมบัญชี
  - ใส่ absolute path (C:\...) หรือ relative path (relative จาก AccAutoKey.exe) หรือ Google Drive letter (G:\...)
  - ถ้าว่าง จะใช้ `resources/` ใน folder เดียวกับ exe

### config/page{n}.xml (แก้ต่อลูกค้า)
```xml
<ArrayOfFileConfig>
  <FileConfig>
    <Id>1</Id>
    <Name>ชื่อโปรแกรมที่แสดงบนปุ่ม</Name>
    <Show>true</Show>          <!-- false = ซ่อนปุ่มนี้ -->
    <FileName>HP.exe</FileName> <!-- ชื่อไฟล์ใน resource_path -->
  </FileConfig>
</ArrayOfFileConfig>
```

### config/about.xml
```xml
<ArrayOfAboutModel>
  <AboutModel>
    <Address>ชื่อบริษัทลูกค้า</Address>
    <Description>...</Description>
    <StartDate>วันเริ่มสัญญา</StartDate>
    <EndDate>วันสิ้นสุด MA</EndDate>
    <Location>ที่อยู่เก็บข้อมูล</Location>
    <Record>จำนวนรายการ</Record>
    <SN>รหัสโปรแกรม</SN>
  </AboutModel>
</ArrayOfAboutModel>
```

### config/copyRight.xml
```xml
<ArrayOfCopyRightModel>
  <CopyRightModel><Title>ข้อความแต่ละบรรทัด</Title></CopyRightModel>
</ArrayOfCopyRightModel>
```

## Build & Deploy

### Build
1. เปิด `Project/AutoImport.sln` ใน **Visual Studio 2019**
2. เลือก Configuration = **Release**
3. Build → output ที่ `Project/AccAutoKey/bin/Release/`

### สร้าง Installer
1. เปิด `setup.iss` ด้วย **Inno Setup Compiler**
2. กด Compile (Ctrl+F9)
3. ได้ไฟล์ `installer/ACCautokey_V{version}_Setup.exe`
   - version อ่านจาก `AssemblyInfo.cs` อัตโนมัติ

### ส่งให้ลูกค้า
- ส่ง installer `.exe` ไฟล์เดียว
- ลูกค้า double-click ติดตั้ง → ได้โปรแกรม + config XMLs ครบ
- หลังติดตั้งแก้ `App.config` ให้ `resource_path` ชี้ไปที่ Google Drive ของลูกค้า

## Design Decisions สำคัญ

| เรื่อง | การตัดสินใจ | เหตุผล |
|--------|------------|--------|
| JobPage รับ pageNumber | แทน NewPage1-5 ที่แยกไฟล์ | ลด duplicate code 5 ไฟล์เหลือ 1 |
| Application.Exit() หลัง Process.Start | แทนการ monitor process | ลูกค้าใช้ทีละโปรแกรม ป้องกันเปิดซ้ำ |
| File.Exists() ก่อน Process.Start | เช็คก่อนเปิด | Google Drive อาจ offline ได้ |
| Path.IsPathRooted() | ตรวจ absolute vs relative | รองรับทั้ง local path และ Google Drive letter |
| Inno Setup แทน .vdproj | single-file installer | .vdproj ต้องการ VS extension พิเศษ |
| version ใน AssemblyInfo.cs | single source of truth | Inno Setup อ่านจาก compiled exe อัตโนมัติ |

## Dead Code (ยังไม่ได้ลบ)
- `Page/NewPage1.cs` ถึง `Page/NewPage5.cs` — ถูกแทนโดย `JobPage.cs` แล้ว สามารถลบได้

## Namespace
ทุกไฟล์ใช้ `namespace AccAutoKey` (เดิมเป็น `AccAutoKey4` — เปลี่ยนแล้วทั้งหมด)
