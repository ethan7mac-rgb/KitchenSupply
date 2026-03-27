namespace Lab2KitchenSupply
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lstCustomers = new ListBox();
            cboOrders = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            tabNav = new TabControl();
            tabPageOrders = new TabPage();
            dgvOrderItems = new DataGridView();
            tabPageUpdateOrder = new TabPage();
            btnAddToOrder = new Button();
            label3 = new Label();
            groupBox1 = new GroupBox();
            radProduct = new RadioButton();
            radService = new RadioButton();
            cboProductService = new ComboBox();
            lblStat1Desc = new Label();
            lblStat1Value = new Label();
            lblStat2Value = new Label();
            lblStat2Desc = new Label();
            tabNav.SuspendLayout();
            tabPageOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrderItems).BeginInit();
            tabPageUpdateOrder.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lstCustomers
            // 
            lstCustomers.FormattingEnabled = true;
            lstCustomers.ItemHeight = 15;
            lstCustomers.Location = new Point(10, 24);
            lstCustomers.Margin = new Padding(3, 2, 3, 2);
            lstCustomers.Name = "lstCustomers";
            lstCustomers.Size = new Size(278, 139);
            lstCustomers.TabIndex = 0;
            lstCustomers.SelectedIndexChanged += lstCustomers_SelectedIndexChanged;
            // 
            // cboOrders
            // 
            cboOrders.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOrders.FormattingEnabled = true;
            cboOrders.Location = new Point(10, 199);
            cboOrders.Margin = new Padding(3, 2, 3, 2);
            cboOrders.Name = "cboOrders";
            cboOrders.Size = new Size(148, 23);
            cboOrders.TabIndex = 1;
            cboOrders.SelectedIndexChanged += cboOrders_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 7);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 2;
            label1.Text = "Customers:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 182);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 3;
            label2.Text = "Orders:";
            // 
            // tabNav
            // 
            tabNav.Controls.Add(tabPageOrders);
            tabNav.Controls.Add(tabPageUpdateOrder);
            tabNav.Location = new Point(311, 7);
            tabNav.Margin = new Padding(3, 2, 3, 2);
            tabNav.Name = "tabNav";
            tabNav.SelectedIndex = 0;
            tabNav.Size = new Size(591, 183);
            tabNav.TabIndex = 4;
            // 
            // tabPageOrders
            // 
            tabPageOrders.Controls.Add(dgvOrderItems);
            tabPageOrders.Location = new Point(4, 24);
            tabPageOrders.Margin = new Padding(3, 2, 3, 2);
            tabPageOrders.Name = "tabPageOrders";
            tabPageOrders.Padding = new Padding(3, 2, 3, 2);
            tabPageOrders.Size = new Size(583, 155);
            tabPageOrders.TabIndex = 0;
            tabPageOrders.Text = "Order Items";
            tabPageOrders.UseVisualStyleBackColor = true;
            // 
            // dgvOrderItems
            // 
            dgvOrderItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderItems.Location = new Point(6, 12);
            dgvOrderItems.Margin = new Padding(3, 2, 3, 2);
            dgvOrderItems.Name = "dgvOrderItems";
            dgvOrderItems.RowHeadersWidth = 51;
            dgvOrderItems.Size = new Size(573, 138);
            dgvOrderItems.TabIndex = 5;
            // 
            // tabPageUpdateOrder
            // 
            tabPageUpdateOrder.Controls.Add(btnAddToOrder);
            tabPageUpdateOrder.Controls.Add(label3);
            tabPageUpdateOrder.Controls.Add(groupBox1);
            tabPageUpdateOrder.Controls.Add(cboProductService);
            tabPageUpdateOrder.Location = new Point(4, 24);
            tabPageUpdateOrder.Margin = new Padding(3, 2, 3, 2);
            tabPageUpdateOrder.Name = "tabPageUpdateOrder";
            tabPageUpdateOrder.Padding = new Padding(3, 2, 3, 2);
            tabPageUpdateOrder.Size = new Size(583, 155);
            tabPageUpdateOrder.TabIndex = 1;
            tabPageUpdateOrder.Text = "Update Order";
            tabPageUpdateOrder.UseVisualStyleBackColor = true;
            // 
            // btnAddToOrder
            // 
            btnAddToOrder.Location = new Point(388, 29);
            btnAddToOrder.Margin = new Padding(3, 2, 3, 2);
            btnAddToOrder.Name = "btnAddToOrder";
            btnAddToOrder.Size = new Size(132, 52);
            btnAddToOrder.TabIndex = 5;
            btnAddToOrder.Text = "Add to Order";
            btnAddToOrder.UseVisualStyleBackColor = true;
            btnAddToOrder.Click += btnAddToOrder_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(186, 14);
            label3.Name = "label3";
            label3.Size = new Size(88, 15);
            label3.TabIndex = 4;
            label3.Text = "Select Offering:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radProduct);
            groupBox1.Controls.Add(radService);
            groupBox1.Location = new Point(15, 14);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(156, 94);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Select Type:";
            // 
            // radProduct
            // 
            radProduct.AutoSize = true;
            radProduct.Location = new Point(5, 20);
            radProduct.Margin = new Padding(3, 2, 3, 2);
            radProduct.Name = "radProduct";
            radProduct.Size = new Size(67, 19);
            radProduct.TabIndex = 0;
            radProduct.TabStop = true;
            radProduct.Text = "Product";
            radProduct.UseVisualStyleBackColor = true;
            radProduct.CheckedChanged += radProduct_CheckedChanged;
            // 
            // radService
            // 
            radService.AutoSize = true;
            radService.Location = new Point(6, 50);
            radService.Margin = new Padding(3, 2, 3, 2);
            radService.Name = "radService";
            radService.Size = new Size(62, 19);
            radService.TabIndex = 1;
            radService.TabStop = true;
            radService.Text = "Service";
            radService.UseVisualStyleBackColor = true;
            radService.CheckedChanged += radService_CheckedChanged;
            // 
            // cboProductService
            // 
            cboProductService.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProductService.FormattingEnabled = true;
            cboProductService.Location = new Point(186, 31);
            cboProductService.Margin = new Padding(3, 2, 3, 2);
            cboProductService.Name = "cboProductService";
            cboProductService.Size = new Size(196, 23);
            cboProductService.TabIndex = 2;
            // 
            // lblStat1Desc
            // 
            lblStat1Desc.AutoSize = true;
            lblStat1Desc.Location = new Point(320, 199);
            lblStat1Desc.Name = "lblStat1Desc";
            lblStat1Desc.Size = new Size(56, 15);
            lblStat1Desc.TabIndex = 5;
            lblStat1Desc.Text = "stat1desc";
            // 
            // lblStat1Value
            // 
            lblStat1Value.AutoSize = true;
            lblStat1Value.Location = new Point(320, 220);
            lblStat1Value.Name = "lblStat1Value";
            lblStat1Value.Size = new Size(60, 15);
            lblStat1Value.TabIndex = 6;
            lblStat1Value.Text = "stat1value";
            // 
            // lblStat2Value
            // 
            lblStat2Value.AutoSize = true;
            lblStat2Value.Location = new Point(561, 220);
            lblStat2Value.Name = "lblStat2Value";
            lblStat2Value.Size = new Size(60, 15);
            lblStat2Value.TabIndex = 8;
            lblStat2Value.Text = "stat2value";
            // 
            // lblStat2Desc
            // 
            lblStat2Desc.AutoSize = true;
            lblStat2Desc.Location = new Point(561, 199);
            lblStat2Desc.Name = "lblStat2Desc";
            lblStat2Desc.Size = new Size(56, 15);
            lblStat2Desc.TabIndex = 7;
            lblStat2Desc.Text = "stat2desc";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(906, 254);
            Controls.Add(lblStat2Value);
            Controls.Add(lblStat2Desc);
            Controls.Add(lblStat1Value);
            Controls.Add(lblStat1Desc);
            Controls.Add(tabNav);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cboOrders);
            Controls.Add(lstCustomers);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Kitchen Supply";
            Load += Form1_Load;
            tabNav.ResumeLayout(false);
            tabPageOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrderItems).EndInit();
            tabPageUpdateOrder.ResumeLayout(false);
            tabPageUpdateOrder.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstCustomers;
        private ComboBox cboOrders;
        private Label label1;
        private Label label2;
        private TabControl tabNav;
        private TabPage tabPageOrders;
        private DataGridView dgvOrderItems;
        private TabPage tabPageUpdateOrder;
        private Button btnAddToOrder;
        private Label label3;
        private GroupBox groupBox1;
        private RadioButton radProduct;
        private RadioButton radService;
        private ComboBox cboProductService;
        private Label lblStat1Desc;
        private Label lblStat1Value;
        private Label lblStat2Value;
        private Label lblStat2Desc;
    }
}
