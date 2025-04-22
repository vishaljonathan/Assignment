--Create Database
CREATE DATABASE OMS;

--Use the Database
USE OMS;

--Create User Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(100,1),
    Username VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    Role VARCHAR(50) NOT NULL
);

--Create Product Table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(200,1) NOT NULL,
    ProductName VARCHAR(100),
    Description VARCHAR(MAX),
    Price DECIMAL(10,2),
    QuantityInStock INT,
    Type VARCHAR(50)
);

--Create Electronics Table
CREATE TABLE Electronics (
    ProductId INT PRIMARY KEY,
    Brand VARCHAR(100),
    WarrantyPeriod INT,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

--Create Clothing Table
CREATE TABLE Clothing (
    ProductId INT PRIMARY KEY,
    Size VARCHAR(50),
    Color VARCHAR(50),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

--Create Orders Table
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(300,1),
    UserId INT,
    OrderDate DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

--Create OrderItems Table
CREATE TABLE OrderItems (
    OrderItemId INT PRIMARY KEY IDENTITY(400,1),
    OrderId INT,
    ProductId INT,
    Quantity INT,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
