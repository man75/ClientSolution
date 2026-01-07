# ClientSolution

![Architecture](https://img.shields.io/badge/architecture-Clean%20%2F%20Hexagonal-blue)
![Language](https://img.shields.io/badge/language-C%23-green)
![Framework](https://img.shields.io/badge/framework-.NET%20%7C%20ASP.NET%20Core-lightgrey)

ClientSolution is a Clean / Hexagonal .NET project that demonstrates a
well-structured architecture for building a scalable and maintainable
API.\
The current focus is on the **AddClient use case**, with clear
separation of concerns between layers.

------------------------------------------------------------------------

## 🧱 Project Structure

    ClientSolution
    │
    ├── ClientSi.DOMAIN
    ├── ClientSi.APPLICATION
    ├── ClientSi.INFRASTRUCTURE
    └── ClientSi.API

### 📌 Layers Overview

  ------------------------------------------------------------------------
  Layer                Responsibility
  -------------------- ---------------------------------------------------
  **Domain**           Business entities, rules and core interfaces

  **Application**      Use cases, business orchestration, validation

  **Infrastructure**   External technical concerns (DB, EF Core,
                       messaging, etc.)

  **API**              REST endpoints, DTOs, presentation adapters
  ------------------------------------------------------------------------

------------------------------------------------------------------------
```mermaid
sequenceDiagram
    autonumber

    participant U as Utilisateur (Front)
    participant API as ClientController (API)
    participant UCVal as AddClientUseCaseValidation
    participant UC as AddClientUseCase
    participant Repo as IClientRepository
    participant NotifSvc as INotificationService (RabbitMQ Producer)
    participant MQ as RabbitMQ
    participant NotifAPI as Notification.API (ClientCreatedConsumer)

    U->>API: POST /api/client/add (AddClientDto)
    API->>API: Mapper DTO -> Client
    API->>UCVal: SetOutputPort(this)
    API->>UCVal: Execute(client)

    UCVal->>UCVal: Validate(client)

    alt Client invalide
        UCVal->>API: IOutputPort.Invalid()
        API-->>U: 400 Bad Request (Validation Errors)
    else Client valide
        UCVal->>UC: Execute(client)

        UC->>Repo: AddAsync(client)
        Repo-->>UC: OK

        UC->>NotifSvc: NotifyClientCreatedAsync(client)
        NotifSvc->>MQ: Publish client.created
        MQ-->>NotifSvc: ACK

        UC->>API: IOutputPort.Ok(response)
        API-->>U: 200 OK
    end

    MQ-->>NotifAPI: client.created
    NotifAPI->>NotifAPI: Traiter notification
```

## 🧠 Architectural Principles

This project follows **Clean / Hexagonal Architecture**:

-   **Domain** is pure and independent of frameworks.
-   **Application** contains use case logic and validation.
-   **Infrastructure** implements technical details like persistence.
-   **API** maps DTOs, calls use cases, and returns HTTP results.

Dependencies always flow inward:

    ClientSi.API → ClientSi.APPLICATION → ClientSi.DOMAIN
                            ↑
                    ClientSi.INFRASTRUCTURE

------------------------------------------------------------------------

## 🚀 Add Client Use Case

This API supports adding a new client via:

    POST /api/AddClient/ajouter

### Example Request

``` json
{
  "nom": "Dupont",
  "prenom": "Jean",
  "typologie": "FOURNISSEUR"
}
```

### Expected Responses

-   **200 OK** -- on success
-   **400 Bad Request** -- on validation failure

------------------------------------------------------------------------

## 🛠 Setting Up

### ⚙️ Prerequisites

-   .NET 6.0 or later
-   SQL Server (local or remote)

------------------------------------------------------------------------

## 📦 Database Setup

Add your connection string to `ClientSi.API/appsettings.json`:

``` json
{
  "ConnectionStrings": {
    "ClientDb": "Server=(localdb)\\MSSQLLocalDB;Database=ClientDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Register your DbContext in `Program.cs`.

------------------------------------------------------------------------

## 🗺 Dependency Registration

Dependencies are configured in the API startup to inject:

-   Repository implementations
-   Use cases
-   Validation decorators
-   AutoMapper
-   DbContext

------------------------------------------------------------------------

## 🧪 Running Migrations

Run EF Core migrations from Infrastructure with API as startup project.

------------------------------------------------------------------------

## 🚀 Testing

You can import a Postman collection to test the AddClient endpoint.

------------------------------------------------------------------------

## 📈 Design Goals

-   Maintainability\
-   Strict separation of responsibilities\
-   No framework leakage into Domain\
-   Easy extension (e.g., UpdateClient, RabbitMQ integration)

------------------------------------------------------------------------

## 📅 Roadmap

-   ✔ AddClient
-   ⏳ UpdateClient
-   📤 RabbitMQ event publishing
-   🔐 Authentication / Authorization
-   📊 Unit & integration tests

------------------------------------------------------------------------

## 🤝 Contributing

Feel free to open issues or submit pull requests.

------------------------------------------------------------------------

## 📝 License

This repository is open-source under the MIT License.
