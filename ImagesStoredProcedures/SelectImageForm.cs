using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplication_cs
{
    public partial class SelectImageForm : Form
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public SelectImageForm()
        {
            InitializeComponent();
        }

        private void FinishedButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = OpenFileDialog1.FileName;
                FileName = label1.Text;
            }
        }

        private void DescriptionTextBox1_TextChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = !string.IsNullOrWhiteSpace(txtDescription.Text);
            if (OkButton.Enabled)
            {
                Description = txtDescription.Text;
            }
        } 
    }
}
