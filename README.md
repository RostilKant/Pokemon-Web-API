Pokemon-Web-API

If u got stuck with dotnet ef migrations add XXX, u would try to add flags: 
'-s' with mainDirectory(Pokemon-Web-API) & '-p' targetDirectory with DbContext(Entities)
But if your migration assembly project is main project, u should simply run dotnet ef migrations add XXX -s MainProject(Pokemon-Web-API)

Swagger:
For using Swagger u should to registrate at first, if u want to use PUT, POST, PATCH - use 'Administrator' role in the list of roles.
After registration go to login and get JWT, after that click button 'Authorize', and paste your JWT as here: 'Bearer TOKEN', where 'TOKEN' is your JWT, coppied during login.
