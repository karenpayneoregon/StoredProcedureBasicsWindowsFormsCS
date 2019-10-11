namespace StoredProcedureExaminations
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.GetStoredProcDetailsButton = new System.Windows.Forms.Button();
            this.StoredProcedureNamesListBox = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.StoredProcedureDefinitionTextBox = new System.Windows.Forms.TextBox();
            this.ParameterListView = new System.Windows.Forms.ListView();
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SystemTypeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaxLengthColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrecisionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScaleColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.StoredProcedureNamesListBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 450);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.GetStoredProcDetailsButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 387);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 63);
            this.panel2.TabIndex = 1;
            // 
            // GetStoredProcDetailsButton
            // 
            this.GetStoredProcDetailsButton.Location = new System.Drawing.Point(12, 17);
            this.GetStoredProcDetailsButton.Name = "GetStoredProcDetailsButton";
            this.GetStoredProcDetailsButton.Size = new System.Drawing.Size(190, 23);
            this.GetStoredProcDetailsButton.TabIndex = 0;
            this.GetStoredProcDetailsButton.Text = "Get details";
            this.GetStoredProcDetailsButton.UseVisualStyleBackColor = true;
            this.GetStoredProcDetailsButton.Click += new System.EventHandler(this.GetStoredProcDetailsButton_Click);
            // 
            // StoredProcedureNamesListBox
            // 
            this.StoredProcedureNamesListBox.BackColor = System.Drawing.Color.Black;
            this.StoredProcedureNamesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StoredProcedureNamesListBox.ForeColor = System.Drawing.Color.White;
            this.StoredProcedureNamesListBox.FormattingEnabled = true;
            this.StoredProcedureNamesListBox.Location = new System.Drawing.Point(0, 0);
            this.StoredProcedureNamesListBox.Name = "StoredProcedureNamesListBox";
            this.StoredProcedureNamesListBox.Size = new System.Drawing.Size(208, 450);
            this.StoredProcedureNamesListBox.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(208, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.StoredProcedureDefinitionTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ParameterListView);
            this.splitContainer1.Size = new System.Drawing.Size(592, 450);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // StoredProcedureDefinitionTextBox
            // 
            this.StoredProcedureDefinitionTextBox.BackColor = System.Drawing.Color.Black;
            this.StoredProcedureDefinitionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StoredProcedureDefinitionTextBox.ForeColor = System.Drawing.Color.White;
            this.StoredProcedureDefinitionTextBox.Location = new System.Drawing.Point(0, 0);
            this.StoredProcedureDefinitionTextBox.Multiline = true;
            this.StoredProcedureDefinitionTextBox.Name = "StoredProcedureDefinitionTextBox";
            this.StoredProcedureDefinitionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.StoredProcedureDefinitionTextBox.Size = new System.Drawing.Size(592, 200);
            this.StoredProcedureDefinitionTextBox.TabIndex = 0;
            // 
            // ParameterListView
            // 
            this.ParameterListView.BackColor = System.Drawing.Color.Black;
            this.ParameterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.SystemTypeColumn,
            this.MaxLengthColumn,
            this.PrecisionColumn,
            this.ScaleColumn});
            this.ParameterListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParameterListView.ForeColor = System.Drawing.Color.White;
            this.ParameterListView.FullRowSelect = true;
            this.ParameterListView.Location = new System.Drawing.Point(0, 0);
            this.ParameterListView.Name = "ParameterListView";
            this.ParameterListView.Size = new System.Drawing.Size(592, 242);
            this.ParameterListView.TabIndex = 0;
            this.ParameterListView.UseCompatibleStateImageBehavior = false;
            this.ParameterListView.View = System.Windows.Forms.View.Details;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            // 
            // SystemTypeColumn
            // 
            this.SystemTypeColumn.Text = "System Type";
            this.SystemTypeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MaxLengthColumn
            // 
            this.MaxLengthColumn.Text = "Max length";
            this.MaxLengthColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PrecisionColumn
            // 
            this.PrecisionColumn.Text = "Precision";
            this.PrecisionColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ScaleColumn
            // 
            this.ScaleColumn.Text = "Scale";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get Stored Procedure definitions";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox StoredProcedureNamesListBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox StoredProcedureDefinitionTextBox;
        private System.Windows.Forms.ListView ParameterListView;
        private System.Windows.Forms.Button GetStoredProcDetailsButton;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader SystemTypeColumn;
        private System.Windows.Forms.ColumnHeader MaxLengthColumn;
        private System.Windows.Forms.ColumnHeader PrecisionColumn;
        private System.Windows.Forms.ColumnHeader ScaleColumn;
    }
}

