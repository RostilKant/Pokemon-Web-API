Pokemon-Web-API

If u got stuck with dotnet ef migrations add XXX, u would try to add flags: '-s' with mainDirectory(Pokemon-Web-API) & '-p' targetDirectory with DbContext(Entities)
But if your migration assembly project is main project, u should simply run dotnet ef migrations add XXX -s MainProject(Pokemon-Web-API)
