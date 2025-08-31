# ✈️ FlightApi (.NET 8 Web API)

A RESTful API built with **ASP.NET Core 8** for managing flight information.  
It supports full CRUD operations, search filters, and includes unit tests using **xUnit**.

---

## 📁 Project Structure

```
FlightApi/
├── Controllers/      # API Controllers
├── Data/             # EF Core DbContext
├── Models/           # Domain models and enums
├── Repositories/     # Data access layer
├── Services/         # Business logic layer
├── Validation/       # Custom validation (DateGreaterThanAttribute.cs)
├── Program.cs        # App entrypoint
├── FlightApi.csproj
|
FlightApi.Tests/      # Unit test project (xUnit)
└── FlightApi.Tests.csproj
```

---

## ✅ Prerequisites

- **Visual Studio 2022** (v17.8+)
- **.NET 8 SDK**
- Installed NuGet packages:
  - `Microsoft.EntityFrameworkCore.InMemory`
  - `Swashbuckle.AspNetCore`
  - `xunit`, `Moq`, `Microsoft.AspNetCore.Mvc.Testing` (for tests)

---

## 🚀 Quick Start

### 1. Build the Project

```bash
dotnet build
```

### 2. Run the API

```bash
dotnet run --project FlightApi
```

> By default, the API runs at `https://localhost:7283`.

### 3. Access Swagger UI

Open: [https://localhost:7283/swagger](https://localhost:7283/swagger)

### 4. Run Unit Tests

```bash
dotnet test FlightApi.Tests
```

Or via **Visual Studio Test Explorer**:  
- Open **Test Explorer** → Click **Run All Tests**.

---

## 🔍 API Endpoints

| Method | Endpoint                     | Description                  |
|--------|------------------------------|------------------------------|
| GET    | `/api/flights`               | List all flights             |
| GET    | `/api/flights/{id}`          | Get flight by ID             |
| POST   | `/api/flights`               | Create a new flight          |
| PUT    | `/api/flights/{id}`          | Update a flight              |
| DELETE | `/api/flights/{id}`          | Delete a flight              |
| GET    | `/api/flights/search`        | Search by airline/airport    |

---

## ❗ Optional Search Parameters

For `/api/flights/search`, you can provide any combination of:

- `airline`  
- `departure`  
- `arrival`

Example:
```http
GET /api/flights/search?airline=Delta
```

## 🛠 Design Decisions

- **InMemory Database:** Chosen for simplicity and easy testing.  
- **SOLID Principles:** Applied via **Service + Repository layers** for clean separation of concerns.  
- **Swagger Integration:** Interactive API documentation for testing endpoints.  
- **Unit Testing:** xUnit + Moq used for services and controllers.

---

## 📄 License

This project is licensed under the MIT License.

