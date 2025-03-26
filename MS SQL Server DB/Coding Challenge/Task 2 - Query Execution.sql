--Use the Database
USE Ecomm;

--Update the price of refregirator
UPDATE products SET price = 800.00 WHERE product_id = 7;

--Remove all cart items for a specific customer
DELETE FROM Cart WHERE customer_id = 5;

--Subquery to remove all cart items for a specific customer
DELETE FROM Cart  
WHERE customer_id = (SELECT customer_id FROM customers WHERE customer_id = 6); 

--Subquery without WHERE to remove all cart items for a specific customer
DELETE FROM Cart  
WHERE customer_id IN (SELECT customer_id FROM customers) AND customer_id = 6;  
SELECT *FROM cart;


--Retrieve products priced below 100
SELECT product_id AS Product_ID, name as Product_Name, description as Description, price AS Price FROM products
WHERE price<100;

--Products with stock quantity greater than 5
SELECT product_id AS Product_ID, name as Product_Name, stockQuantity as Stock_Quantity FROM products
WHERE stockQuantity>5;

--Orders with Total Amount Between $500 and $1000.
SELECT order_id AS Order_ID, total_price AS Total_Price FROM orders WHERE total_price > 500 AND total_price < 1000; 

--Products with names ending with the letter 'r'
SELECT product_id AS Product_ID, name as Name FROM Products WHERE name LIKE '%r';  

--Cart items for customer 5
SELECT *FROM cart WHERE Customer_id = 5;

--Customers who placed order in 2023
SELECT c.customer_id AS Customer_ID, c.name AS Name
FROM customers AS c  
JOIN orders AS o ON c.customer_id = o.Customer_Id  
WHERE YEAR(o.order_date) = 2023;

--Minimum stock quantity for each product category
SELECT product_id AS Product_ID, name as Product_Name, MIN(stockQuantity) AS Min_Stock_Quantity  
FROM products  
GROUP BY product_id, name;

--Total amount spent by each customer
SELECT c.customer_id AS Customer_ID, c.name AS Name, SUM(o.total_price) AS Total_Amount_Spent
FROM customers AS c
JOIN orders AS o ON c.customer_id = o.Customer_Id
GROUP BY c.customer_id, c.name;

--Average order amount for each customer
SELECT c.customer_id AS Customer_ID, c.name as Name, AVG(o.total_price) AS Avg_Order_Amount  
FROM customers AS c  
JOIN orders AS o ON c.customer_id = o.Customer_Id  
GROUP BY c.customer_id, c.name;

--Number of orders placed by each customer
SELECT c.customer_id AS Customer_ID, c.name AS Name, COUNT(o.order_id) AS Order_Count  
FROM customers AS c  
JOIN orders AS o ON c.customer_id = o.Customer_Id  
GROUP BY c.customer_id, c.name;

--Maximum order amount for each customer
SELECT c.customer_id AS Customer_ID, c.name AS Name, MAX(o.total_price) AS Maximum_Order_Amount 
FROM customers AS c  
JOIN orders AS o ON c.customer_id = o.Customer_Id  
GROUP BY c.customer_id, c.name;

--Customer who placed orders totaling over 1000
SELECT c.customer_id AS Customer_ID, c.name AS Name, SUM(o.total_price) AS Total_Amount_Spent  
FROM customers AS c  
JOIN orders AS o ON c.customer_id = o.Customer_Id  
GROUP BY c.customer_id, c.name  
HAVING SUM(o.total_price) > 1000;

--Subquery to find products not in the cart
SELECT product_id AS Product_ID, name AS Name, price as Price FROM products  
WHERE product_id NOT IN (SELECT DISTINCT product_id FROM cart);

--Subquery to find customers who haven't placed orders
SELECT customer_id AS Customer_ID, name AS Name FROM customers
WHERE customer_id NOT IN(SELECT DISTINCT Customer_Id FROM orders); 

--Subquery to calculate the percentage of total revenue for a product
SELECT p.product_id, p.name,  
       (SUM(oi.quantity * p.price) * 100) /  
       (SELECT SUM(oi.quantity * p.price) FROM order_items AS oi  
        JOIN products p ON oi.Product_Id = p.product_id) AS Revenue_Percentage  
FROM products AS p  
JOIN order_items oi ON p.product_id = oi.Product_Id  
GROUP BY p.product_id, p.name;

--Subquery to find products with low stock
SELECT product_id AS Product_ID, name AS Name FROM products  
WHERE stockQuantity < (SELECT AVG(stockQuantity) FROM products);

--Subquery to find customers who placed high valued orders
SELECT customer_id AS Customer_ID, name AS Name FROM customers  
WHERE customer_id IN (  
    SELECT DISTINCT customer_id  
    FROM orders  
    WHERE total_price > (SELECT AVG(total_price) FROM orders)  
);


