# ACCOUNTANROBOT (AccAutoKey)

Launcher สำหรับโปรแกรมบัญชี Express — ให้ลูกค้าเปิดโปรแกรมบัญชีต่างๆ ได้จากหน้าจอเดียว โดยไฟล์โปรแกรมจริงเก็บอยู่ใน Google Drive หรือ folder ที่กำหนด

พัฒนาโดย **หจก. Cma การบัญชีและซอฟต์แวร์ เซ็นเตอร์**

---

## ความต้องการ

- Windows 10 ขึ้นไป
- .NET Framework 4.7.2
- Visual Studio 2019 (สำหรับ dev)
- Inno Setup (สำหรับสร้าง installer)

---

## วิธี Build

1. เปิด `Project/AutoImport.sln` ใน Visual Studio 2019
2. เลือก Configuration: **Release**
3. Build Solution → ไฟล์ output อยู่ที่ `Project/AccAutoKey/bin/Release/`

## วิธีสร้าง Installer

1. Build เป็น Release ก่อน
2. เปิด `setup.iss` ใน Inno Setup Compiler
3. กด Compile — ได้ไฟล์ `installer/ACCautokey_V{version}_Setup.exe`

---

## การตั้งค่าต่อลูกค้า

แก้ไข 2 ที่:

### 1. `App.config` — path โปรแกรมบัญชี

```xml
<add key="form_title"    value="ACCOUNTANROBOT V.6 To Express"/>
<add key="resource_path" value="G:\My Drive\pylnn"/>
```

`resource_path` ชี้ไปที่ folder ที่มีไฟล์ `.exe` โปรแกรมบัญชี รองรับ:
- Google Drive letter เช่น `G:\My Drive\folder`
- Path เต็ม เช่น `C:\Users\...\folder`
- Path สัมพัทธ์ เช่น `resources` (จะหาจาก folder เดียวกับ AccAutoKey.exe)

### 2. `config/about.xml` — ข้อมูลบริษัทลูกค้า

```xml
<ArrayOfAboutModel>
  <AboutModel>
    <Address>ชื่อบริษัทลูกค้า</Address>
    <StartDate>01/01/2026</StartDate>
    <EndDate>31/12/2026</EndDate>
    <Location>G:\My Drive\data</Location>
    <Record>500</Record>
    <SN>ACC-XXXX</SN>
  </AboutModel>
</ArrayOfAboutModel>
```

### 3. `config/page1.xml` ถึง `page5.xml` — รายการโปรแกรมในแต่ละ tab

```xml
<ArrayOfFileConfig>
  <FileConfig>
    <Id>1</Id>
    <Name>ชื่อที่แสดงบนปุ่ม</Name>
    <Show>true</Show>
    <FileName>HP.exe</FileName>
  </FileConfig>
</ArrayOfFileConfig>
```

ตั้ง `<Show>false</Show>` เพื่อซ่อนปุ่มที่ลูกค้าไม่ใช้

---

## โครงสร้างโฟลเดอร์หลัก

```
Project/AccAutoKey/
├── App.config          ← แก้ resource_path ตรงนี้
├── config/
│   ├── page1.xml       ← รายการปุ่ม tab R1
│   ├── page2.xml–page5.xml
│   ├── about.xml       ← ข้อมูลบริษัทลูกค้า
│   └── copyRight.xml   ← ข้อความลิขสิทธิ์
└── Properties/
    └── AssemblyInfo.cs ← เปลี่ยน version ตรงนี้
```

---

## Version

Version จัดการใน `Properties/AssemblyInfo.cs` ที่เดียว:

```csharp
[assembly: AssemblyVersion("6.0.0.0")]
[assembly: AssemblyFileVersion("6.0.0.0")]
```

Inno Setup อ่าน version จาก compiled exe อัตโนมัติ ไม่ต้องแก้ใน `setup.iss`
