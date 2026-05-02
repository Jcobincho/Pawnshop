# Pawnshop Management System

A web application for managing pawnshop chains, built with .NET 8 (Blazor & Web API) following Clean Architecture principles.

This project was developed to streamline daily pawnshop operations, including client management, collateral valuation, automated PDF contract generation, and geographical tracking of workplaces.

---

## Key Features

* **Full i18n Localization:** Deep integration of both Polish and English languages (PL/EN) across the UI and data grid components.
* **Geographical Tracking:** Integrated geocoding (OpenStreetMap/Nominatim) and Leaflet.js maps for pinning and validating real physical pawnshop locations.
* **Automated Contracts:** System-generated PDF agreements (powered by QuestPDF) with real-time, timezone-aware timestamps.
* **Security & Identity:** Token-based authentication (JWT) with Refresh Tokens and role-based access control.
* **Business Dashboard:** Dynamic KPI tracking and live-feed of recent sales and loan operations.
* **Universal CRUD Architecture:** Highly reusable, generic components (UniversalGrid, UniversalCrudMenu) reducing frontend boilerplate.

---

## Tech Stack

### Frontend
* **Blazor Web App** - Interactive client-side web UI with C#.
* **MudBlazor** - Material Design component framework.
* **Leaflet.js** - Open-source interactive maps.

### Backend
* **.NET 8 Web API** - High-performance backend routing and RESTful endpoints.
* **Clean Architecture & CQRS** - Utilizing MediatR for decoupled request/handler logic.
* **Entity Framework Core** - ORM framework.
* **PostgreSQL** - Relational database.
* **QuestPDF** - Open-source library for advanced, code-first PDF generation.
* **MassTransit & RabbitMQ** - Message-based architecture for background processing (e.g., valuation queues, email notifications).

---

## Architecture Overview

The solution is divided into distinct layers emphasizing the separation of concerns:

* `Pawnshop.Domain` - Enterprise logic, entities, exceptions, and core enums.
* `Pawnshop.Application` - Business rules, CQRS Commands/Queries, validations, and DTOs.
* `Pawnshop.Infrastructure` - External concerns: EF Core DbContext, S3 configurations, RabbitMQ, PDF Engine, and Geocoding APIs.
* `Pawnshop.Api` - Application entry point, Controllers, Exception Filters, and Dependency Injection bootstrapping.
* `Pawnshop.Web` - The Blazor presentation layer with Themes, Localizers, and UI Components.

---

## Getting Started

### Prerequisites
* .NET 8 SDK
* Docker Desktop (for PostgreSQL & RabbitMQ)

### Local Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/YourUsername/Pawnshop.git
   cd Pawnshop
   ```

2. **Start the Infrastructure**
   Ensure Docker is running, then spin up the required databases and message brokers:
   ```bash
   docker-compose up -d
   ```

3. **Apply Database Migrations**
   ```bash
   dotnet ef database update --project Pawnshop.Infrastructure --startup-project Pawnshop.Api
   ```

4. **Run the Application**
   You can run the API and Blazor frontend using the .NET CLI or your favorite IDE.
   ```bash
   # Run the Backend API
   dotnet run --project Pawnshop.Api
   
   # Run the Blazor Web UI
   dotnet watch run --project Pawnshop.Web
   ```
