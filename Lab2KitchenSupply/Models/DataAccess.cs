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
        //Prop for the connection string
        public string Connection { get; set; }
        //Returns a list of all the customers in the DB
        public List<Customer> GetAllCustomers()
        {
            //Declaring a list of customers
            List<Customer> customers = new List<Customer>();
            try
            {
                //Opening our connection
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
                //Building a data table using the query results
                string query = "SELECT CustomerID, FirstName, LastName FROM Customers";
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                //Making a customer using every row in our DT
                foreach (DataRow row in dataTable.Rows)
                {
                    customers.Add(new Customer
                    {
                        CustomerID = Convert.ToInt32(row["CustomerID"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString()
                    });
                }
                //Return the list
                return customers;
            }
            //If an error occurs returns empty list and error mbox
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetAllCustomers() {ex}");
                return customers;
            }
        }
        //Returns a list of all products in the db
        public List<Product> GetAllProducts()
        {
            //Declare a list of products
            List<Product> products = new List<Product>();
            try 
            { 
                //Opening our connection
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
                //Buildinga data table using our query results
                string query = "SELECT * FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                //Make a product for each row in the DT

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
                //Return a list of products
                return products;
            }
            //If an error occurs returns empty list and error mbox
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetAllProducts() {ex}");
                return products;
            }
        }
        //Returns a list of all services in the DB
        public List<Service> GetAllServices()
        {
            //Declare a list of services
            List<Service> services = new List<Service>();
            try 
            { 
                //Opening our connection
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();

                //Building a dt based on our query (Now realizing I probably should have made this a method)
                string query = "SELECT * FROM Services";
                SqlCommand command = new SqlCommand(query, connection);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                //Make a new servie for each row in the DT
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
                //return a list of services
                return services;
            }
            //If an error occurs returns empty list and error mbox
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetAllServcies() {ex}");
                return services;
            }
        }
        //Returns a list of all data from orders that matches the customerID sent in
        public List<Order> GetOrdersByCustomerId(int customerID)
        {
            //Declare a list of orders
            List<Order> orders = new List<Order>();
            try
            {
                //Opening our connection
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();

                //Building a parameterized query using the customerID sent into the method 
                string query = @"SELECT * FROM Orders WHERE CustomerID = @CustID";
                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter custID = new SqlParameter("@CustID", customerID);
                command.Parameters.Add(custID);
                //Building a DT based on our query
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                
                //Make a new order for each row in the DT
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
                //Return a list of orders
                return orders;
            }
            //If an error occurs returns empty list and error mbox
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetOrdersByCustomerID {ex}");
                return orders;
            }
        }
        //Gets all OrderItems data matching the orderID
        public List<OrderItem> GetOrderItemsByOrderId(int orderID)
        {
            //Declare a list of orderItems
            List<OrderItem> orderItems = new List<OrderItem>();
            try
            {
                //Open our connection
                using SqlConnection connection = new SqlConnection(Connection);
                connection.Open();
                //Parameterized query taking in orderid
                string query = @"SELECT * FROM OrderItems WHERE OrderID = @OrderID";
                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter ordID = new SqlParameter("@OrderID", orderID);
                command.Parameters.Add(ordID);
                //Building a DT based on the query
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());

                
                //Make a new OrderItem for each row in the DT
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
                //Return a list of OrderItems
                return orderItems;
            }
            //If an error occurs returns empty list and error mbox
            catch (Exception ex)
            {
                MessageBox.Show($"Error with GetOrderItemsByOrderID {ex}");
                return orderItems;
            }
        }

        public void AddOrderItemAndRecalculateTotal(OrderItem ordI)
        {
            //Opening our connection
            using SqlConnection connection = new SqlConnection(Connection);
            connection.Open();
            //Declaring two strings for null check
            string prodID, servID;
            //Checking if eiether is null and setting the actual string to read NULL so when we insert into the DB the value will read NULL
            if (ordI.ProductID is null)
                prodID = "NULL";
            else
                prodID = ordI.ProductID;
            if (ordI.ServiceID is null)
                servID = "NULL";
            else
                servID = ordI.ServiceID;
            
            //Didnt use a parameterized query for our query since there is no way for the user to enter text this should be entirely safe 
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
