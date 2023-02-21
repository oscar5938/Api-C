#BeatStarsApi EF MIGRATION C#
Compilación y prueba de la API web
dotnet run

Migración
##Instalar FW EntityFramework
dotnet add package Microsoft.EntityFrameworkCore

##Instalar Plugin SQL Server
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

##Herramientas
dotnet add package Microsoft.EntityFrameworkCore.Tools

##Cadena de conexión appsettings.json
"UsersContext":"Server=localhost,1435;Database=BeatStarApi;Pwd=pwdXXX;Uid=sa;TrustServerCertificate=True"

##Herramientas para entity framework
dotnet tool install --global dotnet-ef

##Añadir la migración:
dotnet ef migrations add migracion1

##Subir la BBDD:
//El servidor API-C debe estar parado.
dotnet ef database update

##Instance image SQL Server - Docker 
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Sanvalero77" -p 1435:1433 -d mcr.microsoft.com/mssql/server:2022-latest
-p 1435(puerto local):1433(caja negra docker, puerto sqlserver no cambiar)

###APIC DOCKERHUB
https://hub.docker.com/repository/docker/dariocuberozgz/api-c/general