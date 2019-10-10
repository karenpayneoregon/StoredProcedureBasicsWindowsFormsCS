//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DataOperations.TypeClasses;

namespace DataOperations
{
	/// <summary>
	/// All data queries are in Stored Procedures
	/// </summary>
	public class BackendOperations : BaseConnectionLibrary.ConnectionClasses.SqlServerConnection
	{
		public BackendOperations()
		{
			//
			// Make sure to change DatabaseServer to the server
			// with CustomerDatabase
			//
			DatabaseServer = "KARENS-PC";
			DefaultCatalog = "CustomerDatabase";

		}

		public DataTable RetrieveAllRecords()
		{

			mHasException = false;
			var dt = new DataTable();

			try
			{
				using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
				{
					using (SqlCommand cmd = new SqlCommand
					{
						Connection = cn,
						CommandType = CommandType.StoredProcedure
					})
					{

						cmd.CommandText = "dbo.[SelectAllCustomers]";

						cn.Open();
						dt.Load(cmd.ExecuteReader());

						dt.Columns["ContactTypeIdentifier"].ColumnMapping = MappingType.Hidden;

					}
				}

			}
			catch (Exception ex)
			{
				mHasException = true;
				mLastException = ex;
			}

			return dt;

		}
		/// <summary>
		/// Mocked up sample showing how to return error information from a failed
		/// operation within a stored procedure.
		/// </summary>
		public void ReturnErrorInformation()
		{
			using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
			{
				using (SqlCommand cmd = new SqlCommand
				{
					Connection = cn,
					CommandType = CommandType.StoredProcedure
				})
				{

					cmd.CommandText = "dbo.[usp_ThrowDummyException]";

					cmd.Parameters.Add(new SqlParameter
					{
						ParameterName = "@ErrorMessage",
						SqlDbType = SqlDbType.NVarChar,
						Direction = ParameterDirection.Output
					}).Value = "";

					cmd.Parameters.Add(new SqlParameter
					{
						ParameterName = "@ErrorSeverity",
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Output
					});

					cmd.Parameters.Add(new SqlParameter
					{
						ParameterName = "@ErrorState",
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Output
					});

					try
					{
						cn.Open();
						cmd.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						Console.WriteLine($"[{ex.Message}]");
						Console.WriteLine(cmd.Parameters["@ErrorSeverity"].Value);
						Console.WriteLine(cmd.Parameters["@ErrorState"].Value);
					}
				}
			}
		}
		/// <summary>
		/// This example does not return contact type (contact name) as this is
		/// common practice as we know the type from the ComboBox selection.
		/// </summary>
		/// <param name="contactTypeIdentifier"></param>
		/// <returns></returns>
		public DataTable GetAllRecordsByContactTitle(int contactTypeIdentifier)
		{
			mHasException = false;
			var dt = new DataTable();

			try
			{
				using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
				{
					using (SqlCommand cmd = new SqlCommand
					{
						Connection = cn,
						CommandType = CommandType.StoredProcedure
					})
					{

						cmd.CommandText = "dbo.ContactByType";

						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@ContactTitleTypeIdentifier",
							SqlDbType = SqlDbType.Int
						});

						cmd.Parameters["@ContactTitleTypeIdentifier"].Value = contactTypeIdentifier;

						cn.Open();

						dt.Load(cmd.ExecuteReader());

					}
				}

			}
			catch (Exception ex)
			{
				mHasException = true;
				mLastException = ex;
			}

			return dt;

		}
		public List<ContactTypes> RetrieveContactTitles()
		{

			mHasException = false;

			List<ContactTypes> contactList = new List<ContactTypes>();

			try
			{
				using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
				{
					using (SqlCommand cmd = new SqlCommand
					{
						Connection = cn,
						CommandType = CommandType.StoredProcedure
					})
					{

						cmd.CommandText = "dbo.[SelectContactTitles]";

						cn.Open();
						SqlDataReader reader = cmd.ExecuteReader();
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								contactList.Add(new ContactTypes()
								{
									Identifier = reader.GetInt32(0),
									ContactType = reader.GetString(1)
								});
							}
						}
					}
				}

			}
			catch (Exception ex)
			{
				mHasException = true;
				mLastException = ex;
			}

			return contactList;

		}
		public int AddCustomer(string companyName, string contactName, int contactTypeIdentifier)
		{
			mHasException = false;
			try
			{
				using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
				{

					using (SqlCommand cmd = new SqlCommand
					{
						Connection = cn,
						CommandType = CommandType.StoredProcedure
					})
					{

						cmd.CommandText = "dbo.InsertCustomer";

						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@CompanyName",
							SqlDbType = SqlDbType.NVarChar
						});
						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@ContactName",
							SqlDbType = SqlDbType.NVarChar
						});
						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@ContactTypeIdentifier",
							SqlDbType = SqlDbType.Int
						});
						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@Identity",
							SqlDbType = SqlDbType.Int,
							Direction = ParameterDirection.Output
						});

						cmd.Parameters["@CompanyName"].Value = companyName;
						cmd.Parameters["@ContactName"].Value = contactName;
						cmd.Parameters["@ContactTypeIdentifier"].Value = contactTypeIdentifier;

						cn.Open();

						cmd.ExecuteScalar();

						return Convert.ToInt32(cmd.Parameters["@Identity"].Value);

					}
				}
			}
			catch (Exception ex)
			{
				mHasException = true;
				mLastException = ex;

				return -1;

			}
		}
		public bool RemoveCustomer(int identifier)
		{
			mHasException = false;
			using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
			{
				using (SqlCommand cmd = new SqlCommand
				{
					Connection = cn,
					CommandType = CommandType.StoredProcedure
				})
				{

					cmd.CommandText = "dbo.[DeleteCustomer]";

					cmd.Parameters.Add(new SqlParameter
					{
						ParameterName = "@Identity",
						SqlDbType = SqlDbType.Int
					});
					cmd.Parameters.Add(new SqlParameter
					{
						ParameterName = "@flag",
						SqlDbType = SqlDbType.Bit,
						Direction = ParameterDirection.Output
					});

					cmd.Parameters["@Identity"].Value = identifier;
					cmd.Parameters["@flag"].Value = 0;

					try
					{

						cn.Open();

						cmd.ExecuteNonQuery();

						if (Convert.ToBoolean(cmd.Parameters["@flag"].Value))
						{
							return true;
						}
						else
						{
							return false;
						}
					}
					catch (Exception ex)
					{
						mHasException = true;
						mLastException = ex;
						return false;
					}
				}
			}
		}
		/// <summary>
		/// Update record if primary is found
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <param name="companyName"></param>
		/// <param name="contactName"></param>
		/// <param name="contactIdentifier"></param>
		/// <returns></returns>
		public bool UpdateCustomer(int primaryKey, string companyName, string contactName, int contactIdentifier)
		{

			mHasException = false;

			try
			{
				using (SqlConnection cn = new SqlConnection {ConnectionString = this.ConnectionString})
				{
					using (SqlCommand cmd = new SqlCommand
					{
						Connection = cn,
						CommandType = CommandType.StoredProcedure
					})
					{

						cmd.CommandText = "dbo.[UpateCustomer]";

						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@CompanyName",
							SqlDbType = SqlDbType.NVarChar
						});
						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@ContactName",
							SqlDbType = SqlDbType.NVarChar
						});
						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@ContactTypeIdentifier",
							SqlDbType = SqlDbType.Int
						});
						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@Identity",
							SqlDbType = SqlDbType.Int
						});
						cmd.Parameters.Add(new SqlParameter
						{
							ParameterName = "@flag",
							SqlDbType = SqlDbType.Bit,
							Direction = ParameterDirection.Output
						});

						cmd.Parameters["@CompanyName"].Value = companyName;
						cmd.Parameters["@ContactName"].Value = contactName;
						cmd.Parameters["@ContactTypeIdentifier"].Value = contactIdentifier;
						cmd.Parameters["@Identity"].Value = primaryKey;
						cmd.Parameters["@flag"].Value = 0;

						cn.Open();

						cmd.ExecuteNonQuery();

						if (Convert.ToBoolean(cmd.Parameters["@flag"].Value))
						{
							return true;
						}
						else
						{
							return false;
						}

					}
				}
			}
			catch (Exception ex)
			{
				mHasException = true;
				mLastException = ex;
				return false;
			}
		}
	}

}