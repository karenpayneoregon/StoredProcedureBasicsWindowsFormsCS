using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;


namespace WindowsApplication_cs.Classes
{
    public enum Success
    {
        /// <summary>
        /// Successfully completed
        /// </summary>
        Okay,
        /// <summary>
        /// Something went wrong
        /// </summary>
        OhSnap
    }

    public class DatabaseImageOperations 
    {

        private static string ConnectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=NORTHWND_NEW.MDF;" +
            "Integrated Security=True";
        /// <summary>
        /// Determines if there are any records
        /// </summary>
        /// <returns></returns>
        /// <remarks>not used, was for use during writing code for the code sample</remarks>
        public bool HasRecords()
        {
            bool result = true;

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn, CommandText = "SELECT COUNT(ImageID) FROM ImageData" })
                {
                    cn.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }

            return result;
        }
        /// <summary>
        /// Given a valid image converts it to a byte array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        /// <remarks>
        /// Suitable for saving a file to disk
        /// Alternate is to use a memory stream
        /// </remarks>
        public byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        /// <summary>
        /// Save image to the sql-server database table
        /// </summary>
        /// <param name="image">Valid image</param>
        /// <param name="description">Information to describe the image</param>
        /// <param name="identifier">New primary key</param>
        /// <returns></returns>
        public Success InsertImage(Image image, string description, ref int identifier)
        {

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn, CommandText = "SaveImage" })
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@img", SqlDbType.Image).Value = ImageToByte(image);
                    cmd.Parameters.Add("@description", SqlDbType.Text).Value = description;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@new_identity",
                        SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output
                    });

                    try
                    {
                        cn.Open();
                        identifier = Convert.ToInt32(cmd.ExecuteScalar());
                        return Success.Okay;
                    }
                    catch (Exception ex)
                    {
                        return Success.OhSnap;
                    }
                }
            }
        }
        /// <summary>
        /// Insert image where ImageByes is a byte array from a valid image
        /// </summary>
        /// <param name="imageBytes">Byte array </param>
        /// <param name="description">used to describe the image</param>
        /// <param name="identifier">New primary key</param>
        /// <returns></returns>
        public (Success success, Exception exception) InsertImage(byte[] imageBytes, string description, ref int identifier)
        {


            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn, CommandText = "SaveImage" })
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@img", SqlDbType.Image).Value = imageBytes;

                    cmd.Parameters.Add("@description", SqlDbType.Text).Value = 
                        string.IsNullOrWhiteSpace(description) ? "None" : description;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@new_identity",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    });

                    try
                    {
                        cn.Open();
                        identifier = Convert.ToInt32(cmd.ExecuteScalar());
                        return (Success.Okay, null);
                    }
                    catch (Exception ex)
                    {
                        return (Success.OhSnap,ex);
                    }
                }
            }
        }

        /// <summary>
        /// Set image passed in parameter 2 to bytes returned from image field of identifier or
        /// on error or key not found an error message is set and can be read back by the caller
        /// </summary>
        /// <param name="identifier">primary key to locate</param>
        /// <param name="inBoundImage">Image to set from returned bytes in database table of found record</param>
        /// <param name="description"></param>
        /// <returns>Success</returns>
        /// <remarks>
        /// An alternative is to return the image rather than success as done now
        /// </remarks>
        public (Success success, Exception exception) GetImage(int identifier, ref Image inBoundImage, ref string description)
        {

            DataTable dtResults = new DataTable();
            Success SuccessType = 0;

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn, CommandText = "ReadImage" })
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@imgId", SqlDbType.Int).Value = identifier;

                    try
                    {

                        cn.Open();
                        dtResults.Load(cmd.ExecuteReader());

                        if (dtResults.Rows.Count == 1)
                        {
                            var ms = new MemoryStream((byte[])(dtResults.Rows[0]["ImageData"]));
                            description = Convert.ToString(dtResults.Rows[0]["Description"]);
                            inBoundImage = Image.FromStream(ms);
                            SuccessType = Success.Okay;
                        }
                        else
                        {
                            SuccessType = Success.OhSnap;
                        }
                    }
                    catch (Exception ex)
                    {
                        return (Success.OhSnap, ex);
                    }
                }
            }

            return (SuccessType, null);

        }
        /// <summary>
        /// Get an image in the database table by primary key
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="inBoundImage"></param>
        /// <returns></returns>
        /// <remarks>
        /// Not used, here for you if needed to get a single image, has been tested and works
        /// </remarks>
        public (Success success, Exception exception) GetImage(int identifier, ref Image inBoundImage)
        {
            var description = "";
            return GetImage(identifier, ref inBoundImage, ref description);
        }
        /// <summary>
        /// Return all records in our table
        /// </summary>
        /// <returns></returns>
        public DataTable DataTable()
        {
            var dt = new DataTable();

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn, CommandText = "ReadAllRecords" })
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }

            return dt;

        }

    }


}
