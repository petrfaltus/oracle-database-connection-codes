@echo off

rem https://hub.docker.com/_/oracle-database-enterprise-edition

docker run -d -p 1521:1521 --name oracle-db store/oracle/database-enterprise:12.2.0.1
echo.

docker container ls
echo.

pause
