# ECommerce Inventory Management System

A comprehensive inventory management system built with .NET 9, featuring JWT authentication, product management, category management, and search functionality.

## 🚀 Features

- **Authentication & Authorization**
  - JWT-based authentication with refresh tokens
  - User registration and login
  - Secure token management

- **Product Management**
  - CRUD operations for products
  - Advanced search functionality
  - Product filtering and pagination
  - Category-based organization

- **Category Management**
  - Category CRUD operations
  - Product count per category
  - Category-based product filtering

- **Search & Filtering**
  - Product search by name and description
  - Filter by category, price range
  - Pagination support
  - Sorting capabilities

## 🏗️ Architecture

This project follows Clean Architecture principles with the following layers:

- **ECommerce_Inventory.Api** - Web API layer (Controllers, Swagger)
- **ECommerce_Inventory.Application** - Application layer (Services, DTOs, Interfaces)
- **ECommerce_Inventory.Domain** - Domain layer (Entities, Domain Services)
- **ECommerce_Inventory.Infrastructure** - Infrastructure layer (Data Access, External Services)

## 🛠️ Technology Stack

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0.9**
- **SQL Server / LocalDB**
- **JWT Authentication**
- **AutoMapper 15.0.1**
- **Serilog** for logging
- **Swagger/OpenAPI** for documentation

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

## ⚙️ Setup Instructions

### 1. Clone the Repository
