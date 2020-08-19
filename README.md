# Oracle database connection codes
Small example console source codes how to connect to the Oracle database, how to update rows and how to read the table.

## Running under Windows
1. clone this repository to your computer
2. install the **Oracle database** (as a **Docker** container)
3. create the user, the table and insert rows in the database
4. build and run the example **Java** code
5. compile and run the example **.NET C#** code
6. run the example **PHP** code

### 1. Cloning to your computer
- install [GIT] on your computer
- clone this repository to your computer by the GIT command
  `git clone https://github.com/petrfaltus/oracle-database-connection-codes.git`

### 2. Install the Oracle database (as a Docker container)
- install [docker desktop] on your computer
- refer the [Oracle Database Enterprise Edition image]

The subdirectory `docker-database` contains prepared Windows batches:
- `01-run-database.cmd` - pull the image (the download size is about 2GB) and run the container **at the first time** (takes a time until ready)
- `02-switch-database-OFF.cmd` - stop already existing container
- `02-switch-database-ON.cmd` - start already existing container (takes a time until ready)
- `03-inspect-database.cmd` - show details for already existing container
- `04-exec-connection-to-database.cmd` - execute the **SQL Plus** terminal into running database container
- `containers.cmd` - list of currently running containers and list of all existing containers

## Versions
Now in August 2020 I have the computer with **Windows 10 Pro 64bit**, **12GB RAM** and available **50GB free HDD space**

| Tool | Version | Setting |
| ------ | ------ | ------ |
| [GIT] | 2.26.0.windows.1 | |
| [docker desktop] | 2.3.0.4 (46911) stable | 2 CPUs, 3GB memory, 1GB swap, 48GB disc image size |
| [Oracle Database Enterprise Edition image] | 12.2.0.1 | default password for sys: Oradoc_db1 |

## To do (my plans to the future)


[GIT]: <https://git-scm.com>
[docker desktop]: <https://docs.docker.com/desktop/>
[Oracle Database Enterprise Edition image]: <https://hub.docker.com/_/oracle-database-enterprise-edition>
