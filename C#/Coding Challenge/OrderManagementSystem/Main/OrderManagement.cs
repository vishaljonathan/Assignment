using OrderManagementSystem.Dao;
using OrderManagementSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Main
{
    public class OrderManagement
    {
        private static OrderProcessor _orderProcessor;

        public static void Main(string[] args)
        {
            string configFile = "AppSettings.json";
            _orderProcessor = new OrderProcessor(configFile);

            while (true)
            {
                Console.WriteLine("\n---- Order Management System Menu ---");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Cancel Order");
                Console.WriteLine("4. Get All Products");
                Console.WriteLine("5. Get Order By User");
                Console.WriteLine("6. Exit");
                Console.Write("Please enter your choice (1-6): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            CreateUser();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in CreateUser: " + ex.Message);
                        }
                        break;
                    case "2":
                        try
                        {
                            CreateProduct();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in CreateProduct: " + ex.Message);
                        }
                        break;
                    case "3":
                        try
                        {
                            CancelOrder();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in CancelOrder: " + ex.Message);
                        }
                        break;
                    case "4":
                        try
                        {
                            GetAllProducts();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in GetAllProducts: " + ex.Message);
                        }
                        break;
                    case "5":
                        try
                        {
                            GetOrderByUser();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in GetOrderByUser: " + ex.Message);
                        }
                        break;
                    case "6":
                        Console.WriteLine("Exiting Order Management System.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void CreateUser()
        {
            Console.WriteLine("\n--- Create User ---");
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
            Console.WriteLine("\n--- Create Product ---");

            Console.Write("Enter User ID (creating the product): ");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();
            Console.Write("Enter Product Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Product Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter Quantity in Stock: ");
            int quantityInStock = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Product Type (e.g., Electronics, Clothing): ");
            string type = Console.ReadLine();

            User user = new User { UserId = userId };
            Product newProduct = new Product
            {
                ProductName = productName,
                Description = description,
                Price = price,
                QuantityInStock = quantityInStock,
                Type = type
            };

            _orderProcessor.CreateProduct(user, newProduct);
            Console.WriteLine("Product created successfully.");
        }

        private static void CancelOrder()
        {
            Console.WriteLine("\n--- Cancel Order ---");
            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Order ID: ");
            int orderId = Convert.ToInt32(Console.ReadLine());

            _orderProcessor.CancelOrder(userId, orderId);
            Console.WriteLine("Order canceled successfully.");
        }

        private static void GetAllProducts()
        {
            Console.WriteLine("\n--- Get All Products ---");
            List<Product> products = _orderProcessor.GetAllProducts();

            Console.WriteLine("List of Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.ProductName}, Price: {product.Price}, Quantity: {product.QuantityInStock}, Type: {product.Type}");
            }
        }

        private static void GetOrderByUser()
        {
            Console.WriteLine("\n--- Get Orders By User ---");
            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            List<Product> products = _orderProcessor.GetOrderByUser(new User { UserId = userId });

            Console.WriteLine("User's Orders:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductId}, Product Name: {product.ProductName}, Price: {product.Price}, Quantity: {product.QuantityInStock}");
            }
        }
    }
}
