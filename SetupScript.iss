; -- SetupScript.iss --
; Inno Setup script to create a Windows installer and uninstaller


[Setup]
AppName=Texjet Temporary Fix June 2023
AppVersion=1.0
WizardStyle=modern
DefaultDirName={autopf}\TexjetTempFix
DisableProgramGroupPage=yes
UninstallDisplayIcon={app}\TexjetTempFix.exe
Compression=lzma2
SolidCompression=yes
OutputDir=C:\users\liam\texjetfix\output
MinVersion=6.0

[Files]
Source: "c:\users\liam\texjetfix\TexjetTempFix.exe"; DestDir: "{app}"
Source: "c:\users\liam\texjetfix\TexjetTempFix.dll"; DestDir: "{app}"
Source: "c:\users\liam\texjetfix\TexjetTempFix.pdb"; DestDir: "{app}"
Source: "c:\users\liam\texjetfix\TexjetTempFix.runtimeconfig.json"; DestDir: "{app}"
Source: "c:\users\liam\texjetfix\TexjetTempFix.deps.json"; DestDir: "{app}"
Source: "c:\users\liam\texjetfix\P600\*.*"; DestDir: "{app}\P600"
Source: "c:\users\liam\texjetfix\P800\*.*"; DestDir: "{app}\P800"

[Icons]
Name: "{autoprograms}\Texjet Fix"; Filename: "{app}\TexjetTempFix.exe"
Name: "{autodesktop}\Texjet Fix"; Filename: "{app}\TexjetTempFix.exe"
