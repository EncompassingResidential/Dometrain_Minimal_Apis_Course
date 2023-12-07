
Visual Studio 2022

This was all added in C# version 9.0

My current language version is 11.0 as of 12/01/2023

Using .NET 7

Frameworks
- MS.NETCore.App
- MS.AspNetCore.App

Dependencies (all installed in Visual Studio via Manage NuGet packages)

- Swagger API Service - Swashbuckle.AspNetCore 6.5.0
- MS SQLite 8.0.0     - SQL database is in library.db file

I think Nick used Dapper because of using SQLite.  I forget what his video stated.
- Dapper 2.1.24       - https://dappertutorial.net/dapper
    from Stack Overflow team
    ORM is an Object Relational Mapper
    mapping between a database and a programming language

- FluentValidation.DependencyInjectionExtension 
     11.8.1 which auto installs Parent FluentValidation 11.8.1
     https://docs.fluentvalidation.net/en/latest/di.html
     Validators can be used with any dependency injection library, such as "Microsoft.Extensions.DependencyInjection".


Project Description

This Library ISBN project is in 2nd section of course, 
"From Zero to Hero - Minimal APIs in .NET with C#"
by Nick Chapsas of Dometrain

- Added three Endpoints in this initial commit & push 
POST create Book - /books - JSON Book
GET Retrieve ALL Books - /books - no parameters
GET Retrieve Book via ISBN - /books - ISBN 10 or 13 length string

