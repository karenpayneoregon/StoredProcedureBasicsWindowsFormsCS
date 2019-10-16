using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataOperations;

namespace SpecialReadStoredProcedureExample
{
    public partial class Form1 : Form
    {
        private readonly BackendOperations _dataOperations = new BackendOperations();
        public Form1()
        {
            InitializeComponent();
        }

        private void GetAllCustomersButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dataOperations.GetAllCustomersRecords();
        }

        private void GetCustomerByIdentifierButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dataOperations.GetAllCustomerRecordsByIdentifier(2);
        }

        private void GetCustomerByCompanyNameButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dataOperations.GetAllCustomerRecordsByCompanyName("Salem Boat Rentals");
        }
    }
}
