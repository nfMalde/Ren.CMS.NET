@echo off

for %%i in (*.sql) do sqlcmd -S %1 -d %2 -i %%i 

pause