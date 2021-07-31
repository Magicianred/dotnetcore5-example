# Example project dotnet core 5

!!! working in progress !!!

An experiment of a project structure with many repository from different sources/providers in dotnet core 5.

## Create a project

Open visual studio and create a *ASP.NET Core Web API*

Give a *Solution name* (for example: *it.example.dotnetcore5*) and add a project name to solution name (for example: *it.example.dotnetcore5.webapi*)

Select *Target Framework* as *.NET 5.0 (Current)*, *Authentication Type* None.

This project will be your WebApi controllers.

## Add Domain model project

Add a new project *Class library (for .NET Standard or .NET Core)*, call it (for example: *it.example.dotnetcore5.domain*) and select *Target Framework* as *.NET 5.0 (Current)*

This project will be your domain model class. This project contains Interfaces (models, repositories and services) and Classes for the Models of the solution.

## Add DAL(s) project(s)

Add a new project *Class library (for .NET Standard or .NET Core)*, call it (for example: *it.example.dotnetcore5.dal.<your_provider>*) and select *Target Framework* as *.NET 5.0 (Current)*

This project will be your dal classes and contexts, you have to create a project for each providers you need (for example: json, restapi, csv, mysql ef, sqlserver ef, sqllite ef, etc).

This project have reference to Domain project for import Interfaces.

### Dapper provider

Add this nuget packages:
- *Dapper*
- *Microsoft.Extensions.Configuration.Abstractions*
- *MySql.Data*
- *System.Data.SqlClient*

### EF providers

Install the follow nuget package:
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design

#### EF SqlServer provider (database first)

Install the follow nuget package:
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools (for scaffolding)

Set this project as Startup project (in our case this is a library and not the primary project - webapi project)

Open *Package Manager Console* and set this project as "Default project", run this command:

```shell
Scaffold-DbContext <your_connection_string> Microsoft.EntityFrameworkCore.SqlServer -OutputDir <your_folder_ef> -Table <your_table>  
```

- <your_connection_string> : is the connection string of your sql server db
- <your_folder_ef> : is the folder where you want put model created with scaffolding
- <your_table> : this is optional, use param *-Table* if you want made scaffolding of specific table (without it will be created all the table in database)

You can run the same command any time you want create or update the ef models.

#### EF MySQL provider

#### EF Sqlite provider

### Json file provider



## Add Service project

Add a new project *Class library (for .NET Standard or .NET Core)*, call it (for example: *it.example.dotnetcore5.bl*) and select *Target Framework* as *.NET 5.0 (Current)*



[TO COMPLETE]