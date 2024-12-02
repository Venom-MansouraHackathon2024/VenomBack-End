to try endpoints check this link 
http://venom.runasp.net/swagger/index.html

# Cretec Marketplace System

## Project Overview

The **Cretec Marketplace System** is a backend application designed to manage an online marketplace. Built using **Clean Architecture**, it ensures a clear separation of concerns, maintainability, and scalability. The system supports multiple roles:

- **Admin**: Manages users, products, and orders.  
- **Supplier**: Adds, updates, and deletes products.  
- **Customer**: Browses products, adds items to the cart, places orders, and writes reviews.  

## Technology Stack
- **Backend Framework**: C# .NET (ASP.NET Core)  
- **Database**: SQL Server  
- **Authentication**: JWT (JSON Web Token), OTP for two-factor authentication  
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, API)  

## Project Structure

### 1. Domain Layer (Core)
- Contains **Entities** and **Interfaces**.  
- Independent of other layers to ensure separation of concerns.  

### 2. Application Layer
- Contains the **Business Logic**, **DTOs**, and **Managers**.  
- Uses **AutoMapper** to map between entities and DTOs.  

### 3. Infrastructure Layer
- Manages interactions with external systems such as the database.  
- Contains **Repositories** for data access logic.  

### 4. API Layer
- Exposes **RESTful API** endpoints via **Controllers**.  

## API Documentation

### AuthController
- `POST /api/auth/register`: Registers a new user.  
- `POST /api/auth/login`: Logs in and returns a JWT token.  
- `POST /api/auth/send-otp`: Sends an OTP to the user for verification.  
- `POST /api/auth/verify-otp`: Verifies the OTP provided by the user.  
- `POST /api/auth/reset-password`: Allows users to reset their password using OTP.  

### ProductController
- `GET /api/products`: Retrieves all products.  
- `POST /api/products`: Adds a new product.  
- `PUT /api/products/{id}`: Updates an existing product.  
- `DELETE /api/products/{id}`: Deletes a product by ID.  

### OrderController
- `POST /api/orders`: Creates a new order.  
- `GET /api/orders/{id}`: Retrieves order details by ID.  
- `PATCH /api/orders/{id}/status`: Updates the status of an order.  

## Database Overview

### Key Tables
- **Users**: Stores user details and authentication data.  
- **Products**: Contains product information such as name, price, and stock.  
- **Orders**: Manages customer orders and their statuses.  
- **OrderItems**: Tracks items within each order.  
- **Cart**: Represents the shopping cart linked to a user.  
- **CartItems**: Stores items added to the cart, including quantity and product references.  
- **Reviews**: Stores customer reviews and ratings for products.  
- **Suppliers**: Represents the suppliers who manage product listings.  


In the `appsettings.json` file, the following configurations are used:
 
```json
{

  "ConnectionStrings": {
    "DefaultConnection": "Server=db10767.public.databaseasp.net; Database=db10767; User Id=db10767; Password=dG-3?5EpR+t2; Encrypt=False;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "y9XQ!@324fkpq34Vn04i5#W6$%fTgQwErTgBhYtNmQqPzXqFjKl09",
    "Issuer": "https://localhost:7209/",
    "Audience": "https://localhost:7209/"
  },

  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "UserName": "moustafa.anafea@gmail.com",
    "Password": "wfsv vksy wosp mfaq"
  }
}
## Development Setup

### Prerequisites
- .NET SDK  
- SQL Server  

### Installation

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/your-repo/ECommerceSystem.git
   cd ECommerceSystem

2. **Install Dependencies:**
   ```bash
   Copy code

   Set Up the Database: Configure the connection string in appsettings.json to connect to your SQL Server database.

3. **Run the Application:**
   ```bash
   Copy code
   dotnet run
   The API will be available at http://localhost:5000.

## Features
- Authentication: JWT authentication for secure access.
- OTP for Verification: OTP-based two-factor authentication for enhanced security.
- Password Reset: Users can reset their passwords via OTP.
- Product Management: Admin and Supplier roles can add, update, and delete products.
- Order Management: Customers can place orders and track their status.
- Cart Management: Customers can add products to their cart and convert it into an order.




   
