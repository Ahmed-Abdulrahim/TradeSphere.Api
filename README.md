<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 8.0" />
  <img src="https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C# 12.0" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="Entity Framework Core" />
  <img src="https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" alt="JWT" />
  <img src="https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger" />
  <img src="https://img.shields.io/badge/License-MIT-blue?style=for-the-badge" alt="License: MIT" />
</p>

<h1 align="center">🛒 TradeSphere API</h1>

<p align="center">
  <strong>A modern, scalable e-commerce RESTful API built with Clean Architecture principles</strong>
</p>

<p align="center">
  TradeSphere is a comprehensive e-commerce backend solution that provides all the essential features for building an online store — including user authentication, product management, shopping cart functionality, order processing, and customer feedback management.
</p>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Project Structure](#-project-structure)
- [Prerequisites](#-prerequisites)
- [Installation](#-installation)
- [API Endpoints](#-api-endpoints)
- [Usage Examples](#-usage-examples)
- [Contributing](#-contributing)

---

## 🌟 Overview

**TradeSphere** is a feature-rich e-commerce Web API designed to serve as the backend for online storefronts. It follows **Clean Architecture** principles, ensuring separation of concerns, maintainability, and testability. The API provides everything needed to manage an e-commerce platform, from user registration and authentication to processing orders and collecting customer feedback.

### Key Highlights

- 🔐 **Secure Authentication** — JWT-based authentication with refresh tokens
- 📧 **Email Integration** — Email confirmation and password reset functionality
- 🛍️ **Complete E-Commerce Features** — Products, Categories, Shopping Cart, Orders
- 📦 **Order Management** — Full order lifecycle with status tracking
- ⭐ **Customer Feedback** — Product review and rating system
- 🎯 **Role-Based Access Control** — Granular permission management

---

## ✨ Features

### 🔐 Authentication & Authorization
| Feature | Description |
|---------|-------------|
| User Registration | New user signup with email confirmation |
| Login/Logout | Secure session management with JWT tokens |
| Refresh Tokens | Automatic token refresh for seamless UX |
| Password Reset | Forgot password with email-based reset flow |
| Email Verification | Email confirmation for new accounts & email changes |
| Role Management | Create, assign, and manage user roles |

### 📦 Product Management
| Feature | Description |
|---------|-------------|
| CRUD Operations | Create, read, update, and delete products |
| Search by Name | Find products by name |
| Category Association | Products organized by categories |
| Price Management | Product pricing support |

### 🗂️ Category Management
| Feature | Description |
|---------|-------------|
| CRUD Operations | Full category lifecycle management |
| Search by Name | Find categories by name |
| Hierarchical Structure | Products grouped under categories |

### 🛒 Shopping Cart
| Feature | Description |
|---------|-------------|
| Add to Cart | Add products to user's cart |
| Update Quantity | Modify item quantities |
| Remove Items | Remove specific items from cart |
| Clear Cart | Empty the entire cart |
| View Cart | Get current cart contents |

### 📋 Order Management
| Feature | Description |
|---------|-------------|
| Checkout | Convert cart to order |
| Order History | View all orders by user |
| Order Status | Track order status (Pending, Processing, etc.) |
| Cancel Order | Cancel pending orders |
| Status Updates | Admin order status management |

### ⭐ Customer Feedback
| Feature | Description |
|---------|-------------|
| Add Feedback | Submit product reviews |
| View Feedback | Get product feedback/reviews |
| Manage Feedback | Update and delete reviews |

---

## 🏗️ Architecture

TradeSphere follows **Clean Architecture** principles with clear separation between layers:

```
┌────────────────────────────────────────────────────────────┐
│                    TradeSphere.Api                         │
│              (Controllers, Middlewares, Extensions)        │
├────────────────────────────────────────────────────────────┤
│                 TradeSphere.Application                    │
│            (Use Cases, DTOs, Interfaces, Mapping)          │
├────────────────────────────────────────────────────────────┤
│                TradeSphere.Infrastructure                  │
│      (Repositories, Persistence, Services, Specifications) │
├────────────────────────────────────────────────────────────┤
│                   TradeSphere.Domain                       │
│                    (Entities/Models)                       │
└────────────────────────────────────────────────────────────┘
```

### Layer Responsibilities

| Layer | Responsibility |
|-------|----------------|
| **Domain** | Core business entities and models (Product, Order, User, etc.) |
| **Application** | Business logic, use cases, DTOs, and service interfaces |
| **Infrastructure** | Data access, external services, repository implementations |
| **API** | HTTP endpoints, request/response handling, middleware |

---

## 📁 Project Structure

```
TradeSphere.Api/
├── 📂 TradeSphere.Api/              # Presentation Layer
│   ├── Controllers/
│   │   ├── AccountController.cs     # User account management
│   │   ├── AuthController.cs        # Authentication endpoints
│   │   ├── CategoryController.cs    # Category CRUD operations
│   │   ├── FeedBackController.cs    # Customer feedback endpoints
│   │   ├── OrderController.cs       # Order management
│   │   ├── ProductController.cs     # Product CRUD operations
│   │   ├── RoleController.cs        # Role management
│   │   └── ShoppingCartController.cs # Shopping cart operations
│   ├── Errors/                      # Custom error responses
│   ├── Extensions/                  # Service extensions
│   ├── Middlewares/                 # Custom middleware (Global Error Handler)
│   ├── Program.cs                   # Application entry point
│   └── appsettings.json             # Configuration settings
│
├── 📂 TradeSphere.Application/      # Application Layer
│   ├── DTOs/
│   │   ├── AuthDto/                 # Authentication DTOs
│   │   ├── Category/                # Category DTOs
│   │   ├── FeedBackDto/             # Feedback DTOs
│   │   ├── OrderDto/                # Order DTOs
│   │   ├── ProductAddDto/           # Product DTOs
│   │   ├── ShoppingCartDto/         # Shopping cart DTOs
│   │   └── RolesDto/                # Role DTOs
│   ├── Interfaces/                  # Service & repository interfaces
│   ├── Mapping/                     # AutoMapper profiles
│   └── UseCases/
│       ├── AccountUseCase.cs        # Account business logic
│       ├── AuthUseCase.cs           # Authentication logic
│       ├── CategoryUseCase.cs       # Category operations
│       ├── FeedBackUseCase.cs       # Feedback logic
│       ├── OrderUseCase.cs          # Order processing
│       ├── ProductUseCase.cs        # Product operations
│       ├── RoleUseCase.cs           # Role management
│       └── ShoppingCartUseCase.cs   # Cart operations
│
├── 📂 TradeSphere.Infrastructure/   # Infrastructure Layer
│   ├── Persistence/                 # DbContext and Migrations
│   ├── Repositories/
│   │   ├── AuthRepository/          # User authentication data access
│   │   ├── CategoryRepository/      # Category data access
│   │   ├── FeedBackRepository/      # Feedback data access
│   │   ├── MainRepository/          # Generic repository
│   │   ├── OrderRepository/         # Order data access
│   │   ├── ProductRepository/       # Product data access
│   │   ├── RoleRepository/          # Role data access
│   │   └── ShoppingCartRepository/  # Cart data access
│   ├── Services/                    # External service implementations
│   ├── Specification/               # Specification pattern implementations
│   └── UnitOfWork/                  # Unit of Work pattern
│
├── 📂 TradeSphere.Domain/           # Domain Layer
│   └── Models/
│       ├── BaseEntity.cs            # Base entity class
│       ├── CartItem.cs              # Cart item entity
│       ├── Category.cs              # Category entity
│       ├── FeedBack.cs              # Feedback entity
│       ├── Order.cs                 # Order entity
│       ├── OrderItem.cs             # Order item entity
│       ├── Payment.cs               # Payment entity
│       ├── Product.cs               # Product entity
│       ├── ShoppingCart.cs          # Shopping cart entity
│       └── IdentityUser/
│           ├── AppUser.cs           # Application user (ASP.NET Identity)
│           ├── AppRole.cs           # Application role
│           └── RefreshToken.cs      # Refresh token entity
│
└── 📄 TradeSphere.Api.sln           # Solution file
```

---

## 🛠️ Technologies Used

| Technology | Version | Purpose |
|------------|---------|---------|
| ![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet) | 8.0 | Core framework |
| ![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp) | 12.0 | Programming language |
| ![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?logo=dotnet) | 8.0.22 | ORM / Data access |
| ![SQL Server](https://img.shields.io/badge/SQL%20Server-Latest-CC2927?logo=microsoftsqlserver) | Latest | Database |
| ![JWT](https://img.shields.io/badge/JWT-Bearer-000000?logo=jsonwebtokens) | 8.0.22 | Authentication |
| ![AutoMapper](https://img.shields.io/badge/AutoMapper-13.0-BE1621) | 13.0.1 | Object mapping |
| ![MailKit](https://img.shields.io/badge/MailKit-4.14-blue) | 4.14.1 | Email services |
| ![Swagger](https://img.shields.io/badge/Swagger-6.6-85EA2D?logo=swagger) | 6.6.2 | API documentation |

---

## 📋 Prerequisites

Before running TradeSphere, ensure you have the following installed:

- ✅ [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- ✅ [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or full version)
- ✅ [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with C# extension
- ✅ [Git](https://git-scm.com/) for version control

---

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/Ahmed-Abdulrahim/TradeSphere.git
cd TradeSphere/TradeSphere.Api
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Configure the Database

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "conn1": "Data Source=YOUR_SERVER;Initial Catalog=TradeSphere;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Apply Migrations

```bash
dotnet ef database update --project TradeSphere.Infrastructure --startup-project TradeSphere.Api
```

> **Note:** The application also auto-applies migrations on startup via `ApplyMigrationWithSeed()`.

### 5. Run the Application

```bash
dotnet run --project TradeSphere.Api
```

The API will be available at:
- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:7013`
- **Swagger UI:** `https://localhost:7013/swagger`

---

## ⚙️ Configuration

### Application Settings (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "conn1": "Your SQL Server connection string"
  },
  "JwtOptions": {
    "issuer": "your-issuer",
    "audience": "your-audience",
    "secretKey": "your-super-secret-key-minimum-32-characters"
  },
  "EmailSettings": {
    "From": "noreply@yourdomain.com",
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "AppUrl": "https://localhost:7013"
  }
}
```

### Environment Variables

For production, use environment variables or user secrets:

```bash
dotnet user-secrets set "JwtOptions:secretKey" "your-production-secret-key"
dotnet user-secrets set "EmailSettings:Password" "your-email-app-password"
```

---

## 📡 API Endpoints

### 🔐 Authentication (`/api/Auth`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/Auth/login` | User login |
| `POST` | `/api/Auth/register` | User registration |
| `GET` | `/api/Auth/confirmEmail` | Confirm email address |
| `GET` | `/api/Auth/confirmChangeEmail` | Confirm email change |
| `POST` | `/api/Auth/forgotPassword` | Request password reset |
| `POST` | `/api/Auth/resetPassword` | Reset password |
| `POST` | `/api/Auth/refreshToken` | Refresh access token |
| `POST` | `/api/Auth/logout` | User logout |

### 👤 Account (`/api/Account`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Account` | Get current user info |
| `PUT` | `/api/Account` | Update account details |

### 📦 Products (`/api/Product`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Product` | Get all products |
| `GET` | `/api/Product/{id}` | Get product by ID |
| `GET` | `/api/Product/GetByName{name}` | Get product by name |
| `POST` | `/api/Product` | Create new product |
| `PUT` | `/api/Product/{id}` | Update product |
| `DELETE` | `/api/Product/{id}` | Delete product |

### 🗂️ Categories (`/api/Category`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Category/GetAllCategory` | Get all categories |
| `GET` | `/api/Category/GetById/{id}` | Get category by ID |
| `GET` | `/api/Category/GetByName/{name}` | Get category by name |
| `POST` | `/api/Category` | Create category |
| `PUT` | `/api/Category/{id}` | Update category |
| `DELETE` | `/api/Category/{id}` | Delete category |

### 🛒 Shopping Cart (`/api/ShoppingCart`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/ShoppingCart` | Get user's cart |
| `POST` | `/api/ShoppingCart` | Add item to cart |
| `PUT` | `/api/ShoppingCart/{productId}` | Update item quantity |
| `DELETE` | `/api/ShoppingCart` | Remove item from cart |
| `POST` | `/api/ShoppingCart/clear` | Clear entire cart |

### 📋 Orders (`/api/Order`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Order` | Get all orders |
| `GET` | `/api/Order/{id}` | Get order by ID |
| `GET` | `/api/Order/Users/{userId}` | Get orders by user |
| `POST` | `/api/Order/Checkout` | Create order (checkout) |
| `POST` | `/api/Order/cancel/{id}` | Cancel order |
| `PUT` | `/api/Order/status` | Update order status |
| `GET` | `/api/Order/status/{id}` | Get order status |
| `DELETE` | `/api/Order/{id}` | Delete order |

### ⭐ Feedback (`/api/FeedBack`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/FeedBack` | Get all feedback |
| `POST` | `/api/FeedBack` | Submit feedback |
| `PUT` | `/api/FeedBack/{id}` | Update feedback |
| `DELETE` | `/api/FeedBack/{id}` | Delete feedback |

### 🔑 Roles (`/api/Role`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/Role` | Get all roles |
| `POST` | `/api/Role` | Create new role |
| `POST` | `/api/Role/assign` | Assign role to user |

---

## 📝 Usage Examples

### Register a New User

```bash
curl -X POST https://localhost:7013/api/Auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "SecurePassword123!",
    "firstName": "John",
    "lastName": "Doe"
  }'
```

### Login

```bash
curl -X POST https://localhost:7013/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "SecurePassword123!"
  }'
```

### Get All Products

```bash
curl -X GET https://localhost:7013/api/Product \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

### Add to Cart

```bash
curl -X POST https://localhost:7013/api/ShoppingCart \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "productId": 1,
    "quantity": 2
  }'
```

### Checkout

```bash
curl -X POST https://localhost:7013/api/Order/Checkout \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "appUserId": 1,
    "shippingAddress": "123 Main St, City",
    "orderItems": [
      { "productId": 1, "quantity": 2 }
    ]
  }'
```

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch: `git checkout -b feature/amazing-feature`
3. **Commit** your changes: `git commit -m 'Add amazing feature'`
4. **Push** to the branch: `git push origin feature/amazing-feature`
5. **Open** a Pull Request

### Development Guidelines

- Follow C# coding conventions
- Write unit tests for new features
- Update documentation as needed
- Use meaningful commit messages

---
