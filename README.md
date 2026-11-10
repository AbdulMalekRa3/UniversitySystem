# University System API

A RESTful Web API for managing university students, built with **ASP.NET Core** and a clean **three-layer architecture**. Includes user registration and login with **ASP.NET Identity + JWT**, role-based authorization, validation, and centralized error handling.

## Tech stack

- ASP.NET Core Web API (C#)
- Entity Framework Core + SQL Server (code-first migrations)
- ASP.NET Core Identity + JWT Bearer authentication
- Autofac (dependency injection modules)
- AutoWrapper (consistent API responses)
- Serilog (logging)
- Swagger / OpenAPI

## Architecture

```
UniversitySystemSummer        → API layer: controllers, filters, configuration, helpers
UniversitySystemSummer.Core   → Business layer: services, DTOs, forms, validation, exceptions
UniversitySystemSummer.Data   → Data layer: entities, DbContext, EF configurations, repositories, migrations
```

- Controllers stay thin and call services from the Core layer
- Services hold the business rules and validation and throw `BusinessException` for invalid input
- Repositories in the Data layer wrap EF Core data access
- A global `ApiExceptionFilter` turns exceptions into consistent JSON responses

## Features

- Register and log in (Identity users and roles, JWT tokens)
- Role-based access (for example, `Teacher`-only endpoints)
- Student CRUD endpoints under `/api/students`
- Request validation and centralized error handling
- Swagger UI for exploring and testing the API

## Getting started

```bash
git clone https://github.com/AbdulMalekRa3/UniversitySystem.git
cd UniversitySystem
```

1. Set the connection string and JWT settings in `UniversitySystemSummer/appsettings.json` (or use `dotnet user-secrets`)
2. Apply migrations:
   ```bash
   dotnet ef database update --project UniversitySystemSummer.Data --startup-project UniversitySystemSummer
   ```
3. Run the API:
   ```bash
   dotnet run --project UniversitySystemSummer
   ```
4. Open `https://localhost:7127/swagger`

## Author

**Abdul Malek Rajab** — [Portfolio](https://abdulmalekra3.github.io/Abdulmalekportfolio/) · [LinkedIn](https://www.linkedin.com/in/abdulmalek-rajab-287283287/)
