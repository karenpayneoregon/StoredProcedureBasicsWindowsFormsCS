namespace SpecialReadStoredProcedureExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.GetCustomerByIdentifierButton = new System.Windows.Forms.Button();
            this.GetAllCustomersButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GetCustomerByCompanyNameButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GetCustomerByCompanyNameButton);
            this.panel1.Controls.Add(this.GetCustomerByIdentifierButton);
            this.panel1.Controls.Add(this.GetAllCustomersButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 328);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 55);
            this.panel1.TabIndex = 0;
            // 
            // GetCustomerByIdentifierButton
            // 
            this.GetCustomerByIdentifierButton.Location = new System.Drawing.Point(158, 15);
            this.GetCustomerByIdentifierButton.Name = "GetCustomerByIdentifierButton";
            this.GetCustomerByIdentifierButton.Size = new System.Drawing.Size(121, 23);
            this.GetCustomerByIdentifierButton.TabIndex = 1;
            this.GetCustomerByIdentifierButton.Text = "Get by Identifier";
            this.GetCustomerByIdentifierButton.UseVisualStyleBackColor = true;
            this.GetCustomerByIdentifierButton.Click += new System.EventHandler(this.GetCustomerByIdentifierButton_Click);
            // 
            // GetAllCustomersButton
            // 
            this.GetAllCustomersButton.Location = new System.Drawing.Point(21, 15);
            this.GetAllCustomersButton.Name = "GetAllCustomersButton";
            this.GetAllCustomersButton.Size = new System.Drawing.Size(121, 23);
            this.GetAllCustomersButton.TabIndex = 0;
            this.GetAllCustomersButton.Text = "Get All";
            this.GetAllCustomersButton.UseVisualStyleBackColor = true;
            this.GetAllCustomersButton.Click += new System.EventHandler(this.GetAllCustomersButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(662, 328);
            this.dataGridView1.TabIndex = 1;
            // 
            // GetCustomerByCompanyNameButton
            // 
            this.GetCustomerByCompanyNameButton.Location = new System.Drawing.Point(294, 15);
            this.GetCustomerByCompanyNameButton.Name = "GetCustomerByCompanyNameButton";
            this.GetCustomerByCompanyNameButton.Size = new System.Drawing.Size(121, 23);
            this.GetCustomerByCompanyNameButton.TabIndex = 2;
            this.GetCustomerByCompanyNameButton.Text = "Get by company name";
            this.GetCustomerByCompanyNameButton.UseVisualStyleBackColor = true;
            this.GetCustomerByCompanyNameButton.Click += new System.EventHandler(this.GetCustomerByCompanyNameButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 383);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multi-purpose reader";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button GetAllCustomersButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button GetCustomerByIdentifierButton;
        private System.Windows.Forms.Button GetCustomerByCompanyNameButton;
    }
}

