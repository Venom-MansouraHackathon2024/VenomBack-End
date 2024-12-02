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
- **Testing**: xUnit  

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

## Development Setup

### Prerequisites
- .NET SDK  
- SQL Server  

### Installation

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/your-repo/ECommerceSystem.git
   cd ECommerceSystem



   
