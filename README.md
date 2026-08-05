# ProductApi

A learning-focused ASP.NET Core Web API application built using .NET, Entity Framework Core, and PostgreSQL.

The purpose of this project is to understand how a real-world REST API is structured, how requests flow through the application, and how different .NET components work together.

---

# Technologies Used

* .NET 10
* ASP.NET Core Web API
* C#
* Entity Framework Core
* PostgreSQL
* Swagger / OpenAPI
* Dependency Injection

---

# Application Architecture

The application follows a layered architecture approach.

```
Client
  |
  ↓
Controller
  |
  ↓
Service Layer
  |
  ↓
Entity Framework Core
  |
  ↓
PostgreSQL Database
```

## Controller Layer

Location:

```
Controllers/
```

Responsibilities:

* Receives HTTP requests.
* Validates incoming request flow.
* Calls the service layer.
* Returns HTTP responses.

Controllers should not contain database logic.

Example:

```
ProductsController
        |
        ↓
IProductService
```

---

## Service Layer

Location:

```
Services/
```

The service layer contains application business logic.

Responsibilities:

* Process product operations.
* Communicate with Entity Framework Core.
* Apply business rules.
* Keep controllers clean.

Structure:

```
Services
│
├── Interfaces
│   └── IProductService.cs
│
└── ProductService.cs
```

---

# Dependency Injection (DI)

ASP.NET Core uses Dependency Injection to create and provide required objects automatically.

Instead of manually creating objects:

```csharp
var service = new ProductService();
```

ASP.NET Core creates and provides the required service.

Example:

```csharp
public ProductsController(
    IProductService productService)
{
    _productService = productService;
}
```

The controller requests `IProductService`, and ASP.NET Core provides the registered implementation.

Registration happens in `Program.cs`:

```csharp
builder.Services.AddScoped<IProductService, ProductService>();
```

## Scoped Lifetime

The service is registered with:

```csharp
AddScoped()
```

Meaning:

* One instance is created per HTTP request.
* The same instance is reused during that request.
* A new instance is created for the next request.

Benefits of Dependency Injection:

* Reduces dependency between classes.
* Makes code easier to maintain.
* Makes unit testing easier.
* Centralizes object creation.

---

# Entity Framework Core

Entity Framework Core (EF Core) is used to communicate with PostgreSQL using C# objects instead of manually writing SQL queries.

Example:

Instead of:

```sql
SELECT * FROM Products;
```

We use:

```csharp
_context.Products.ToListAsync();
```

EF Core converts C# operations into SQL queries.

---

# Database Layer

Location:

```
Data/
```

The application uses:

```
AppDbContext
```

Responsibilities:

* Manage database connection.
* Track entity changes.
* Execute database operations.

Database operations are performed using:

* Add()
* Find()
* Remove()
* SaveChangesAsync()

---

# Database Migrations

Entity Framework Core migrations keep the database schema synchronized with application models.

Migration workflow:

```
Modify Model
      |
      ↓
Create Migration
      |
      ↓
Apply Migration
      |
      ↓
Database Updated
```

Commands:

Create migration:

```bash
dotnet ef migrations add MigrationName
```

Apply migration:

```bash
dotnet ef database update
```

---

# Product API Endpoints

## Get All Products

```
GET /api/products
```

Returns all products.

---

## Get Product By ID

```
GET /api/products/{id}
```

Returns a specific product.

---

## Create Product

```
POST /api/products
```

Request body:

```json
{
  "name": "Laptop",
  "price": 85000,
  "category": "Electronics"
}
```

---

## Update Product

```
PUT /api/products/{id}
```

Updates an existing product.

---

## Delete Product

```
DELETE /api/products/{id}
```

Deletes a product.

---

# Project Structure

```
ProductApi
│
├── Controllers
│   └── ProductsController.cs
│
├── Services
│   ├── Interfaces
│   │   └── IProductService.cs
│   │
│   └── ProductService.cs
│
├── Data
│   └── AppDbContext.cs
│
├── Models
│   └── Product.cs
│
├── DTOs
│   └── ProductRequest.cs
│
├── Migrations
│
├── Program.cs
│
└── appsettings.json
```

---

# Application Flow

When a client sends a request:

```
HTTP Request
      |
      ↓
Controller receives request
      |
      ↓
Controller calls Service
      |
      ↓
Service performs business logic
      |
      ↓
Service uses DbContext
      |
      ↓
EF Core communicates with PostgreSQL
      |
      ↓
Response returned to client
```

---

# Running the Application

Restore dependencies:

```bash
dotnet restore
```

Build:

```bash
dotnet build
```

Run:

```bash
dotnet run
```

Swagger documentation:

```
http://localhost:5173/swagger
```

---

# Key Learnings

Through this project, we learned:

* How to create an ASP.NET Core Web API.
* How Program.cs starts and configures an application.
* How middleware and routing work.
* How controllers handle HTTP requests.
* How model binding receives client data.
* How DTOs separate API models from database models.
* How Entity Framework Core communicates with databases.
* How PostgreSQL is integrated with ASP.NET Core.
* How migrations manage database schema changes.
* How CRUD operations are implemented.
* How Dependency Injection works.
* How Service Layer architecture improves maintainability.
* How to separate responsibilities between application layers.

---

# Future Improvements

Possible enhancements:

* Add request validation.
* Add global exception handling.
* Add authentication and authorization.
* Add unit tests.
* Add repository pattern if application complexity increases.
* Add logging and monitoring.
