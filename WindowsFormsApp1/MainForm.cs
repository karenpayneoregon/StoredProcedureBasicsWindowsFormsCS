using System;
using System.Windows.Forms;
using DataOperations;
using DataOperations.TypeClasses;
using ExtensionsLibrary;
using static WindowsFormsApp1.Special.My.KarenDialogs;


namespace WindowsFormsApp1
{
    /// <summary>
    /// * ComboBox and button at bottom work but not needed at this time so they are hidden.
    /// </summary>
	public partial class MainForm
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private readonly BackendOperations _dataOperations = new BackendOperations();
		private readonly BindingSource _bsCustomers = new BindingSource();
		/// <summary>
		/// Insert a new row
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks></remarks>
		private void NewCustomerButton_Click(object sender, EventArgs e)
		{

			if (_bsCustomers.DataSource == null)
			{
				MessageBox.Show("Please select some data");
				return;
			}


			CustomerEditorForm f = new CustomerEditorForm();
			try
			{
				f.cboContactTitles.DataSource = _dataOperations.RetrieveContactTitles();

				if (f.ShowDialog() == DialogResult.OK)
				{
                    var contactTypeIdentifier = ((ContactTypes)f.cboContactTitles.SelectedItem).Identifier;
                    int primaryKey = _dataOperations.AddCustomer(f.CompanyNameTextBox.Text, f.ContactNameTextBox.Text, contactTypeIdentifier);

                    if (primaryKey != -1)
                    {
                        _bsCustomers.AddRow(
                            primaryKey, 
                            f.CompanyNameTextBox.Text, 
                            f.ContactNameTextBox.Text, 
                            f.cboContactTitles.Text, 
                            contactTypeIdentifier);
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

			if (_bsCustomers.DataSource == null)
			{
				MessageBox.Show("Please select some data");
				return;
			}

			if (Question("Remove " + _bsCustomers.CurrentRow().CompanyName()))
			{
				if (!_dataOperations.RemoveCustomer(_bsCustomers.CurrentRow().Identifier()))
				{
					if (!_dataOperations.IsSuccessFul)
					{
						MessageBox.Show($"Failed to remove customer{Environment.NewLine}{_dataOperations.LastExceptionMessage}");
					}
				}
				else
				{
					//
					// Only remove row if removed successfully from the database table
					//
					_bsCustomers.RemoveCurrent();

				}
			}

		}
		private void MainForm_Shown(object sender, EventArgs e)
		{
			LoadCustomers();
		}
		private void LoadCustomers()
		{

			var customerDataTable = _dataOperations.RetrieveAllCustomerRecords();
			var contactList = _dataOperations.RetrieveContactTitles();

			if (_dataOperations.IsSuccessFul)
			{

				_bsCustomers.DataSource = customerDataTable;
				_bsCustomers.Sort = "CompanyName";

				DataGridView1.DataSource = _bsCustomers;
				DataGridView1.ExpandColumns();

				_bsCustomers.MoveFirst();

				ContactTypeComboBox.DataSource = contactList;

			}
			else
			{
				MessageBox.Show($"Failed to load data\n{_dataOperations.LastExceptionMessage}");
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

			var dt = _dataOperations.GetAllRecordsByContactTitle(contactTypeIdentifier);

			if (_dataOperations.IsSuccessFul)
			{
				_bsCustomers.DataSource = dt;
				DataGridView1.DataSource = _bsCustomers;
				_bsCustomers.MoveFirst();
			}
			else
			{
				_bsCustomers.DataSource = null;
				DataGridView1.DataSource = _bsCustomers;
			}

			_bsCustomers.DataSource = dt;
			DataGridView1.DataSource = _bsCustomers;

			_bsCustomers.MoveFirst();

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
			if (_bsCustomers.Current != null)
			{
				EditCurrentCustomer();
			}
		}
		private void EditCurrentCustomer()
		{
			if (_bsCustomers.DataSource == null)
			{
				MessageBox.Show("Please select some data");
				return;
			}

			var contactList = _dataOperations.RetrieveContactTitles();

			CustomerEditorForm f = new CustomerEditorForm();
			try
			{

				f.CompanyNameTextBox.Text = _bsCustomers.CurrentRow().CompanyName();
				f.ContactNameTextBox.Text = _bsCustomers.CurrentRow().ContactName();
				f.cboContactTitles.DataSource = contactList;

				f.cboContactTitles.SelectedIndex = 
                    contactList.FindIndex((contactType) => contactType.ContactType == _bsCustomers.CurrentRow()
                                                               .ContactTitle());

				if (f.ShowDialog() == DialogResult.OK)
				{
					if (f.ValidateTextBoxes())
					{

						var customerIdentifier = _bsCustomers.CurrentRow().Identifier();
						var contactTypeIdentifier = ((ContactTypes)f.cboContactTitles.SelectedItem).Identifier;

						if (_dataOperations.UpdateCustomer(customerIdentifier, f.CompanyNameTextBox.Text, f.ContactNameTextBox.Text, contactTypeIdentifier))
						{
							_bsCustomers.CurrentRow().SetCompanyName(f.CompanyNameTextBox.Text);
							_bsCustomers.CurrentRow().SetContactName(f.ContactNameTextBox.Text);
							_bsCustomers.CurrentRow().SetContactTitle(f.cboContactTitles.Text);
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
            Close();
            _dataOperations.ReturnErrorInformation();
            //_dataOperations.GetStoredProcedureContentsWithParameters("UpateCustomer");

            /*
             * https://en.wikipedia.org/wiki/Stored_procedure
             */

        }


    }

}