using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsApplication_cs.Classes;

namespace WindowsApplication_cs
{
    public partial class MainForm : Form
    {
        private readonly BindingSource _bindingSource = new BindingSource();

        private readonly DatabaseImageOperations  _databaseImageOperations =
            new DatabaseImageOperations();

        public MainForm()
        {
            InitializeComponent();
        }
        void BindImage(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType == typeof(Image))
            {
                var ms = new MemoryStream((byte[])e.Value);
                Image image = Image.FromStream(ms);
                e.Value = image;
            }
        }
        void Form1_Load(object sender, EventArgs e)
        {           
     
            DataGridView1.AutoGenerateColumns = false;

            _bindingSource.DataSource = _databaseImageOperations.DataTable();
            DataGridView1.DataSource = _bindingSource;
            bindingNavigator1.BindingSource = _bindingSource;

            var imageBinding = new Binding("Image", _bindingSource, "ImageData");
            imageBinding.Format += BindImage;
            CurrentPictureBox.DataBindings.Add(imageBinding);

        }
        void InsertImageButton_Click(object sender, EventArgs e)
        {
            var f = new SelectImageForm();

            try
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    int Identifier = 0;
                    var fileBytes = File.ReadAllBytes(f.FileName);
                    var (success, exception) = _databaseImageOperations.InsertImage(fileBytes, f.Description, ref Identifier);
                    if (success == Success.Okay)
                    {
                        ((DataTable)_bindingSource.DataSource).Rows.Add(Identifier, fileBytes, f.Description);

                        var index = _bindingSource.Find("ImageId", Identifier);

                        if (index > -1)
                        {
                            _bindingSource.Position = index;
                        }
                    }
                }
            }
            finally
            {
                f.Dispose();
            }
        }       
    }
}
