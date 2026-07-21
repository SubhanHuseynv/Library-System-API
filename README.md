# LibrarySystem

This project is a library management system built with **.NET Web API**, following the **Layered Architecture** pattern.

## 📐 Architecture

The project consists of the following layers:

```
LibrarySystem/
 ├── Presentation/
 │    └── LibrarySystem.API            → Controllers, Swagger, Program.cs
 ├── Core/
 │    ├── LibrarySystem.Application    → Services, DTOs, Interfaces, Validators
 │    └── LibrarySystem.Domain         → Entities (Core business models)
 └── Infrastructure/
      ├── LibrarySystem.Infrastructure → Shared infrastructure (Middleware, etc.)
      └── LibrarySystem.Persistence    → DbContext, Repositories, EF Core configurations
```

The application follows a Controller → Service → Repository flow, with all layers interacting through **Dependency Injection (DI)**.

## 🛠 Technologies used

| Technology | Purpose |
|---|---|
| ASP.NET Core Web API | Core of the API |
| Entity Framework Core | ORM, database access |
| FluentValidation | Input validation |
| AutoMapper | Entity ↔ DTO mapping (in some services) |
| Swagger / Swashbuckle | API documentation and testing interface |
| Generic Repository Pattern | Prevents code duplication |

## ✨ Key features

- **DTO usage** — Entities are never exposed directly to the client; all responses are returned as DTOs.
- **Generic Repository** — Each entity-specific repository inherits from a generic repository, eliminating code duplication.
- **Pagination & Sorting** — Implemented in `GetAll` methods for efficient data retrieval.
- **Global Exception Handling** — All exceptions are caught by a centralized middleware and returned with appropriate status codes.
- **Proper HTTP Status Codes** — `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, `404 Not Found`, etc. are used according to the scenario.
- **FluentValidation** — Rule-based validation applied on request DTOs.
- **Swagger UI** — All endpoints can be tested interactively.

## 🚀 Getting started

```bash
# Clone the repository
git clone <repo-link>
cd LibrarySystem

# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update --project LibrarySystem.Persistence --startup-project LibrarySystem.API

# Run the project
dotnet run --project LibrarySystem.API
```

Once running, access the Swagger UI at:
```
https://localhost:{port}/swagger
```

## 📡 API Endpoints (example)

| Method | Endpoint | Description | Status code |
|---|---|---|---|
| GET | `/api/books` | Retrieves all books (with pagination/sorting) | 200 |
| GET | `/api/books/{id}` | Retrieves a book by ID | 200 / 404 |
| POST | `/api/books` | Creates a new book | 201 / 400 |
| PUT | `/api/books/{id}` | Updates an existing book | 204 / 404 |
| DELETE | `/api/books/{id}` | Deletes a book | 204 / 404 |

## ⚠️ Note

Unit and Integration tests have **not yet been added** to the project — this part is still in progress. All other parts of the project (architecture, CRUD operations, validation, exception handling, Swagger) are fully completed.

## 📁 Project structure (Solution)

The solution consists of 5 projects:
- `LibrarySystem.API`
- `LibrarySystem.Application`
- `LibrarySystem.Domain`
- `LibrarySystem.Infrastructure`
- `LibrarySystem.Persistence`
