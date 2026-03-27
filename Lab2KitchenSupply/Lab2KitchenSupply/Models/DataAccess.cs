using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lab2KitchenSupply.Models
{
    public class DataAccess
    {
        public string Connection { get; set; }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
                string query = "SELECT CustomerID, FirstName, LastName FROM Customers";
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                
                foreach (DataRow row in dataTable.Rows)
                {
                    customers.Add(new Customer
                    {
                        CustomerID = Convert.ToInt32(row["CustomerID"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString()
                    });
                }
                return customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetAllCustomers() {ex}");
                return customers;
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try 
            { 
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
                string query = "SELECT * FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());


                

                foreach (DataRow row in dataTable.Rows)
                {
                    products.Add(new Product
                    {
                        ProductID = row["ProductID"].ToString(),
                        ProductName = row["ProductName"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        QuantityInStock = Convert.ToInt32(row["QuantityInStock"]),
                        Description = row["Description"].ToString()
                    });
                }
                return products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetAllProducts() {ex}");
                return products;
            }
        }

        public List<Service> GetAllServices()
        {
            List<Service> services = new List<Service>();
            try 
            { 
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
                string query = "SELECT * FROM Services";
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                
                foreach (DataRow row in dataTable.Rows)
                {
                    services.Add(new Service
                    {
                        ServiceID = row["ServiceID"].ToString(),
                        ServiceName = row["ServiceName"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Description = Convert.ToString(row["Description"])
                    });
                }
                return services;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetAllServcies() {ex}");
                return services;
            }
        }
        
        public List<Order> GetOrdersByCustomerId(int customerID)
        {
            List<Order> orders = new List<Order>();
            try
            {
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
                string query = @"SELECT * FROM Orders WHERE CustomerID = @CustID";

                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter custID = new SqlParameter("@CustID", customerID);
                command.Parameters.Add(custID);

                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                

                foreach (DataRow row in dataTable.Rows)
                {
                    orders.Add(new Order
                    {
                        OrderID = Convert.ToInt32(row["OrderID"]),
                        CustomerID = Convert.ToInt32(row["CustomerID"]),
                        OrderDate = Convert.ToDateTime(row["OrderDate"]),
                        TotalAmount = Convert.ToDecimal(row["TotalAmount"])
                    });
                }

                return orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetOrdersByCustomerID {ex}");
                return orders;
            }
        }

        public List<OrderItem> GetOrderItemsByOrderId(int orderID)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            try
            {
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
                string query = @"SELECT * FROM OrderItems WHERE OrderID = @OrderID";

                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter ordID = new SqlParameter("@OrderID", orderID);
                command.Parameters.Add(ordID);

                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                

                foreach (DataRow row in dataTable.Rows)
                {

                    orderItems.Add(new OrderItem
                    {
                        LineID = Convert.ToInt32(row["LineID"]),
                        OrderID = Convert.ToInt32(row["OrderID"]),
                        ProductID = row["ProductID"].ToString(),
                        ServiceID = row["ServiceID"].ToString(),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        Price = Convert.ToDecimal(row["Price"])
                    });
                }

                return orderItems;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error with GetOrderItemsByOrderID {ex}");
                return orderItems;
            }
        }

        public void AddOrderItemAndRecalculateTotal(OrderItem ordI)
        {
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
            //Insert OrderItem into the OrderItems table
            string prodID, servID;
            if (ordI.ProductID is null)
                prodID = "NULL";
            else
                prodID = ordI.ProductID;
            if (ordI.ServiceID is null)
                servID = "NULL";
            else
                servID = ordI.ServiceID;
            
                
            SqlCommand command = new SqlCommand(@$"INSERT INTO OrderItems (OrderID, ProductID, ServiceID, Quantity, Price)
                                                    VALUES ({ordI.OrderID},{prodID},{servID},{ordI.Quantity},{ordI.Price})",connection);
                command.ExecuteNonQuery();
                //Recalculate the TotalAmount for the OrderID
                SqlCommand commandRecaulc = new SqlCommand(@$"SELECT SUM(Price * Quantity) FROM OrderItems WHERE OrderID = {ordI.OrderID}",connection);
                decimal updatedTotal = (decimal)commandRecaulc.ExecuteScalar();

                //Update Orders with new total
                SqlCommand commandUpdateOrd = new SqlCommand(@$"UPDATE Orders SET TotalAmount = {updatedTotal} WHERE OrderID = {ordI.OrderID}",connection);
                commandUpdateOrd.ExecuteNonQuery();

                //If its a product update the products table with the new quantity
                /*if(ordI.ProductID is not null)
                {
                    using SqlCommand updateStockCmd = new SqlCommand(@$"UPDATE Products SET QuantityInStock = QuantityInStock - {ordI.Quantity} WHERE ProductID = {ordI.ProductID}", connection);
                }*/
        }


    }
}
