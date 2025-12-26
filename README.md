# ClientSolution

![Architecture](https://img.shields.io/badge/architecture-Clean%20%2F%20Hexagonal-blue)
![Language](https://img.shields.io/badge/language-C%23-green)
![Framework](https://img.shields.io/badge/framework-.NET%20%7C%20ASP.NET%20Core-lightgrey)

ClientSolution is a Clean / Hexagonal .NET project that demonstrates a well-structured architecture for building a scalable and maintainable API.  
The current focus is on the **AddClient use case**, with clear separation of concerns between layers.

---

## 🧱 Project Structure

ClientSolution
│
├── Client.DOMAIN
├── Client.APPLICATION
├── Client.INFRASTRUCTURE
└── Client.API

### 📌 Layers Overview

| Layer | Responsibility |
|------|----------------|
| **Domain** | Business entities, rules and core interfaces |
| **Application** | Use cases, business orchestration, validation |
| **Infrastructure** | External technical concerns (DB, EF Core, messaging, etc.) |
| **API** | REST endpoints, DTOs, presentation adapters |

---

## 🧠 Architectural Principles

This project follows **Clean / Hexagonal Architecture**:

- **Domain** is pure and independent of frameworks.
- **Application** contains use case logic and validation.
- **Infrastructure** implements technical details like persistence.
- **API** maps DTOs, calls use cases, and returns HTTP results.

Dependencies always flow inward:
Client.API → Client.APPLICATION → Client.DOMAIN
↑
Client.INFRASTRUCTURE

POST /api/AddClient/ajouter

### Example Request

```json
{
  "nom": "Dupont",
  "prenom": "Jean",
  "typologie": "FOURNISSEUR"
}
Expected Responses

200 OK – on success

400 Bad Request – on validation failure

🛠 Setting Up
⚙️ Prerequisites

.NET 6.0 or later

SQL Server (local or remote)
📦 Database Setup

Add your connection string to Client.API/appsettings.json:
{
  "ConnectionStrings": {
    "ClientDb": "Server=(localdb)\\MSSQLLocalDB;Database=ClientDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
Register your DbContext in Program.cs:
builder.Services.AddDbContext<ClientContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientDb"))
);
Dependency Registration
builder.Services.AddScoped<Notification>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddScoped<AddClientUseCase>();
builder.Services.AddScoped<IAddClientUseCase>(sp =>
{
    var notification = sp.GetRequiredService<Notification>();
    var innerUseCase = sp.GetRequiredService<AddClientUseCase>();
    return new AddClientUseCaseValidation(notification, innerUseCase);
});

builder.Services.AddAutoMapper(typeof(Client.API.Mapping.ClientProfile));
🧪 Running Migrations

To add and apply migrations:
dotnet ef migrations add InitClientDb -p Client.INFRASTRUCTURE -s Client.API
dotnet ef database update -p Client.INFRASTRUCTURE -s Client.API

🚀 Testing

You can import a Postman collection to test:
{
  "info": {
    "name": "Client API",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "Add Client",
      "request": {
        "method": "POST",
        "header": [
          { "key": "Content-Type", "value": "application/json" }
        ],
        "body": {
          "mode": "raw",
          "raw": "{\n  \"nom\": \"Dupont\",\n  \"prenom\": \"Jean\",\n  \"typologie\": \"FOURNISSEUR\"\n}"
        },
        "url": {
          "raw": "https://localhost:5001/api/AddClient/ajouter"
        }
      }
    }
  ]
}
📈 Design Goals

Maintainability

Layered separation of responsibilities

No framework leakage into Domain

Easy extension (e.g., UpdateClient, RabbitMQ integration)

)

📅 Roadmap

✔ AddClient

⏳ UpdateClient

📤 RabbitMQ event publishing

🔐 Authentication / Authorization

📊 Full suite of unit & integration tests
🤝 Contributing

Feel free to open issues or submit pull requests!
📝 License

This project is open-source and available under the MIT License.
