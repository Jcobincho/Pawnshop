# Pawnshop

Pawnshop is a modular application built with .NET, following clean architecture principles. The system is designed to support trading operations, item management, and extensible business logic, with a clear separation between application layers.

## Architecture

The solution is structured using a layered approach:

```
Pawnshop.sln
│
├── Pawnshop.Domain         # Core domain models and business rules
├── Pawnshop.Application    # Application logic and use cases
├── Pawnshop.Infrastructure # External integrations (database, services)
├── Pawnshop.Api            # Backend API
└── Pawnshop.Web            # Frontend (UI layer)
```

This separation ensures maintainability, scalability, and testability.

## Features

* Modular and extensible architecture
* Separation of concerns (Domain / Application / Infrastructure)
* REST API for backend communication
* Web interface for user interaction
* Support for localization (PL / EN)
* Trading and item management system
* Docker support for containerized environments

## Technologies

* .NET
* ASP.NET Core
* Docker / Docker Compose
* REST API
* (optional) Entity Framework Core

## Getting Started

### Requirements

* .NET SDK
* Docker (optional, recommended)

### Running with Docker

```bash
docker-compose up --build
```

## Configuration

Application configuration is managed via standard .NET configuration files:

* `appsettings.json`
* environment variables
* Docker configuration (`docker-compose.yml`)

## Development

The project follows clean architecture principles:

* Domain layer contains no external dependencies
* Application layer defines use cases and business workflows
* Infrastructure layer handles persistence and external services
* API exposes endpoints
* Web provides user interface

This repository is actively evolving. The current focus includes UI improvements, localization, and enhancements to the trading system.
