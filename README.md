# Here's what I've been learning so far

1. Setup dotnet web api project
2. Create data model that will transform into table's structure
3. Create repository for each model as data access mechanism
4. Create service to handle business logic
5. Parse request body from using `[FromBody]` attribute
6. Parse query params using `[FromQuery]`
7. Create table relationship in `OnModelCreating` using `modelBuilder`
8. Connect application to sql server database using `Entity Framework Sql Server`
9. Create migrations using CLI command `dotnet ef migrations add [migration_name]` and `dotnet ef database update`
10. Implement `Dependency Injections`, it says that 'a module/class should depend on abstractions instead of concrete implementations'
11. Using `AutoMapper` for mapping source data with `Data Transform Object` (DTO)
12. Create custom `middleware` that will intercepts request and do stuff with it

# Here's what's coming next

1. Implement clean design using `S.O.L.I.D` principles
2. Implement authentication and authorization
3. [I don't know, we'll see]
