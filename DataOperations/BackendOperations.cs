﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using BaseConnectionLibrary.ConnectionClasses;
using System.Data.SqlClient;
using DataOperations.TypeClasses;

namespace DataOperations
{
	/// <summary>
	/// All data queries are in Stored Procedures
	/// </summary>
	public class BackendOperations : SqlServerConnection
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

	    public List<string> StoredProcedureNameList()
	    {
            var storedProcNames = new List<string>();

	        using (var cn = new SqlConnection { ConnectionString = ConnectionString })
	        {
	            using (var cmd = new SqlCommand() { Connection = cn })
	            {
	                cmd.CommandText = "SELECT NAME from SYS.PROCEDURES WHERE name NOT LIKE 'sp_%' ORDER BY name;";
	                try
	                {
	                    cn.Open();

	                    var reader = cmd.ExecuteReader();

	                    while (reader.Read())
	                    {
	                        storedProcNames.Add(reader.GetString(0));
	                    }

	                }
	                catch (Exception e)
	                {
	                    mHasException = true;
	                    mLastException = e;
	                }
	            }
	        }

            return storedProcNames;

	    }

	    /// <summary>
        /// Get stored procedure definition.
        /// If there are parameters they are not returned
        /// </summary>
        /// <param name="pName"></param>
	    public string GetStoredProcedureContents(string pName)
	    {
	        var contents = "";

	        using (var cn = new SqlConnection {ConnectionString = ConnectionString})
	        {
	            using (var cmd = new SqlCommand() {Connection = cn})
	            {
	                cmd.CommandText = "SELECT definition FROM sys.sql_modules  " + 
	                                 $"WHERE [object_id] = OBJECT_ID('dbo.{pName}');";

	                try
	                {
	                    cn.Open();
	                    contents = Convert.ToString(cmd.ExecuteScalar());
	                }
	                catch (Exception e)
	                {
	                    mHasException = true;
	                    mLastException = e;
                    }
	            }
	        }

	        var index = contents.IndexOf("CREATE", StringComparison.Ordinal);

	        return index != -1 ? 
	            contents.Substring(index) : contents;

	    }
	    public List<StoredProcedureDetail> GetStoredProcedureContentsWithParameters(string pName)
	    {
	        var procedureDetailList = new List<StoredProcedureDetail>();

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
	        {
	            using (var cmd = new SqlCommand() { Connection = cn})
	            {
	                try
	                {
	                    cn.Open();

	                    cmd.CommandText =  "SELECT name, system_type_id, max_length, precision, scale " + 
	                                       "FROM sys.parameters " + 
	                                      $"WHERE [object_id] = OBJECT_ID('dbo.{pName}');";

	                    var reader = cmd.ExecuteReader();
	                    while (reader.Read())
	                    {
	                        procedureDetailList.Add(new StoredProcedureDetail()
	                        {
	                            Name = reader.GetString(0),
	                            SystemType = reader.GetByte(1),
	                            MaxLength = reader.GetInt16(2),
	                            Precision = reader.GetByte(3),
	                            Scale = reader.GetByte(4)
                            });
	                    }
	                }
	                catch (Exception e)
	                {
	                    mHasException = true;
	                    mLastException = e;
                    }
	            }
	        }

	        return procedureDetailList;
	    }

        #region Next three methods utilize a single stored procedure
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCustomersRecords()
        {
            mHasException = false;
            var dt = new DataTable();

            try
            {
                using (var cn = new SqlConnection { ConnectionString = ConnectionString })
                {
                    using (var cmd = new SqlCommand
                    {
                        Connection = cn,
                        CommandType = CommandType.StoredProcedure
                    })
                    {

                        cmd.CommandText = "dbo.[Customer_Reader]";

                        cmd.Parameters.AddWithValue("@CustomerIdentifier", null);
                        cmd.Parameters.AddWithValue("@CompanyName", null);

                        cn.Open();

                        dt.Load(cmd.ExecuteReader());

                    }
                }

            }
            catch (Exception e)
            {
                mHasException = true;
                mLastException = e;
            }

            return dt;
        }
        /// <summary>
        /// Get a single customer by primary key
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
	    public DataTable GetAllCustomerRecordsByIdentifier(int identifier)
        {
            mHasException = false;
            var dt = new DataTable();

            try
            {
                using (var cn = new SqlConnection { ConnectionString = ConnectionString })
                {
                    using (var cmd = new SqlCommand
                    {
                        Connection = cn,
                        CommandType = CommandType.StoredProcedure
                    })
                    {

                        cmd.CommandText = "dbo.[Customer_Reader]";

                        cmd.Parameters.AddWithValue("@CustomerIdentifier", identifier);
                        cmd.Parameters.AddWithValue("@CompanyName", null);

                        cn.Open();

                        dt.Load(cmd.ExecuteReader());

                    }
                }

            }
            catch (Exception e)
            {
                mHasException = true;
                mLastException = e;
            }

            return dt;
        }
        /// <summary>
        /// Get customer by customer name
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public DataTable GetAllCustomerRecordsByCompanyName(string companyName)
        {
            mHasException = false;
            var dt = new DataTable();

            try
            {
                using (var cn = new SqlConnection { ConnectionString = ConnectionString })
                {
                    using (var cmd = new SqlCommand
                    {
                        Connection = cn,
                        CommandType = CommandType.StoredProcedure
                    })
                    {

                        cmd.CommandText = "dbo.[Customer_Reader]";

                        cmd.Parameters.AddWithValue("@CustomerIdentifier", null);
                        cmd.Parameters.AddWithValue("@CompanyName", companyName);

                        cn.Open();

                        dt.Load(cmd.ExecuteReader());

                    }
                }

            }
            catch (Exception e)
            {
                mHasException = true;
                mLastException = e;
            }

            return dt;
        }

        #endregion


        public DataTable RetrieveAllCustomerRecords()
		{

			mHasException = false;
			var dt = new DataTable();

			try
			{
				using (var cn = new SqlConnection {ConnectionString = ConnectionString})
				{
					using (var cmd = new SqlCommand
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
			catch (Exception e)
			{
				mHasException = true;
				mLastException = e;
			}

			return dt;

		}
		/// <summary>
		/// Mocked up sample showing how to return error information from a failed
		/// operation within a stored procedure.
		/// </summary>
		public void ReturnErrorInformation()
		{
			using (var cn = new SqlConnection {ConnectionString = ConnectionString})
			{
				using (var cmd = new SqlCommand
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
				using (var cn = new SqlConnection {ConnectionString = ConnectionString})
				{
					using (var cmd = new SqlCommand
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
        /// <summary>
        /// Retrieve all contact titles
        /// </summary>
        /// <returns></returns>
		public List<ContactTypes> RetrieveContactTitles()
		{

			mHasException = false;

			var contactList = new List<ContactTypes>();

			try
			{
				using (var cn = new SqlConnection {ConnectionString = ConnectionString})
				{
					using (var cmd = new SqlCommand
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
        /// <summary>
        /// Add a new Customer, return new primary key on success.
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="contactName"></param>
        /// <param name="contactTypeIdentifier"></param>
        /// <returns></returns>
		public int AddCustomer(string companyName, string contactName, int contactTypeIdentifier)
		{
			mHasException = false;
			try
			{
				using (var cn = new SqlConnection {ConnectionString = ConnectionString})
				{

					using (var cmd = new SqlCommand
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
        /// <summary>
        /// Remove customer by primary key
        /// </summary>
        /// <param name="primaryKey">Existing customer key</param>
        /// <returns></returns>
		public bool RemoveCustomer(int primaryKey)
		{
			mHasException = false;
			using (var cn = new SqlConnection {ConnectionString = ConnectionString})
			{
				using (var cmd = new SqlCommand
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

					cmd.Parameters["@Identity"].Value = primaryKey;
					cmd.Parameters["@flag"].Value = 0;

					try
					{

						cn.Open();

						cmd.ExecuteNonQuery();

						return Convert.ToBoolean(cmd.Parameters["@flag"].Value);
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
		/// Update record by primary key
		/// </summary>
		/// <param name="primaryKey">Existing customer key</param>
		/// <param name="companyName"></param>
		/// <param name="contactName"></param>
		/// <param name="contactIdentifier">Existing contact key</param>
		/// <returns></returns>
		public bool UpdateCustomer(int primaryKey, string companyName, string contactName, int contactIdentifier)
		{

			mHasException = false;

			try
			{
				using (var cn = new SqlConnection {ConnectionString = this.ConnectionString})
				{
					using (var cmd = new SqlCommand
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

                        return Convert.ToBoolean(cmd.Parameters["@flag"].Value);

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