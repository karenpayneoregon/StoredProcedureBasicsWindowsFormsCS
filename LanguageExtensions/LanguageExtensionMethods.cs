using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace ExtensionsLibrary
{
	public static class LanguageExtensionMethods
	{
		/// <summary>
		/// Determine if a TextBox controls are not empty for a container
		/// </summary>
		/// <param name="sender"></param>
		/// <returns></returns>
		public static bool ValidateTextBoxes(this Control sender)
		{
			List<TextBox> list = sender.Controls.OfType<TextBox>().ToList();
			return !list.Any((tb) => string.IsNullOrWhiteSpace(tb.Text));
		}
		/// <summary>
		/// Return current row as a DataRow
		/// </summary>
		/// <param name="sender"></param>
		/// <returns></returns>
		public static DataRow CurrentRow(this BindingSource sender) => ((DataRowView)sender.Current).Row;
        /// <summary>
		/// Return DataSource as a DataTable
		/// </summary>
		/// <param name="sender"></param>
		/// <returns></returns>
		public static DataTable DataTable(this BindingSource sender) => (DataTable)sender.DataSource;
        /// <summary>
		/// Add new DataRow to DataTable where Identifier is a valid existing primary key
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="Identifier">Existing primary key</param>
		/// <param name="CompanyName">Company name value</param>
		/// <param name="ContactName">Contact name value</param>
		/// <param name="ContactTitle">Contact title value</param>
		/// <param name="ContactTypeIdentifier">Corresponding identifier for contact title</param>
		public static void AddRow(this BindingSource sender, int Identifier, string CompanyName, string ContactName, string ContactTitle, int ContactTypeIdentifier)
		{
			sender.DataTable().Rows.Add(new object[] {Identifier, CompanyName, ContactName, ContactTypeIdentifier, ContactTitle});
		}
		public static int Identifier(this DataRow sender) => sender.Field<int>("Identifier");
        public static string CompanyName(this DataRow sender) => sender.Field<string>("CompanyName");
        public static void SetCompanyName(this DataRow sender, string newCompanyNameValue) => sender.SetField("CompanyName", newCompanyNameValue);
        public static string ContactName(this DataRow sender) => sender.Field<string>("ContactName");
        public static void SetContactName(this DataRow sender, string newContactNameValue) => sender.SetField("ContactName", newContactNameValue);
        public static string ContactTitle(this DataRow sender) => sender.Field<string>("ContactTitle");
        public static void SetContactTitle(this DataRow sender, string newContactTitleValue) => sender.SetField("ContactTitle", newContactTitleValue);
        public static void ExpandColumns(this DataGridView sender)
		{
			foreach (DataGridViewColumn col in sender.Columns)
			{
				col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			}
		}
	}

}