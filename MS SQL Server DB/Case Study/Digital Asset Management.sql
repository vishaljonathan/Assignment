--Create a DB for Asset Management System
CREATE DATABASE DAMS;

--Use the created database
USE DAMS;

--Create Employee Table
CREATE TABLE employees(
employee_id INT IDENTITY(100,1) PRIMARY KEY NOT NULL,
name VARCHAR(max),
department VARCHAR(max),
email VARCHAR(max),
password VARCHAR(max)
);

--Insert Values into Employee Table
INSERT INTO employees (name, department, email, password) VALUES
('John Doe', 'Procurement', 'john.doe@example.com', 'johndoe123'),
('Jane Smith', 'Maintenance', 'jane.smith@example.com', 'janesmith456'),
('Emily Davis', 'Finance & Budgeting', 'emily.davis@example.com', 'emilydavis789'),
('David Brown', 'IT Asset Management', 'david.brown@example.com', 'davidbrown123'),
('Alice Johnson', 'Inventory Control', 'alice.johnson@example.com', 'alicejohnson101'),
('Michael Wilson', 'IT Asset Management', 'michael.wilson@example.com', 'michaelwilson202'),
('Sarah Lee', 'Logistics', 'sarah.lee@example.com', 'sarahlee303'),
('James White', 'Compliance & Auditing', 'james.white@example.com', 'jameswhite404'),
('Robert Miller', 'Finance & Budgeting', 'robert.miller@example.com', 'robertmiller505'),
('Linda Taylor', 'Disposal & Recycling', 'linda.taylor@example.com', 'lindataylor606');

--Display Employee Table
SELECT *FROM employees;

--Create Asset Table
CREATE TABLE assets(
asset_id INT IDENTITY(200,1) PRIMARY KEY NOT NULL,
name VARCHAR(max),
type VARCHAR(max),
serial_number BIGINT,
purchase_date DATE,
location VARCHAR(max),
status VARCHAR(max),
owner_id INT,
CONSTRAINT FK_Ownerid FOREIGN KEY (owner_id) REFERENCES employees(employee_id)
);

--Insert values into Asset Table
INSERT INTO assets (name, type, serial_number, purchase_date, location, status, owner_id) VALUES
('Laptop', 'Laptop', '1001234567', '2024-01-10', 'Office', 'In Use', 100),
('Vehicle', 'Vehicle', '2009876543', '2023-12-01', 'Parking Lot', 'Under Maintenance', 101),
('Printer', 'Equipment', '3005678912', '2024-02-15', 'Office', 'In Use', 102),
('Projector', 'Equipment', '4003456789', '2024-03-01', 'Conference Room', 'Decommissioned', 103),
('Tablet', 'Laptop', '5008765432', '2023-11-01', 'Desk', 'In Use', 104),
('Monitor', 'Equipment', '6002345678', '2024-02-10', 'Meeting Room', 'In Use', 105),
('Camera', 'Equipment', '7006543210', '2023-09-01', 'Storage Room', 'In Use', 106),
('Laptop', 'Laptop', '8009871234', '2024-03-20', 'Office', 'In Use', 107),
('Smartphone', 'Laptop', '9006789123', '2023-05-10', 'Desk', 'Under Maintenance', 108),
('Speaker', 'Equipment', '1012345678', '2023-12-05', 'Office', 'Decommissioned', 109);

--Display Asset Table
SELECT *FROM assets;

--Create Maintenance Record Table
CREATE TABLE maintenance_record(
maintenance_id INT IDENTITY(1000,1) PRIMARY KEY NOT NULL,
Asset_ID INT,
CONSTRAINT FK_AssetID FOREIGN KEY (Asset_ID) REFERENCES assets(asset_id),
maintenance_date DATE,
description VARCHAR(max),
cost INT
);

--Insert values into Maintenance Record Table
INSERT INTO maintenance_record (asset_id, maintenance_date, description, cost) VALUES
(201, '2024-03-15', 'Vehicle servicing and oil change', 150),
(202, '2024-02-20', 'Printer toner replacement', 50),
(205, '2024-02-28', 'Monitor calibration and checkup', 30),
(208, '2024-03-05', 'Smartphone battery replacement', 60),
(203, '2024-03-10', 'Projector lens cleaning', 40),
(204, '2024-01-18', 'Tablet software update', 25),
(206, '2023-11-22', 'Camera firmware update', 35),
(200, '2024-03-12', 'Laptop software optimization', 45),
(209, '2023-12-15', 'Speaker sound system check', 20),
(207, '2024-01-25', 'Laptop hardware inspection', 100);

--Display Maintenance Record Table
SELECT *FROM maintenance_record;

--Create Asset Allocation Table
CREATE TABLE asset_allocation(
allocation_id INT IDENTITY(2000,1) PRIMARY KEY NOT NULL,
Asset_id INT,
CONSTRAINT FK_Asset FOREIGN KEY (Asset_id) REFERENCES assets(asset_id),
Employee_ID INT,
CONSTRAINT FK_EmployeeID FOREIGN KEY (Employee_ID) REFERENCES employees(employee_id),
allocation_date DATE,
return_date DATE
);

--Insert values into Asset Allocation Table
INSERT INTO asset_allocation (asset_id, employee_id, allocation_date, return_date) VALUES
(200, 100, '2024-01-10', NULL),
(201, 101, '2024-01-15', '2024-03-01'),
(202, 102, '2024-02-15', NULL),
(203, 103, '2024-03-01', '2024-03-10'),
(204, 104, '2024-01-10', NULL),
(205, 105, '2024-02-10', NULL),
(206, 106, '2023-09-01', NULL),
(207, 107, '2024-03-20', NULL),
(208, 108, '2023-05-10', '2024-02-28'),
(209, 109, '2023-12-05', NULL);

--Display Asset Allocation Table
SELECT *FROM asset_allocation;

--Create Reservations Table
CREATE TABLE reservations(
reservation_id INT IDENTITY(3000,1) PRIMARY KEY NOT NULL,
Asset_iD INT,
CONSTRAINT FK_Asset_ID FOREIGN KEY (Asset_iD) REFERENCES assets(asset_id),
Employee_id INT,
CONSTRAINT FK_Employee_ID FOREIGN KEY (Employee_id) REFERENCES employees(employee_id),
reservation_date DATE,
start_date DATE,
end_date DATE,
status VARCHAR(50)
);

--Insert values into Reservations Table
INSERT INTO reservations (asset_id, employee_id, reservation_date, start_date, end_date, status) VALUES
(200, 100, '2024-01-05', '2024-01-10', '2024-01-15', 'Approved'),
(201, 101, '2024-03-01', '2024-03-05', '2024-03-10', 'Pending'),
(202, 102, '2024-02-10', '2024-02-15', '2024-02-20', 'Approved'),
(203, 103, '2024-02-20', '2024-02-25', '2024-03-01', 'Canceled'),
(204, 104, '2024-01-10', '2024-01-15', '2024-01-20', 'Approved'),
(205, 105, '2024-02-05', '2024-02-10', '2024-02-15', 'Pending'),
(206, 106, '2023-08-15', '2023-09-01', '2023-09-10', 'Approved'),
(207, 107, '2024-03-18', '2024-03-20', '2024-03-25', 'Pending'),
(208, 108, '2024-02-20', '2024-02-25', '2024-03-01', 'Approved'),
(209, 109, '2023-11-30', '2023-12-05', '2023-12-10', 'Approved');

--Display Reservations Table
SELECT *FROM reservations;

--Display all tables
SELECT *FROM employees;
SELECT *FROM assets;
SELECT *FROM maintenance_record;
SELECT *FROM asset_allocation;
SELECT *FROM reservations;