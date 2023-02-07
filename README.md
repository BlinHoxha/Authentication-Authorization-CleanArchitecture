# DDD.API
**Authentication and Authorization with Identity and using DDD 5-tier layered & Onion design pattern.**

**P.s New features expected to add soon.**
___

## Code-First Migration 

Set DDD.Infrastructure as startup project and open the project with terminal, then run the following command to create the migration:

```
dotnet ef migrations add MyMigration --startup-project "../DDD.API/DDD.api.csproj"
```

To update the database after you create migration, run the following command:

```
dotnet ef database update --startup-project "../DDD.API/DDD.api.csproj"
```

