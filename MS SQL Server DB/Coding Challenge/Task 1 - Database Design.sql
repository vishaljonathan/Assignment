--Create Database
CREATE DATABASE Ecomm;

--Use the database
USE Ecomm;

--Create Customer Table
CREATE TABLE customers(
customer_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
name VARCHAR(max),
email VARCHAR(max),
password VARCHAR(max)
);

--Insert Values into the Customer Table
INSERT INTO customers (name, email, password) VALUES
('John Doe', 'johndoe@example.com', 'johndoe123'),
('Jane Smith','janesmith@example.com','janesmith@234'),
('Robert Johnson', 'robert@example.com', 'robert*123'),
('Sarah Brown', 'sarah@example.com', 'sarah$123'),
('David Lee', 'david@example.com', 'davidlee345'),
('Laura Hall', 'laura@example.com', 'laurahall&123'),
('Michael Davis', 'michael@example.com', 'michael987'),
('Emma Wilson', 'emma@example.com', 'emma@123'),
('William Taylor', 'william@example.com', 'william*123'),
('Olivia Adams', 'olivia@example.com', 'olivia@456');

--Display Customer Table
SELECT *FROM customers;

--Create Product Table
CREATE TABLE products(
product_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
name VARCHAR(max),
price DECIMAL(10,2),
description VARCHAR(max),
stockQuantity INT
);

--Insert values into Product Table
INSERT INTO products(name, description, price, stockQuantity) VALUES
('Laptop', 'High Performance Laptop', 800.00, 10),
('Smartphone', 'Latest Smartphone', 600.00, 15),
('Tablet', 'Portable Tablet', 300.00, 20),
('Headphones', 'Noise-Canceling', 150.00, 30),
('TV', '4K Smart TV', 900.00, 5),
('Coffee Maker', 'Automatic Coffee Maker', 50.00, 25),
('Refregirator', 'Energy-efficient', 700.00, 10),
('Microwave Oven', 'Countertop Microwave', 80.00, 15),
('Blender', 'High-speed Blender', 70.00, 20),
('Vacuum Cleaner', 'Bagless Vacuum Cleaner', 120.00, 10);

--Display Product Table
SELECT *FROM products;

--Create Cart Table
CREATE TABLE cart(
cart_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Customer_id INT,
CONSTRAINT FK_customerid FOREIGN KEY(Customer_id) REFERENCES customers(customer_id),
Product_id INT,
CONSTRAINT FK_productid FOREIGN KEY(Product_id) REFERENCES products(product_id),
quantity INT
);

--Insert values into Cart Table
INSERT INTO cart(Customer_id, Product_id, quantity) VALUES
(1, 1, 2),
(1, 3, 1),
(2, 2, 3),
(3, 4, 4),
(3, 5, 2),
(4, 6, 1),
(5, 1, 1),
(6, 10, 2),
(6, 9, 3),
(7, 7, 2);

--Display Cart Table
SELECT *FROM cart;

--Create Orders Table
CREATE TABLE orders(
order_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Customer_Id INT,
CONSTRAINT FK_Customer_ID FOREIGN KEY(Customer_Id) REFERENCES customers(customer_id),
order_date DATE,
total_price DECIMAL(10,2),
shipping_address VARCHAR(max)
);

--Insert values into Orders Table
INSERT INTO orders(Customer_Id, order_date, total_price, shipping_address) VALUES
(1, '2023-01-05', 1200.00, '123 Main St, City'),
(2, '2023-02-10', 900.00, '456 Elm St, Town'),
(3, '2023-03-15', 300.00, '789 Oak St, Village'),
(4, '2023-04-20', 150.00, '101 Pine St, Suburb'),
(5, '2023-05-25', 1800.00, '234 Cedar St, District'),
(6, '2023-06-30', 400.00, '567 Birch St, County'),
(7, '2023-07-05', 700.00, '890 Maple St, State'),
(8, '2023-08-10', 160.00, '321 Redwood St, Country'),
(9, '2023-09-15', 140.00, '432 Spruce St, Province'),
(10, '2023-10-20', 1400.00, '765 Fir St, Territory');

--Display Orders Table
SELECT *FROM orders;

--Create Order Items Table
CREATE TABLE order_items(
order_item_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Order_Id INT,
CONSTRAINT FK_orderid FOREIGN KEY(Order_Id) REFERENCES orders(order_id),
Product_Id INT,
CONSTRAINT FK_product_id FOREIGN KEY(Product_Id) REFERENCES products(product_id),
quantity INT,
itemAmount DECIMAL(10,2)
);

--Insert values into Order Items Table
INSERT INTO order_items(Order_Id, Product_Id, quantity, itemAmount) VALUES
(1, 1, 2, 1600.00),
(1, 3, 1, 300.00),
(2, 2, 3, 1800.00),
(3, 5, 2, 1800.00),
(4, 4, 4, 600.00),
(4, 6, 1, 50.00),
(5, 1, 1, 800.00),
(5, 2, 2, 1200.00),
(6, 10, 2, 240.00),
(6, 9, 3, 210.00);

--Display Order Items Table
SELECT *FROM order_items;

--Display all the table
SELECT *FROM customers;
SELECT *FROM products;
SELECT *FROM cart;
SELECT *FROM orders;
SELECT *FROM order_items;