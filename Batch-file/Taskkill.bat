@echo off
tasklist /fi "ImageName eq ProcessChecker.exe" /fo csv 2>NUL | find /I "ProcessChecker.exe">NUL
if "%ERRORLEVEL%"=="0" ( 
   cd C:\ProcessChecker
   taskkill /f /im ProcessChecker.exe
  ) else (echo Process  is not running)
if exist "C:\PI-THUNDER" (
for /l %%x in (1,1,3) do (
cd C:\PI-THUNDER
taskkill /f /im ThunderHelper.exe /im ThunderServer.exe
tasklist /fi "ImageName eq ThunderHelper.exe" /fo csv 2>NUL | find /I "ThunderHelper.exe">NUL
if "%ERRORLEVEL 1%"=="0" ( 
   cd C:\PI-THUNDER
   taskkill /f /im ThunderHelper.exe /im ThunderServer.exe 
  ) else (echo Process  is not running)
)
cd ..
ren C:\PI-THUNDER Backuplogs
move C:\AUTO.DEPLOY\Extract\Ticket_18\PI-THUNDER C:\
CD C:\ProcessChecker 
start ProcessChecker.exe
) else ( echo Not have a folder.)
