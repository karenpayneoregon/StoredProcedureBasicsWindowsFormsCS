using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using WindowsFormsApp1;
using WindowsFormsApp1.Special.My;
using DataOperations;
using DataOperations.TypeClasses;
using ExtensionsLibrary;


namespace WindowsApplication1
{
	public partial class MainForm
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private BackendOperations dataOperations = new BackendOperations();
		private BindingSource bsCustomers = new BindingSource();
		/// <summary>
		/// Insert a new row
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks></remarks>
		private void NewCustomerButton_Click(object sender, EventArgs e)
		{

			if (bsCustomers.DataSource == null)
			{
				MessageBox.Show("Please select some data");
				return;
			}


			CustomerEditorForm f = new CustomerEditorForm();
			try
			{
				f.cboContactTitles.DataSource = dataOperations.RetrieveContactTitles();
				if (f.ShowDialog() == DialogResult.OK)
				{
					if (f.ValidateTextBoxes())
					{

						var contactTypeIdentifier = ((ContactTypes)f.cboContactTitles.SelectedItem).Identifier;
						int primaryKey = dataOperations.AddCustomer(f.txtCompanyName.Text, f.txtContactName.Text, contactTypeIdentifier);

						if (primaryKey != -1)
						{
							bsCustomers.AddRow(primaryKey, f.txtCompanyName.Text, f.txtContactName.Text, f.cboContactTitles.Text, contactTypeIdentifier);
						}
					}
					else
					{
						MessageBox.Show("Nothing save as one or more fields were empty.");
					}

				}
			}
			finally
			{
				f.Dispose();
			}
		}

		/// <summary>
		/// Remove the current row
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks></remarks>
		private void RemoveCurrentCustomerButton_Click(object sender, EventArgs e)
		{

			if (bsCustomers.DataSource == null)
			{
				MessageBox.Show("Please select some data");
				return;
			}

			if (KarenDialogs.Question("Remove " + bsCustomers.CurrentRow().CompanyName()))
			{
				if (!dataOperations.RemoveCustomer(bsCustomers.CurrentRow().Identifier()))
				{
					if (!dataOperations.IsSuccessFul)
					{
						MessageBox.Show($"Failed to remove customer{Environment.NewLine}{dataOperations.LastExceptionMessage}");
					}
				}
				else
				{
					//
					// Only remove row if removed successfully from the database table
					//
					bsCustomers.RemoveCurrent();

				}
			}

		}
		private void MainForm_Shown(object sender, EventArgs e)
		{
			LoadCustomers();
		}
		private void LoadCustomers()
		{

			var customerDataTable = dataOperations.RetrieveAllRecords();
			var contactList = dataOperations.RetrieveContactTitles();

			if (dataOperations.IsSuccessFul)
			{

				bsCustomers.DataSource = customerDataTable;
				bsCustomers.Sort = "CompanyName";

				DataGridView1.DataSource = bsCustomers;
				DataGridView1.ExpandColumns();

				bsCustomers.MoveFirst();

				ContactTypeComboBox.DataSource = contactList;

			}
			else
			{
				MessageBox.Show($"Failed to load data{Environment.NewLine}{dataOperations.LastExceptionMessage}");
			}
		}
		/// <summary>
		/// Select rows where contact type is equal to current item
		/// in cboContactTitles
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks></remarks>
		private void ReadByContactTypeButton_Click(object sender, EventArgs e)
		{

			var contactTypeIdentifier = ((ContactTypes)ContactTypeComboBox.SelectedItem).Identifier;

			var dt = dataOperations.GetAllRecordsByContactTitle(contactTypeIdentifier);

			if (dataOperations.IsSuccessFul)
			{
				bsCustomers.DataSource = dt;
				DataGridView1.DataSource = bsCustomers;
				bsCustomers.MoveFirst();
			}
			else
			{
				bsCustomers.DataSource = null;
				DataGridView1.DataSource = bsCustomers;
			}

			bsCustomers.DataSource = dt;
			DataGridView1.DataSource = bsCustomers;

			bsCustomers.MoveFirst();
		}
		/// <summary>
		/// For either
		/// a) Data has changed in the table outside of this code sample
		/// b) The Where/filter was executed which does not return contact type
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReloadCustomersFromDatabaseButton_Click(object sender, EventArgs e)
		{
			LoadCustomers();
		}
		/// <summary>
		/// Update current record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks></remarks>
		private void UpdateCurrentCustomerButton_Click(object sender, EventArgs e)
		{
			EditCurrentCustomer();
		}
		private void DataGridView1_DoubleClick(object sender, EventArgs e)
		{
			if (bsCustomers.Current != null)
			{
				EditCurrentCustomer();
			}
		}
		private void EditCurrentCustomer()
		{
			if (bsCustomers.DataSource == null)
			{
				MessageBox.Show("Please select some data");
				return;
			}

			var contactList = dataOperations.RetrieveContactTitles();

			CustomerEditorForm f = new CustomerEditorForm();
			try
			{

				f.txtCompanyName.Text = bsCustomers.CurrentRow().CompanyName();
				f.txtContactName.Text = bsCustomers.CurrentRow().ContactName();
				f.cboContactTitles.DataSource = contactList;

				f.cboContactTitles.SelectedIndex = contactList.FindIndex((contactType) => contactType.ContactType == bsCustomers.CurrentRow().ContactTitle());

				if (f.ShowDialog() == DialogResult.OK)
				{
					if (f.ValidateTextBoxes())
					{

						var customerIdentifier = bsCustomers.CurrentRow().Identifier();
						var contactTypeIdentifier = ((ContactTypes)f.cboContactTitles.SelectedItem).Identifier;

						if (dataOperations.UpdateCustomer(customerIdentifier, f.txtCompanyName.Text, f.txtContactName.Text, contactTypeIdentifier))
						{
							bsCustomers.CurrentRow().SetCompanyName(f.txtCompanyName.Text);
							bsCustomers.CurrentRow().SetContactName(f.txtContactName.Text);
							bsCustomers.CurrentRow().SetContactTitle(f.cboContactTitles.Text);
						}
					}
					else
					{
						MessageBox.Show("Nothing save as one or more fields were empty.");
					}

				}
			}
			finally
			{
				f.Dispose();
			}
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			dataOperations.ReturnErrorInformation();

		}

		private static MainForm _DefaultInstance;
		public static MainForm DefaultInstance
		{
			get
			{
				if (_DefaultInstance == null || _DefaultInstance.IsDisposed)
					_DefaultInstance = new MainForm();

				return _DefaultInstance;
			}
		}
	}

}