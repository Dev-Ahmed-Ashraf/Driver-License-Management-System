using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class clsLicensesData
    {
        public static DataTable GetAllLicensesforThisPerson(int DriverID)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "SELECT License_ID, Driver_id, IssueDate, ExpirationDate, Notes, IsActive FROM Licenses WHERE Driver_id = @DriverID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                {
                    dt.Load(Reader);
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"
                              INSERT INTO Licenses
                               (Application_id, Driver_id, LicenseClass_id, IssueDate, ExpirationDate, IsActive, Notes, PaidFees, IssueReason, CreatedByUser_id)
                         VALUES
                               (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @IsActive, @Notes, @PaidFees, @IssueReason, @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);

            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            if (Notes == "")
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return LicenseID;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"SELECT Licenses.License_ID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.Driver_id = Drivers.Driver_ID
                            WHERE  
                             
                             Licenses.LicenseClass_id = @LicenseClass 
                              AND Drivers.Person_id = @PersonID
                              And IsActive=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
            return LicenseID;
        }
        public static bool GetLicenseInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
            ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE License_ID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["Application_id"];
                    DriverID = (int)reader["Driver_id"];
                    LicenseClass = (int)reader["LicenseClass_id"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];

                    if (reader["Notes"] == DBNull.Value)
                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUser_id"];
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE License_ID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
   


    //public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
    //     DateTime IssueDate, DateTime ExpirationDate, string Notes,
    //     float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
    //{

    //    int rowsAffected = 0;
    //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

    //    string query = @"UPDATE Licenses
    //                   SET ApplicationID=@ApplicationID, DriverID = @DriverID,
    //                      LicenseClass = @LicenseClass,
    //                      IssueDate = @IssueDate,
    //                      ExpirationDate = @ExpirationDate,
    //                      Notes = @Notes,
    //                      PaidFees = @PaidFees,
    //                      IsActive = @IsActive,IssueReason=@IssueReason,
    //                      CreatedByUserID = @CreatedByUserID
    //                 WHERE LicenseID=@LicenseID";

    //    SqlCommand command = new SqlCommand(query, connection);

    //    command.Parameters.AddWithValue("@LicenseID", LicenseID);
    //    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
    //    command.Parameters.AddWithValue("@DriverID", DriverID);
    //    command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
    //    command.Parameters.AddWithValue("@IssueDate", IssueDate);
    //    command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

    //    if (Notes == "")
    //        command.Parameters.AddWithValue("@Notes", DBNull.Value);
    //    else
    //        command.Parameters.AddWithValue("@Notes", Notes);

    //    command.Parameters.AddWithValue("@PaidFees", PaidFees);
    //    command.Parameters.AddWithValue("@IsActive", IsActive);
    //    command.Parameters.AddWithValue("@IssueReason", IssueReason);
    //    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

    //    try
    //    {
    //        connection.Open();
    //        rowsAffected = command.ExecuteNonQuery();

    //    }
    //    catch (Exception ex)
    //    {
    //        //Console.WriteLine("Error: " + ex.Message);
    //        return false;
    //    }

    //    finally
    //    {
    //        connection.Close();
    //    }

    //    return (rowsAffected > 0);
    //}
    }
}