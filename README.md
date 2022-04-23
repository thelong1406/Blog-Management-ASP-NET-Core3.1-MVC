# Blog Management ASP.NET Core 3.1 MVC
## Overview
This is my mini project used for studying. 

Like it's name, it's a Blog or Blog management web application.

In this project, I have 
- Used `Microsoft.AspNetCore.Identity` package for identitying the users.
  - I have inherit the class `IdentityUser` in class `User`.
  - Used `Microsoft.AspNetCore.Identity.UI` to generate the view/UI for manage users information.
  - Installed `Microsoft.AspNetCore.Identity.EntityFrameworkCore` to complie with EntityFramework when `OnModelCreating` method running. 

- Implemented design pattern in class library `Repository`.
  - `UnitOfWork`: Manage unit of work of data repository.
  - `Generic Repository`: Implemented for other data repository can inherit the inner method.
  - `Data Repository`: Access directly to the DbContext.

- Used `AutoMapper` to convert data model and view model

- Database of this project base on [this sample](https://mysql.tutorials24x7.com/blog/guide-to-design-a-database-for-blog-management-in-mysql).

Ok, that's it. :+1:

>**This is not a completed project, some logic is wrong in the real world.**

## Installation
- Configure your connection string in file: `~/BlogManagementMVC/appsettings.json.`
  - Key: `"ConnectionStrings:DefaultConnection:{your connection string here}"`
 > You can rename the connection strings key name instead of `DefaultConnection`
- Migration (make sure you have configure the conntection string)
  - Open solution in Visual Studio, open `Package Manager Tool` 
  - Choose project `DataAccess`
  - Type command: `Update-Database`
- If you want a faster way to have data in database. You can ignore step Migration above.
Just restoring database by file: `~/BlogManagementMVC.bak` in SSMS (SQL Server Management Tool). 
Make sure your database name in `appsettings.json` matched with the database you named in SQL server. 
Then, let's run the app. 

## Notices
- My code use .NET Core version 3.1. 
So, if you use my code to another version (ex: .NET 6), make sure that the NuGet packages is updated to the lasted version and the syntax is a little bit different with .NET 6 code.
