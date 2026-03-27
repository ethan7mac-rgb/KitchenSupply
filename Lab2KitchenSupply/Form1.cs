using Lab2KitchenSupply.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;

namespace Lab2KitchenSupply
{
    public partial class Form1 : Form
    {
        //Global variable
        private DataAccess dataAccess;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Instantiating(Im not spell checking this) new DataAcess obj into our dataAcess global variable 
            dataAccess = new DataAccess();
            //Send in our connection string from App.Config into our dataAcess
            dataAccess.Connection = ConfigurationManager.ConnectionStrings["KitchenDB"].ConnectionString;
            //Get all customers and populate lstCustomers with each of them
            var customer = dataAccess.GetAllCustomers();
            foreach (Customer cust in customer)
            {
                lstCustomers.Items.Add(cust);
            }

        }

        private void lstCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear all the lbl and dgv (I also cleared the cbo even though the instructions didnt call for it)
            dgvOrderItems.DataSource = "";
            cboOrders.Items.Clear();
            lblStat1Value.Text = "";
            lblStat2Value.Text = "";
            lblStat1Desc.Text = "";
            lblStat2Desc.Text = "";
            //Pulls our customer obj from the SelectedItem in the lst 
            Customer selCust = (Customer)lstCustomers.SelectedItem;
            //Gets all the orders from the customer selected and add them to the cbo
            var custOrders = dataAccess.GetOrdersByCustomerId(selCust.CustomerID);
            foreach (Order order in custOrders)
            {
                cboOrders.Items.Add(order);
            }
            //Call DisplayStats send in our selected customer
            DisplayStats(selCust);
        }

        private void cboOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear all lbls and dgv and grab our selected order from the cbo
            Order selOrder = (Order)cboOrders.SelectedItem;
            dgvOrderItems.DataSource = dataAccess.GetOrderItemsByOrderId(selOrder.OrderID);
            lblStat1Value.Text = "";
            lblStat2Value.Text = "";
            lblStat1Desc.Text = "";
            lblStat2Desc.Text = "";
            //Call DisplayStats and send in our selected order
            DisplayStats(selOrder);
        }
        //Made a method for this since it didnt update if you clicked service first and it bothered me
        private void radProduct_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCBOProdServ();
        }
        private void radService_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCBOProdServ();
        }
        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
            //Making sure you have a Order and Product/Service selected
            if(cboOrders.SelectedIndex > -1)
            {
                if(cboProductService.SelectedIndex > -1)
                {
                    try
                    {
                        //Setting up values for our OrdItem that both a prod or serv would use
                        Order ord = (Order)cboOrders.SelectedItem;
                        OrderItem ordItem = new OrderItem();
                        ordItem.OrderID = ord.OrderID;
                        ordItem.Quantity = 1;
                        if (radProduct.Checked)
                        {
                            //If its a product grab the prodID, Price and set ServiceID to null
                            Product prod = (Product)cboProductService.SelectedItem;
                            ordItem.ProductID = prod.ProductID;
                            ordItem.ServiceID = null;
                            ordItem.Price = prod.Price;
                        }
                        else if (radService.Checked)
                        {
                            //If its a service grab the ServiceID, Price and set ProdcutID to null
                            Service serv = (Service)cboProductService.SelectedItem;
                            ordItem.ServiceID = serv.ServiceID;
                            ordItem.ProductID = null;
                            ordItem.Price = serv.Price;
                        }
                        //Id be shocked if this fired but added it just incase
                        else
                        {
                            MessageBox.Show("Error No Product or Service Selected");
                            return;
                        }
                        try
                        {
                            //Call AddOrderItemAndRecalculateTotal send in our ordItem we want to add
                            dataAccess.AddOrderItemAndRecalculateTotal(ordItem);
                            //Mbox on success
                            MessageBox.Show("Success! Order Added");
                            //Refreshing all our displays
                            dgvOrderItems.DataSource = dataAccess.GetOrderItemsByOrderId(ord.OrderID);
                            cboOrders.Items.Clear();
                            Customer cust = (Customer)lstCustomers.SelectedItem;
                            var custOrders = dataAccess.GetOrdersByCustomerId(cust.CustomerID);
                            foreach (Order order in custOrders)
                            {
                                cboOrders.Items.Add(order);
                            }
                            DisplayStats(ord);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Error Adding Order post ordItem creation");
                        }

                    }
                    //Catch any Exception that happens while creating ordItem
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error with adding order {ex}");
                    }
                    
                }
            }
        }

        //method for updating CBOProductService
        private void UpdateCBOProdServ()
        {
            if (radProduct.Checked)
            {
                cboProductService.Items.Clear();
                foreach (Product prod in dataAccess.GetAllProducts())
                {

                    cboProductService.Items.Add(prod);
                }
            }
            else if (radService.Checked)
            {
                cboProductService.Items.Clear();
                foreach (Service serv in dataAccess.GetAllServices())
                {

                    cboProductService.Items.Add(serv);
                }
            }
        }

        

        private void DisplayStats(Object obj)
        {
            //Type checking obj
            if (obj is Order o)
            {
                //grab all orders based on obj OrderID
                var orderItems = dataAccess.GetOrderItemsByOrderId(o.OrderID);
                //Set text of lblStat(1/2)
                lblStat1Desc.Text = "Total Items in Order:";
                lblStat2Desc.Text = "Number of Products in Order:";
                //Changes our lblStat2Value text to grab all disticnt ProductIDs and then count them
                lblStat2Value.Text = orderItems.Select(ord => ord.ProductID).Distinct().Count().ToString();
                //Changes our lblStat1Value text to the sum of quantity
                lblStat1Value.Text = orderItems.Sum(ord => ord.Quantity).ToString();
            }
            //Type checking obj
            else if (obj is Customer c)
            {
                //grab all orders based on obj CustomerID
                var customer = dataAccess.GetOrdersByCustomerId(c.CustomerID);
                //Change text in lblStat(1/2)Desc
                lblStat1Desc.Text = "Average Order Cost:";
                lblStat2Desc.Text = "Total Spent:";
                //Added a try catch to this to prevent a Exception from being thrown when a person with no orders was selected
                try
                {
                    //Changes our lblStat(1/2)Value text to the sum and average of TotalAmount 
                    lblStat2Value.Text = customer.Sum(cust => cust.TotalAmount).ToString("c");
                    lblStat1Value.Text = customer.Average(cust => cust.TotalAmount).ToString("c");
                }
                catch (Exception)
                {
                    //If an exception happens changes the lblStat(1/2)Value text to No Orders to prevent exception
                    lblStat1Value.Text = "No Orders";
                    lblStat2Value.Text = "No Orders";
                }
            }
            //If obj is neither obj clear the lbl text
            else
            {
                lblStat1Value.Text = "";
                lblStat2Value.Text = "";
                lblStat1Desc.Text = "";
                lblStat2Desc.Text = "";
            }
        }

        
    }
}
