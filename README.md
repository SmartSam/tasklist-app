# tasklist-app

Simple task list app with ASP.NET Core WebAPI (v3.1) and ASP.NET Core Blazor (v3.1)

## WebAPI Development server

Open a terminal window then cd to ToDoList.Api and run: `dotnet watch run`. Navigate to `http://localhost:5001/`. The app will automatically reload if you change any of the source files. 

## Blazor Development server

Open another terminal window then cd to ToDoList.App and run: `dotnet watch run`. Navigate to `http://localhost:5002/`. The app will automatically reload if you change any of the source files.

## Database

Currently using SQLite. Database (todoapp.db) was setup with EF Core migrations. FYI: "DB Browser for SQLite" is an excellent tool for SQLite DBs. 

## Unit Tests

Using NUnit/Moq for Unit tests against the ToDoItemController.    
