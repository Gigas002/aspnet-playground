dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet ef migrations add InitMigration
dotnet ef database update
dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=test1;Username=gigas" Npgsql.EntityFrameworkCore.PostgreSQL
