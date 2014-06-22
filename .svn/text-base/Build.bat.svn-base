@echo off
call update_version_prefix.vbs
echo Building LumaQQ.NET.sln, please wait a minute...
"C:\WINDOWS\Microsoft.NET\Framework\v3.5\MsBuild.exe" LumaQQ.NET.sln /t:Rebuild /p:Configuration=Release > MsBuild.log
call UpdateAssemblies.bat
call ClearBuildingObjects.bat