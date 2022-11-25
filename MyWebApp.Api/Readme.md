# Creating or Updating the Database
Use the following command to create or update the database schema.

Package Manager Console
PM> Update-Database

CLI
> dotnet ef database update

The Update command will create the database based on the context and 
domain classes and the migration snapshot, which is created using the 
add-migration or add command.

If this is the first migration, then it will also create a table called 
__EFMigrationsHistory, which will store the name of all migrations, 
as and when they will be applied to the database.
