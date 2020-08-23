@echo off

set SOURCE=OracleDBclient
set EXECUTABLE=bin\%SOURCE%.exe
set ICON=ico\database.ico
set DOTNET_64_HOME=C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319

"%DOTNET_64_HOME%\csc.exe" /win32icon:%ICON% /out:%EXECUTABLE% %SOURCE%.cs
