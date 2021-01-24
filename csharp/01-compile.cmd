@echo off

set OUT=bin

set SOURCE=OracleDBclient
set EXECUTABLE=%OUT%\%SOURCE%.exe
set ICON=ico\database.ico
set DOTNET_HOME=C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319

set DLL=%OUT%\Oracle.ManagedDataAccess.dll

mkdir %OUT%
"%DOTNET_HOME%\csc.exe" /win32icon:%ICON% /out:%EXECUTABLE% /reference:%DLL% %SOURCE%.cs
