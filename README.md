# Task Manager Backend API

This repository contains the backend API for the Task Manager application built with .NET 8, MediatR, FluentValidation, and Entity Framework Core.
It provides endpoints for managing boards, lists, and cards, following best practices and coding conventions as specified.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (or LocalDB)
- [Git](https://git-scm.com/)
- An IDE such as [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Installation
1. **Clone** repository

   ```bash
   git clone https://github.com/your-username/task-manager-be.git
   cd task-manager-be

2. Setup Connection String
   - Use User Secrets or environment variable for ConnectionStrings:TaskManagerDB.
   - User secrets example:
   
   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:AccelokaDB" "Server=localhost;Database=TaskManagerDB;User Id=sa;Password=xxx;Encrypt=False"
   ``` 

3. Restore & Build

   ```bash
   dotnet restore
   dotnet build
   ```

4. Run

   ```bash
   dotnet run
   ```
   - Atau dari Visual Studio: F5 / Start Debug.

5. Access Swagger
   - Open https://localhost:<port>/swagger for endpoint documentation.

## Database Setup
A SQL script named TaskManagerDB.sql is provided in the Database folder.
To set up the database:
1. Open the TaskManagerDB.sql file in SQL Server Management Studio (SSMS).
2. Run the script to create the database and tables (Board, List, Card).