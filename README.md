# Oracle database connection source codes
Small example console source codes how to connect to the Oracle database, how to update rows and how to read the table.

## Running under Windows
1. clone this repository to your computer
2. install the **Oracle database** (as a **Docker** container)
3. prepare the user, the table and rows in the database
4. build and run the example **Java** code
5. compile and run the example **.NET C#** code
6. run the example **PHP** code

### 1. Cloning to your computer
- install [GIT] on your computer
- clone this repository to your computer by the GIT command
  `git clone https://github.com/petrfaltus/oracle-database-connection-source-codes.git`

### 2. Installation of the Oracle database (as a Docker container)
- install [docker desktop] on your computer
- refer the [Oracle Database Enterprise Edition image]

The subdirectory `docker-database` contains prepared Windows batches:
- `01-run-database.cmd` - pull the image (the download size is about 2GB) and run the container **at the first time** (takes a time until ready)
- `02-switch-database-OFF.cmd` - stop already existing container
- `02-switch-database-ON.cmd` - start already existing container (takes a time until ready)
- `03-inspect-database.cmd` - show details for already existing container
- `04-exec-connection-to-database.cmd` - execute the **SQL Plus** terminal into running database container
- `containers.cmd` - list of currently running containers and list of all existing containers

### 3. Preparing the database
For the connection to the dabase use either the **SQL Plus** terminal or the [Oracle SQL Developer]

#### Connection using SQL Plus
User *sys* (default password *Oradoc_db1*)
```sql
CONNECT sys AS SYSDBA;
ALTER SESSION SET "_ORACLE_SCRIPT"=true;
```

User *testuser* (default password *T3stUs3r!*)
```sql
CONNECT testuser;
ALTER SESSION SET "_ORACLE_SCRIPT"=true;
```

#### Connection using SQL Developer
User *sys* (default password *Oradoc_db1*)
![user sys configuration](sql.developer.sys.png)

User *testuser* (default password *T3stUs3r!*)
![user testuser configuration](sql.developer.testuser.png)

#### SQL lines for sys
```sql
CREATE USER testuser IDENTIFIED BY "T3stUs3r!";
GRANT ALL PRIVILEGES TO testuser;
```

#### SQL lines for testuser
```sql
CREATE TABLE CARS
  (
   MANUFACTURER VARCHAR2(40 BYTE) NOT NULL,
   MODEL VARCHAR2(50 BYTE) NOT NULL,
   DOORS NUMBER(2,0) NOT NULL,
   CREATED TIMESTAMP(6) DEFAULT CURRENT_TIMESTAMP,
   UPDATED TIMESTAMP(6),
   REMARK VARCHAR2(80 BYTE),
   ID NUMBER(6,0) GENERATED ALWAYS AS IDENTITY START WITH 1 INCREMENT BY 1 NOT NULL,
   PRIMARY KEY (ID)
  );

CREATE OR REPLACE TRIGGER CARS_UPDATE
  BEFORE UPDATE ON CARS
  FOR EACH ROW
BEGIN
  :NEW.UPDATED := CURRENT_TIMESTAMP;
END;

INSERT INTO CARS (MANUFACTURER, MODEL, DOORS) VALUES ('Hyundai', 'Veloster', 3);
INSERT INTO CARS (MANUFACTURER, MODEL, DOORS) VALUES ('Skoda', 'Fabia', 5);
INSERT INTO CARS (MANUFACTURER, MODEL, DOORS) VALUES ('Volkswagen', 'Passat', 4);
INSERT INTO CARS (MANUFACTURER, MODEL, DOORS) VALUES ('Ford', 'Saloon', 4);
INSERT INTO CARS (MANUFACTURER, MODEL, DOORS) VALUES ('Ford', 'Focus', 5);
```

## Versions
Now in August 2020 I have the computer with **Windows 10 Pro 64bit**, **12GB RAM** and available **50GB free HDD space**

| Tool | Version | Setting |
| ------ | ------ | ------ |
| [GIT] | 2.26.0.windows.1 | |
| [docker desktop] | 2.3.0.4 (46911) stable | 2 CPUs, 3GB memory, 1GB swap, 48GB disc image size |
| [Oracle Database Enterprise Edition image] | 12.2.0.1 | default password for sys: Oradoc_db1 |
| [Oracle SQL Developer] | 20.2.0 | |

## To do (my plans to the future)


[GIT]: <https://git-scm.com>
[docker desktop]: <https://docs.docker.com/desktop/>
[Oracle Database Enterprise Edition image]: <https://hub.docker.com/_/oracle-database-enterprise-edition>
[Oracle SQL Developer]: <https://www.oracle.com/database/technologies/appdev/sqldeveloper-landing.html>
