namespace WindowsFormsApp1
{
	public partial class CustomerEditorForm : System.Windows.Forms.Form
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
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.cboContactTitles = new System.Windows.Forms.ComboBox();
			this.txtContactName = new System.Windows.Forms.TextBox();
			this.txtCompanyName = new System.Windows.Forms.TextBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(9, 28);
			this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(80, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Company name";
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(17, 55);
			this.Label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(73, 13);
			this.Label2.TabIndex = 2;
			this.Label2.Text = "Contact name";
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(27, 80);
			this.Label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(63, 13);
			this.Label3.TabIndex = 4;
			this.Label3.Text = "Contact title";
			//
			//cboContactTitles
			//
			this.cboContactTitles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboContactTitles.FormattingEnabled = true;
			this.cboContactTitles.Location = new System.Drawing.Point(93, 77);
			this.cboContactTitles.Margin = new System.Windows.Forms.Padding(2);
			this.cboContactTitles.Name = "cboContactTitles";
			this.cboContactTitles.Size = new System.Drawing.Size(192, 21);
			this.cboContactTitles.TabIndex = 5;
			//
			//txtContactName
			//
			this.txtContactName.Location = new System.Drawing.Point(93, 54);
			this.txtContactName.Margin = new System.Windows.Forms.Padding(2);
			this.txtContactName.Name = "txtContactName";
			this.txtContactName.Size = new System.Drawing.Size(192, 20);
			this.txtContactName.TabIndex = 3;
			//
			//txtCompanyName
			//
			this.txtCompanyName.Location = new System.Drawing.Point(93, 28);
			this.txtCompanyName.Margin = new System.Windows.Forms.Padding(2);
			this.txtCompanyName.Name = "txtCompanyName";
			this.txtCompanyName.Size = new System.Drawing.Size(192, 20);
			this.txtCompanyName.TabIndex = 1;
			//
			//cmdSave
			//
			this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdSave.Location = new System.Drawing.Point(20, 128);
			this.cmdSave.Margin = new System.Windows.Forms.Padding(2);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Size = new System.Drawing.Size(74, 33);
			this.cmdSave.TabIndex = 6;
			this.cmdSave.Text = "Save";
			this.cmdSave.UseVisualStyleBackColor = true;
			//
			//cmdCancel
			//
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(210, 128);
			this.cmdCancel.Margin = new System.Windows.Forms.Padding(2);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(74, 33);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			//
			//CustomerEditorForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(307, 179);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdSave);
			this.Controls.Add(this.txtCompanyName);
			this.Controls.Add(this.txtContactName);
			this.Controls.Add(this.cboContactTitles);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.Label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "CustomerEditorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Customer Form";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.ComboBox cboContactTitles;
		internal System.Windows.Forms.TextBox txtContactName;
		internal System.Windows.Forms.TextBox txtCompanyName;
		internal System.Windows.Forms.Button cmdSave;
		internal System.Windows.Forms.Button cmdCancel;
	}

}