1. install .net sdk 7
2. restore package in EventManagement.App project.
3. apply migrations: go to EventManagement.Persistance project run follow commands.
create migrations
dotnet ef --startup-project ../EventManagement.App/  migrations add InitialCreate -c ApplicationDbContext
apply migrations
dotnet ef database --startup-project ../EventManagement.App/ update -c ApplicationDbContext
4. test api crud in postman