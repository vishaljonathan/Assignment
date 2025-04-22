using OrderManagementSystem.Dao;
using OrderManagementSystem.Entity;
using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Main
{
    public class OrderManagement
    {
        private static OrderProcessor _orderProcessor;

        public static void Main(string[] args)
        {
            string configFile = "AppSettings.json";
            _orderProcessor = new OrderProcessor(configFile);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n---- Order Management System Menu ----");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Create Order");
                Console.WriteLine("4. Cancel Order");
                Console.WriteLine("5. Get All Products");
                Console.WriteLine("6. Get Order By User");
                Console.WriteLine("7. Exit");
                Console.Write("Please enter your choice (1-7): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateUser();
                            break;
                        case "2":
                            CreateProduct();
                            break;
                        case "3":
                            CreateOrder();
                            break;
                        case "4":
                            CancelOrder();
                            break;
                        case "5":
                            GetAllProducts();
                            break;
                        case "6":
                            GetOrderByUser();
                            break;
                        case "7":
                            Console.WriteLine("Exiting Order Management System...");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        private static void CreateUser()
        {
            Console.WriteLine("--- Create User ---");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Console.Write("Enter Role (e.g., Admin, Customer): ");
            string role = Console.ReadLine();

            User newUser = new User
            {
                Username = username,
                Password = password,
                Role = role
            };

            _orderProcessor.CreateUser(newUser);
            Console.WriteLine("User created successfully.");
        }

        private static void CreateProduct()
        {
            Console.WriteLine("--- Create Product ---");

            int userId;
            Console.Write("Enter User ID (creating the product): ");
            while (!int.TryParse(Console.ReadLine(), out userId) || userId <= 0)
            {
                Console.WriteLine("Invalid User ID. Please enter a positive number.");
            }

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Product Description: ");
            string description = Console.ReadLine();

            decimal price;
            Console.Write("Enter Product Price: ");
            while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
            {
                Console.WriteLine("Invalid price. Please enter a positive number.");
            }

            int quantity;
            Console.Write("Enter Quantity in Stock: ");
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid quantity. Please enter a non-negative number.");
            }

            Console.Write("Enter Product Type (e.g., Electronics, Clothing): ");
            string type = Console.ReadLine();

            // Additional fields for Electronics or Clothing
            int warrantyPeriod = 0; //It is 0 if no warranty is provided
            string brand = null;
            string size = null;
            string color = null;

            if (type.Equals("Electronics", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter Warranty Period (number of years): ");
                while (!int.TryParse(Console.ReadLine(), out warrantyPeriod) || warrantyPeriod <= 0)
                {
                    Console.WriteLine("Invalid Warranty Period. Please enter a positive number.");
                }

                Console.Write("Enter Brand: ");
                brand = Console.ReadLine();
            }
            else if (type.Equals("Clothing", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter Size (e.g., S, M, L, XL): ");
                size = Console.ReadLine();

                Console.Write("Enter Color: ");
                color = Console.ReadLine();
            }

            Product product = new Product
            {
                ProductName = name,
                Description = description,
                Price = price,
                QuantityInStock = quantity,
                Type = type,
                WarrantyPeriod = warrantyPeriod, 
                Brand = brand,                   
                Size = size,                     
                Color = color                    
            };

            User user = new User { UserId = userId };

            _orderProcessor.CreateProduct(user, product);
            Console.WriteLine("Product created successfully.");
        }

        private static void CreateOrder()
        {
            Console.WriteLine("--- Create Order ---");

            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            List<Product> orderedProducts = new List<Product>();

            while (true)
            {
                Console.Write("Enter Product ID to add to order (or type 'done' to finish): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "done")
                    break;

                if (!int.TryParse(input, out int productId))
                {
                    Console.WriteLine("Invalid Product ID. Please enter a number.");
                    continue;
                }

                orderedProducts.Add(new Product { ProductId = productId });
            }

            if (orderedProducts.Count == 0)
            {
                Console.WriteLine("No products added to the order.");
                return;
            }

            User user = new User { UserId = userId };
            _orderProcessor.CreateOrder(user, orderedProducts);
            Console.WriteLine("Order created successfully.");
        }

        private static void CancelOrder()
        {
            Console.WriteLine("--- Cancel Order ---");
            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Order ID: ");
            int orderId = Convert.ToInt32(Console.ReadLine());

            _orderProcessor.CancelOrder(userId, orderId);
            Console.WriteLine("Order canceled successfully.");
        }

        private static void GetAllProducts()
        {
            Console.WriteLine("--- All Products ---");
            List<Product> products = _orderProcessor.GetAllProducts();

            if (products.Count == 0)
            {
                Console.WriteLine("No products available.");
                return;
            }

            foreach (var p in products)
            {
                Console.WriteLine($"ID: {p.ProductId}, Name: {p.ProductName}, Price: {p.Price}, Quantity: {p.QuantityInStock}, Type: {p.Type}");
            }
        }

        private static void GetOrderByUser()
        {
            Console.WriteLine("--- Get Orders By User ---");
            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            List<Product> orders = _orderProcessor.GetOrderByUser(new User { UserId = userId });

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found for this user.");
                return;
            }

            foreach (var product in orders)
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.Price}, Quantity: {product.QuantityInStock}");
            }
        }
    }
}
