using Microsoft.Data.SqlClient;
using OrderManagementSystem.Entity;
using OrderManagementSystem.Util;
using OrderManagementSystem.Exceptions;
using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Dao
{
    public class OrderProcessor : IOrderManagementRepository
    {
        private readonly string _connectionString;

        public OrderProcessor(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Create User
        public void CreateUser(User user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while creating user: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unexpected error while creating user: " + ex.Message);
            }
        }

        // Create Product
        public void CreateProduct(User user, Product product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Products (ProductName, Description, Price, QuantityInStock, Type) " +
                                   "VALUES (@ProductName, @Description, @Price, @QuantityInStock, @Type); " +
                                   "SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
                    command.Parameters.AddWithValue("@Type", product.Type);

                    connection.Open();
                    int productId = Convert.ToInt32(command.ExecuteScalar());

                    if (product.Type.ToLower() == "electronics")
                    {
                        query = "INSERT INTO Electronics (ProductId, Brand, WarrantyPeriod) VALUES (@ProductId, @Brand, @WarrantyPeriod)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@Brand", "ExampleBrand");
                        command.Parameters.AddWithValue("@WarrantyPeriod", 2);
                        command.ExecuteNonQuery();
                    }
                    else if (product.Type.ToLower() == "clothing")
                    {
                        query = "INSERT INTO Clothing (ProductId, Size, Color) VALUES (@ProductId, @Size, @Color)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@Size", "L");
                        command.Parameters.AddWithValue("@Color", "Red");
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while creating product: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unexpected error while creating product: " + ex.Message);
            }
        }

        // Create Order
        public void CreateOrder(User user, List<Product> products)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Check if user exists
                    string userCheckQuery = "SELECT COUNT(*) FROM Users WHERE UserId = @UserId";
                    SqlCommand userCheckCmd = new SqlCommand(userCheckQuery, connection);
                    userCheckCmd.Parameters.AddWithValue("@UserId", user.UserId);
                    int userExists = (int)userCheckCmd.ExecuteScalar();

                    if (userExists == 0)
                        throw new UserNotFoundException();

                    string query = "INSERT INTO Orders (UserId, OrderDate) VALUES (@UserId, @OrderDate); SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@OrderDate", DateTime.Now);

                    int orderId = Convert.ToInt32(command.ExecuteScalar());

                    foreach (var product in products)
                    {
                        query = "INSERT INTO OrderItems (OrderId, ProductId, Quantity) VALUES (@OrderId, @ProductId, @Quantity)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@ProductId", product.ProductId);
                        command.Parameters.AddWithValue("@Quantity", 1);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while creating order: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unexpected error while creating order: " + ex.Message);
            }
        }

        // Cancel Order
        public void CancelOrder(int userId, int orderId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Check if order exists
                    string checkQuery = "SELECT COUNT(*) FROM Orders WHERE OrderId = @OrderId AND UserId = @UserId";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                    checkCmd.Parameters.AddWithValue("@OrderId", orderId);
                    checkCmd.Parameters.AddWithValue("@UserId", userId);
                    int orderExists = (int)checkCmd.ExecuteScalar();

                    if (orderExists == 0)
                        throw new OrderNotFoundException();

                    string query = "DELETE FROM OrderItems WHERE OrderId = @OrderId; DELETE FROM Orders WHERE OrderId = @OrderId AND UserId = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@UserId", userId);

                    command.ExecuteNonQuery();
                }
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while canceling order: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unexpected error while canceling order: " + ex.Message);
            }
        }

        // Get All Products
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Products";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Description = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            QuantityInStock = reader.GetInt32(4),
                            Type = reader.GetString(5)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while fetching products: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unexpected error while fetching products: " + ex.Message);
            }
            return products;
        }

        // Get Orders by User
        public List<Product> GetOrderByUser(User user)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT p.ProductId, p.ProductName, p.Description, p.Price, p.QuantityInStock, p.Type " +
                                   "FROM Products p " +
                                   "JOIN OrderItems oi ON p.ProductId = oi.ProductId " +
                                   "JOIN Orders o ON oi.OrderId = o.OrderId " +
                                   "WHERE o.UserId = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", user.UserId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Description = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            QuantityInStock = reader.GetInt32(4),
                            Type = reader.GetString(5)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error while retrieving order by user: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unexpected error while retrieving order by user: " + ex.Message);
            }
            return products;
        }
    }
}
