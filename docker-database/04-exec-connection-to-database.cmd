@echo off

rem user: sys
rem default password: Oradoc_db1

rem CONNECT sys AS SYSDBA;
rem ALTER SESSION SET "_ORACLE_SCRIPT"=true;

docker exec -it oracle-db bash -c "source /home/oracle/.bashrc; sqlplus /nolog"
echo.

pause
