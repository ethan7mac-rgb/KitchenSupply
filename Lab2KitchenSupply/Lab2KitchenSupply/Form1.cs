using Lab2KitchenSupply.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;

namespace Lab2KitchenSupply
{
    public partial class Form1 : Form
    {
        private DataAccess dataAccess;
        private List<Product> prodList;
        private List<Service> servList;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataAccess = new DataAccess();
            dataAccess.Connection = ConfigurationManager.ConnectionStrings["KitchenDB"].ConnectionString;
            prodList = dataAccess.GetAllProducts();
            servList = dataAccess.GetAllServices();

            var customer = dataAccess.GetAllCustomers();
            foreach (Customer cust in customer)
            {
                lstCustomers.Items.Add(cust);
            }

        }

        private void lstCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOrderItems.DataSource = "";
            cboOrders.Items.Clear();
            lblStat1Value.Text = "";
            lblStat2Value.Text = "";
            lblStat1Desc.Text = "";
            lblStat2Desc.Text = "";
            Customer cust = (Customer)lstCustomers.SelectedItem;
            var custOrders = dataAccess.GetOrdersByCustomerId(cust.CustomerID);
            foreach (Order order in custOrders)
            {
                cboOrders.Items.Add(order);
            }

            DisplayStats(cust);
        }

        private void cboOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Order selOrder = (Order)cboOrders.SelectedItem;
            dgvOrderItems.DataSource = dataAccess.GetOrderItemsByOrderId(selOrder.OrderID);
            lblStat1Value.Text = "";
            lblStat2Value.Text = "";
            lblStat1Desc.Text = "";
            lblStat2Desc.Text = "";
            DisplayStats(selOrder);
        }
        //Made a method for this since it didnt update if you clicked service first
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
            if(cboOrders.SelectedIndex > -1)
            {
                if(cboProductService.SelectedIndex > -1)
                {
                    try
                    {
                        Order ord = (Order)cboOrders.SelectedItem;
                        OrderItem ordItem = new OrderItem();
                        ordItem.OrderID = ord.OrderID;
                        ordItem.Quantity = 1;
                        if (radProduct.Checked)
                        {
                            Product prod = (Product)cboProductService.SelectedItem;
                            ordItem.ProductID = prod.ProductID;
                            ordItem.ServiceID = null;
                            ordItem.Price = prod.Price;
                        }
                        else if (radService.Checked)
                        {
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
                            dataAccess.AddOrderItemAndRecalculateTotal(ordItem);
                            MessageBox.Show("Success! Order Added");
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
                            MessageBox.Show("Error Adding Order");
                        }

                    }
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
            if (obj is Order o)
            {
                var orderItems = dataAccess.GetOrderItemsByOrderId(o.OrderID);

                lblStat1Desc.Text = "Total Items in Order:";
                lblStat1Value.Text = orderItems.Sum(ord => ord.Quantity).ToString();

                lblStat2Desc.Text = "Number of Products in Order:";
                lblStat2Value.Text = orderItems.Select(ord => ord.ProductID).Distinct().Count().ToString();
            }
            else if (obj is Customer c)
            {
                var customer = dataAccess.GetOrdersByCustomerId(c.CustomerID);
                
                lblStat1Desc.Text = "Average Order Cost:";
                lblStat2Desc.Text = "Total Spent:";
                try
                {
                    lblStat2Value.Text = customer.Sum(cust => cust.TotalAmount).ToString("c");
                    lblStat1Value.Text = customer.Average(cust => cust.TotalAmount).ToString("c");
                }
                catch (Exception)
                {
                    lblStat1Value.Text = "No Orders";
                    lblStat2Value.Text = "No Orders";
                }
            }
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
