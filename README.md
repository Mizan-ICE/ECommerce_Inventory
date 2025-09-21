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

### 2. Configure Connection String

Update the connection string in `ECommerce_Inventory.Api/appsettings.json`:

### 3. Install Dependencies

Navigate to the solution directory and restore packages:

### 4. Database Setup

#### Option A: Using Entity Framework Migrations (Recommended)

1. **Install EF Core Tools** (if not already installed): 
2. **Navigate to the Infrastructure project:**
3. **Create and apply migrations:**

   
#### Option B: Using Package Manager Console (Visual Studio)

1. Open **Package Manager Console** in Visual Studio
2. Set **Default project** to `ECommerce_Inventory.Infrastructure`
3. Run the following commands:

   
### 5. Configure JWT Settings (Optional)

Update JWT settings in `appsettings.json` if needed:

### 6. Run the Application

Navigate to the API project and run:

Or using Visual Studio:
- Set `ECommerce_Inventory.Api` as the startup project
- Press `F5` or click **Run**

### 7. Access the Application

- **API Base URL:** `https://localhost:44323`
- **Swagger UI:** `https://localhost:44323/swagger`
- **API Documentation:** Available through Swagger UI

## 📚 API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh-token` - Refresh access token

### Products
- `GET /api/products` - Get all products with filtering
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/search?q=keyword` - Search products
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create new category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

## 🔧 Configuration Options

### Database Configuration

You can configure different database providers by updating the connection string and modifying the `Program.cs` file:

### Logging Configuration

Logging is configured using Serilog. Update `appsettings.json` to modify logging behavior:

## 🧪 Testing the API

### Using Swagger UI

1. Navigate to `https://localhost:44323/swagger`
2. Register a new user via `/api/auth/register`
3. Login via `/api/auth/login` to get JWT token
4. Use the "Authorize" button in Swagger to set the Bearer token
5. Test other endpoints

### Using curl

## 🚨 Troubleshooting

### Common Issues

1. **Database Connection Issues:**
   - Verify SQL Server is running
   - Check connection string format
   - Ensure database exists or can be created

2. **Migration Issues:**
   - Ensure EF Core tools are installed
   - Check that Infrastructure project is selected
   - Verify connection string is accessible

3. **JWT Token Issues:**
   - Ensure JWT key is long enough (minimum 32 characters)
   - Check token expiration settings
   - Verify issuer and audience URLs

4. **Port Conflicts:**
   - Change port in `launchSettings.json` if 44323 is occupied
   - Update JWT issuer/audience URLs accordingly

### Logs Location

Application logs are written to:
- **Console** (during development)
- **File**: `Logs/web-log-{date}.log`

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/new-feature`
3. Commit changes: `git commit -am 'Add new feature'`
4. Push to branch: `git push origin feature/new-feature`
5. Submit a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

- **Mizan** - *Initial work* - [Mizan-ICE](https://github.com/Mizan-ICE)

## 🙏 Acknowledgments

- Clean Architecture principles
- .NET 9 and Entity Framework Core documentation
- JWT authentication best practices
