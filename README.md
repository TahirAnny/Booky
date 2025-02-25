# Booky

**A full-featured .NET 8 e-commerce platform for book sales.**

## 🚀 Project Overview

Booky is an ASP.NET Core MVC project that serves as an online bookstore. This project includes user authentication, product management, shopping cart functionality, and secure payment processing.

## ✅ Features

### Core Features
- User authentication & role-based access
- Product management (CRUD operations)
- Shopping cart & checkout functionality
- Payment gateway integration
- Category & cover type management
- Order processing & tracking

### Error Handling & Logging
- Global exception handling with `IExceptionHandler`
- Structured logging with **Serilog** (In-progress)

## 🛠 Tech Stack

| Technology      | Description  |
|----------------|-------------|
| **.NET 8**    | Backend framework |
| **Entity Framework Core** | ORM for database interactions |
| **SQL Server** | Database |
| **Stripe** | Payment gateway |
<!--| **Serilog** | Logging framework |
| **Redis** | Caching | -->

## 🔧 Installation & Setup

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/your-repo/Booky.git
cd Booky
```

### 2️⃣ Configure Database
- Update `appsettings.json` with your **SQL Server** connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=Booky;Trusted_Connection=True;"
}
```

- Run database migrations:
```sh
dotnet ef database update
```

### 3️⃣ Install Dependencies
```sh
dotnet restore
```

### 4️⃣ Run the Project
```sh
dotnet run
```
Visit: `http://localhost:5000`

## 🛡 Exception Handling & Logging

- Global error handling with `IExceptionHandler`.
<!--- Logging setup using **Serilog**, storing logs in a file.

📌 **Serilog Configuration in `Program.cs`**
```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/app-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

 ## 🧪 Unit Testing with xUnit & Moq

- **Controllers & Services** tested using `xUnit`.
- Mock database interactions with `Moq`.

📌 **Run Tests**
```sh
dotnet test
```
 -->

## 💳 Payment Integration with Stripe

- The project uses **Stripe** for handling payments securely.
- Configure your Stripe keys in `appsettings.json`:
```json
"Stripe": {
  "PublishableKey": "your-publishable-key",
  "SecretKey": "your-secret-key"
}
```
- Install Stripe package:
```sh
dotnet add package Stripe.net
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Submit a Pull Request

## 📜 License
This project is licensed under the **MIT License**.

