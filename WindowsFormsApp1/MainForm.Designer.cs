using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.NewCustomerButton = new System.Windows.Forms.Button();
            this.ReadByContactTypeButton = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ReloadCustomersFromDatabaseButton = new System.Windows.Forms.Button();
            this.ContactTypeComboBox = new System.Windows.Forms.ComboBox();
            this.RemoveCurrentCustomerButton = new System.Windows.Forms.Button();
            this.UpdateCurrentCustomerButton = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // NewCustomerButton
            // 
            this.NewCustomerButton.Location = new System.Drawing.Point(269, 22);
            this.NewCustomerButton.Margin = new System.Windows.Forms.Padding(2);
            this.NewCustomerButton.Name = "NewCustomerButton";
            this.NewCustomerButton.Size = new System.Drawing.Size(125, 23);
            this.NewCustomerButton.TabIndex = 2;
            this.NewCustomerButton.Text = "New Customer";
            this.NewCustomerButton.UseVisualStyleBackColor = true;
            this.NewCustomerButton.Click += new System.EventHandler(this.NewCustomerButton_Click);
            // 
            // ReadByContactTypeButton
            // 
            this.ReadByContactTypeButton.Location = new System.Drawing.Point(269, 80);
            this.ReadByContactTypeButton.Margin = new System.Windows.Forms.Padding(2);
            this.ReadByContactTypeButton.Name = "ReadByContactTypeButton";
            this.ReadByContactTypeButton.Size = new System.Drawing.Size(119, 23);
            this.ReadByContactTypeButton.TabIndex = 5;
            this.ReadByContactTypeButton.Text = "By contact type";
            this.ReadByContactTypeButton.UseVisualStyleBackColor = true;
            this.ReadByContactTypeButton.Visible = false;
            this.ReadByContactTypeButton.Click += new System.EventHandler(this.ReadByContactTypeButton_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.ExitButton);
            this.Panel1.Controls.Add(this.ReloadCustomersFromDatabaseButton);
            this.Panel1.Controls.Add(this.ContactTypeComboBox);
            this.Panel1.Controls.Add(this.RemoveCurrentCustomerButton);
            this.Panel1.Controls.Add(this.UpdateCurrentCustomerButton);
            this.Panel1.Controls.Add(this.NewCustomerButton);
            this.Panel1.Controls.Add(this.ReadByContactTypeButton);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 284);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(536, 114);
            this.Panel1.TabIndex = 1;
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(403, 80);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(119, 23);
            this.ExitButton.TabIndex = 6;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ReloadCustomersFromDatabaseButton
            // 
            this.ReloadCustomersFromDatabaseButton.Location = new System.Drawing.Point(13, 22);
            this.ReloadCustomersFromDatabaseButton.Margin = new System.Windows.Forms.Padding(2);
            this.ReloadCustomersFromDatabaseButton.Name = "ReloadCustomersFromDatabaseButton";
            this.ReloadCustomersFromDatabaseButton.Size = new System.Drawing.Size(119, 23);
            this.ReloadCustomersFromDatabaseButton.TabIndex = 0;
            this.ReloadCustomersFromDatabaseButton.Text = "Reload from database";
            this.ReloadCustomersFromDatabaseButton.UseVisualStyleBackColor = true;
            this.ReloadCustomersFromDatabaseButton.Click += new System.EventHandler(this.ReloadCustomersFromDatabaseButton_Click);
            // 
            // ContactTypeComboBox
            // 
            this.ContactTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ContactTypeComboBox.FormattingEnabled = true;
            this.ContactTypeComboBox.Location = new System.Drawing.Point(11, 82);
            this.ContactTypeComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.ContactTypeComboBox.Name = "ContactTypeComboBox";
            this.ContactTypeComboBox.Size = new System.Drawing.Size(248, 21);
            this.ContactTypeComboBox.TabIndex = 4;
            this.ContactTypeComboBox.Visible = false;
            // 
            // RemoveCurrentCustomerButton
            // 
            this.RemoveCurrentCustomerButton.Location = new System.Drawing.Point(403, 22);
            this.RemoveCurrentCustomerButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveCurrentCustomerButton.Name = "RemoveCurrentCustomerButton";
            this.RemoveCurrentCustomerButton.Size = new System.Drawing.Size(119, 23);
            this.RemoveCurrentCustomerButton.TabIndex = 3;
            this.RemoveCurrentCustomerButton.Text = "Remove Current";
            this.RemoveCurrentCustomerButton.UseVisualStyleBackColor = true;
            this.RemoveCurrentCustomerButton.Click += new System.EventHandler(this.RemoveCurrentCustomerButton_Click);
            // 
            // UpdateCurrentCustomerButton
            // 
            this.UpdateCurrentCustomerButton.Location = new System.Drawing.Point(141, 22);
            this.UpdateCurrentCustomerButton.Margin = new System.Windows.Forms.Padding(2);
            this.UpdateCurrentCustomerButton.Name = "UpdateCurrentCustomerButton";
            this.UpdateCurrentCustomerButton.Size = new System.Drawing.Size(119, 23);
            this.UpdateCurrentCustomerButton.TabIndex = 1;
            this.UpdateCurrentCustomerButton.Text = "Update Current";
            this.UpdateCurrentCustomerButton.UseVisualStyleBackColor = true;
            this.UpdateCurrentCustomerButton.Click += new System.EventHandler(this.UpdateCurrentCustomerButton_Click);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView1.Location = new System.Drawing.Point(0, 0);
            this.DataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            this.DataGridView1.RowTemplate.Height = 24;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Size = new System.Drawing.Size(536, 284);
            this.DataGridView1.TabIndex = 0;
            this.DataGridView1.DoubleClick += new System.EventHandler(this.DataGridView1_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 398);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Working with Stored Procedures";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Button NewCustomerButton;
		internal System.Windows.Forms.Button ReadByContactTypeButton;
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.DataGridView DataGridView1;
		internal System.Windows.Forms.Button UpdateCurrentCustomerButton;
		internal System.Windows.Forms.Button RemoveCurrentCustomerButton;
		internal System.Windows.Forms.ComboBox ContactTypeComboBox;
		internal System.Windows.Forms.Button ReloadCustomersFromDatabaseButton;
		internal Button ExitButton;
	}

}