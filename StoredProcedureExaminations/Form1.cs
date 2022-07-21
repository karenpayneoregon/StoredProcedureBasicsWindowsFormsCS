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

namespace StoredProcedureExaminations
{
    public partial class Form1 : Form
    {
        private readonly BackendOperations _dataOperations = new BackendOperations();
        public Form1()
        {
            InitializeComponent();

            Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            var (list, _) = _dataOperations.StoredProcedureNameList();
            
            StoredProcedureNamesListBox.DataSource = list;
            StoredProcedureNamesListBox.MouseDoubleClick += StoredProcedureNamesListBox_MouseDoubleClick;
        }
        private void GetStoredProcDetailsButton_Click(object sender, EventArgs e)
        {
            GetDetails();
        }
        private void StoredProcedureNamesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GetDetails();
        }
        private void GetDetails()
        {
            ParameterListView.Items.Clear();
            var (results, _) = _dataOperations.GetStoredProcedureContents(StoredProcedureNamesListBox.Text);
            StoredProcedureDefinitionTextBox.Text = results;

            var (details, _) = _dataOperations.GetStoredProcedureContentsWithParameters(StoredProcedureNamesListBox.Text);

            foreach (var detail in details)
            {
                ParameterListView.Items.Add(new ListViewItem(detail.ItemArray()));
            }

            ParameterListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

    }
}
