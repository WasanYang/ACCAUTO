; ======================================================
; Inno Setup Script สำหรับ ACCOUNTANROBOT
; วิธีใช้:
;   1. Build โปรเจคใน Release mode ก่อน (Build → Configuration: Release)
;   2. เปิดไฟล์นี้ด้วย Inno Setup Compiler แล้วกด Compile (Ctrl+F9)
;   3. ได้ไฟล์ installer\ACCautokey_V5_Setup.exe
; ======================================================

#define AppName "ACCOUNTANROBOT"
#define AppExe "AccAutoKey.exe"
#define SrcDir "Project\AccAutoKey\bin\Release"
#define CfgDir "Project\AccAutoKey\config"
#define AppVersion GetFileVersion(SrcDir + "\" + AppExe)

[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
OutputDir=installer
OutputBaseFilename=ACCautokey_V{#AppVersion}_Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
SetupIconFile=Project\AccAutoKey\icon.ico

[Tasks]
Name: "desktopicon"; Description: "สร้าง Shortcut บน Desktop"; Flags: unchecked

[Files]
Source: "{#SrcDir}\{#AppExe}";              DestDir: "{app}"; Flags: ignoreversion
Source: "{#SrcDir}\AccAutoKey.exe.config";  DestDir: "{app}"; Flags: ignoreversion
Source: "{#SrcDir}\*.dll";                  DestDir: "{app}"; Flags: ignoreversion
Source: "{#CfgDir}\*";                      DestDir: "{app}\config"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\{#AppName}";                    Filename: "{app}\{#AppExe}"
Name: "{group}\ถอนการติดตั้ง {#AppName}";     Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppName}";              Filename: "{app}\{#AppExe}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExe}"; Description: "เปิดโปรแกรม {#AppName}"; Flags: nowait postinstall skipifsilent
