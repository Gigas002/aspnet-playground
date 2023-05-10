dotnet add package Microsoft.EntityFrameworkCore.Sqlite --prerelease
dotent add package Mictosoft.EntityFrameworkCore.Design --prerelease
# dotnet tool install --global dotnet-ef --prerelease
# dotnet tool update --global dotnet-ef --prerelease
dotnet ef migrations add InitialMigration
# dotnet ef migrations script > script.sql
# dotnet ef migrations bundle
dotnet ef database update
