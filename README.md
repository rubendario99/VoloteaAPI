# VoloteaTiendaCRUD

Welcome to VoloteaTiendaCRUD! This repository contains a .NET Core project for an online clothing store application with CRUD functionalities.

## Requirements

- .NET Core >= 2.0 or .NET Framework >= 4.6.1
- SQL Server 2012 or higher

## Getting Started

1. Clone this repository to your local machine.
2. Create a database in SQL Server (e.g., "VoloteaStore").
3. Run the SQL script provided (`database_script.sql`) to create the necessary tables in your database.
4. Open the solution in Visual Studio or your preferred IDE.
5. Update the connection string in `appsettings.json` to point to your SQL Server instance.
6. Build and run the application.

## Project Structure

The project follows a typical layered architecture, with separate layers for data access, business logic, and presentation.

- **VoloteaTiendaCRUD.API**: Contains the ASP.NET Core Web API controllers.
- **VoloteaTiendaCRUD.Models**: Defines the data models used in the application.
- **VoloteaTiendaCRUD.Repositories**: Contains the repository interfaces and implementations.
- **VoloteaTiendaCRUD.Services**: Contains the service interfaces and implementations.
- **VoloteaTiendaCRUD.Tests**: Contains unit tests for the application.

## Running Tests

The project includes unit tests to ensure the correctness of its functionalities. You can run the tests using your preferred test runner or directly from the command line.

```bash
dotnet test
