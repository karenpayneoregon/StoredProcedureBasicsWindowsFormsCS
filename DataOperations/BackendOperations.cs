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
    public class BackendOperations
    {
        private static string ConnectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=CustomerDatabase;" +
            "Integrated Security=True";


        public (List<string> list, Exception) StoredProcedureNameList()
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
                        return (null, e);
                    }
                }
            }

            return (storedProcNames, null);

        }

        /// <summary>
        /// Get stored procedure definition.
        /// If there are parameters they are not returned
        /// </summary>
        /// <param name="pName"></param>
        public (string results, Exception exception) GetStoredProcedureContents(string pName)
        {
            var contents = "";

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn })
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
                        return ("", e);
                    }
                }
            }

            var index = contents.IndexOf("CREATE", StringComparison.Ordinal);

            return (index != -1 ? contents.Substring(index) : contents, null);

        }
        public (List<StoredProcedureDetail> list, Exception exception) GetStoredProcedureContentsWithParameters(string pName)
        {
            var procedureDetailList = new List<StoredProcedureDetail>();

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {
                    try
                    {
                        cn.Open();

                        cmd.CommandText = "SELECT name, system_type_id, max_length, precision, scale " +
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
                        return (null, e);
                    }
                }
            }

            return (procedureDetailList, null);
        }

        #region Next three methods utilize a single stored procedure
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCustomersRecords()
        {

            var dt = new DataTable();

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

            return dt;
        }
        /// <summary>
        /// Get a single customer by primary key
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
	    public DataTable GetAllCustomerRecordsByIdentifier(int identifier)
        {

            var dt = new DataTable();
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

            return dt;
        }

        /// <summary>
        /// Get customer by customer name
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public DataTable GetAllCustomerRecordsByCompanyName(string companyName)
        {
            var dt = new DataTable();

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

                return dt;
            }
        }

        #endregion


        public DataTable RetrieveAllCustomerRecords()
        {

            var dt = new DataTable();

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
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

            return dt;

        }
        /// <summary>
        /// Mocked up sample showing how to return error information from a failed
        /// operation within a stored procedure.
        /// </summary>
        public void ReturnErrorInformation()
        {
            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
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

            var dt = new DataTable();

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
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

            return dt;

        }
        /// <summary>
        /// Retrieve all contact titles
        /// </summary>
        /// <returns></returns>
        public List<ContactTypes> RetrieveContactTitles()
        {



            var contactList = new List<ContactTypes>();

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
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

            return contactList;

        }
        /// <summary>
        /// Add a new Customer, return new primary key on success.
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="contactName"></param>
        /// <param name="contactTypeIdentifier"></param>
        /// <returns></returns>
        public (int result, Exception exception) AddCustomer(string companyName, string contactName, int contactTypeIdentifier)
        {

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

                        return (Convert.ToInt32(cmd.Parameters["@Identity"].Value), null);

                    }
                }
            }
            catch (Exception e)
            {
                return (-1, e);

            }
        }
        /// <summary>
        /// Remove customer by primary key
        /// </summary>
        /// <param name="primaryKey">Existing customer key</param>
        /// <returns></returns>
        public (bool success, Exception exception) RemoveCustomer(int primaryKey)
        {

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
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

                        return (Convert.ToBoolean(cmd.Parameters["@flag"].Value), null);
                    }
                    catch (Exception e)
                    {
                        return (false, e);
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
        public (bool succcess, Exception exception) UpdateCustomer(int primaryKey, string companyName, string contactName, int contactIdentifier)
        {

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

                        return (Convert.ToBoolean(cmd.Parameters["@flag"].Value), null);

                    }
                }
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }
    }

}