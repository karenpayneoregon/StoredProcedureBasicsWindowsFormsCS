using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class CustomerEditorForm
	{

		public CustomerEditorForm()
		{
			InitializeComponent();
		}

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            if (Controls.OfType<TextBox>().Any(tb => string.IsNullOrWhiteSpace(tb.Text)))
            {
                MessageBox.Show("All fields are required");
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}