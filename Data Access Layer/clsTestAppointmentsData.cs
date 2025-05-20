using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class clsTestAppointmentsData
    {
        public static DataTable GetAllTestAppointments(int LocalDrivingLicenseID, byte TestTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "Select TestAppointment_ID, AppointmentDate, IsLocked from TestAppointments where LocalDrivingLicense_id =@LocalDrivingLicenseID and TestType_id = @TestTypeID;";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.Add("@LocalDrivingLicenseID", LocalDrivingLicenseID);
            Command.Parameters.Add("@TestTypeID", TestTypeID);
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
        public static int AddNewTestAppointment(int TestTypeID, DateTime AppointmentDate, int LocalDrivingLicenseID,
             int RetakeTestApplicationID, float PaidFees, int CreatedByUserID)
        {
            int TestAppointment_ID = -1;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"INSERT INTO TestAppointments (
                            TestType_id,LocalDrivingLicense_id,
                            AppointmentDate,PaidFees,
                            IsLocked,CreatedByUser_id,RetakeTestApplicationID)
                             VALUES (@TestType_id,@LocalDrivingLicense_id,
                                      @AppointmentDate,@PaidFees,0,
                                      @CreatedByUser_id,@RetakeTestApplicationID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestType_id", @TestTypeID);
            command.Parameters.AddWithValue("LocalDrivingLicense_id", @LocalDrivingLicenseID);
            command.Parameters.AddWithValue("AppointmentDate", @AppointmentDate);
            command.Parameters.AddWithValue("PaidFees", @PaidFees);
            command.Parameters.AddWithValue("CreatedByUser_id", @CreatedByUserID);

            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointment_ID = insertedID;
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
            return TestAppointment_ID;
        }
        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, DateTime AppointmentDate, int LocalDrivingLicenseID,
             bool IsLocked, int RetakeTestApplicationID, float PaidFees, int CreatedByUserID)
        {

            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"Update  TestAppointments  
                            set TestType_id = @TestType_id, 
                                LocalDrivingLicense_id = @LocalDrivingLicense_id, 
                                AppointmentDate = @AppointmentDate,
                                PaidFees  = @PaidFees,
                                IsLocked = @IsLocked,
                                CreatedByUser_id = @CreatedByUser_id,
                                RetakeTestApplicationID = @RetakeTestApplicationID                               
                                where TestAppointment_ID = @TestAppointment_ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointment_ID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestType_id", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicense_id", LocalDrivingLicenseID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            command.Parameters.AddWithValue("@CreatedByUser_id", CreatedByUserID);


            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

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
#pragma warning restore CS0168 // Variable is declared but never used

            return (RowsAffected > 0);
        }
        public static int GetTrialsOfTestAppo(int LocalDrivingLicenseID, string TestTypeTitle)
        {
            int Trials = 0;
            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "select count (TestAppointment_ID) as Trial from TestAppointments_View \r\nwhere LocalDrivingLicense_id = @LocalDrivingLicense_id and TestTypeTitle = @TestTypeTitle";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicense_id", LocalDrivingLicenseID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);

            try
            {
                connection.Open();
                Trials = Convert.ToInt16(command.ExecuteScalar());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);              
            }

            finally
            {
                connection.Close();
            }
#pragma warning restore CS0168 // Variable is declared but never used

            return Trials;
        }
        public static bool GetTestAppoInfoByID(int TestAppoID, ref int TestTypeID, ref int LocalDrivingLicenseID,
            ref DateTime AppointmentDate, ref bool IsLocked, ref int CreatedByUserID, ref float PaidFees, ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "Select * from TestAppointments WHERE TestAppointment_ID = @TestAppoID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppoID", TestAppoID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestTypeID = (int)reader["TestType_id"];
                    LocalDrivingLicenseID = (int)reader["LocalDrivingLicense_id"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]); 
                    IsLocked = (bool)reader["IsLocked"];
                    CreatedByUserID = (int)reader["CreatedByUser_id"];                 
                   
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
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
        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"select Test_ID from Tests where TestAppointment_id=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
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

            return TestID;
        }
        public static bool IsTestAppoIsLocked(int TestAppointmentID)
        {
            bool IsLocked = false;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "SELECT Found = 1 FROM TestAppointments WHERE TestAppointment_ID = @TestAppointmentID AND IsLocked = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                IsLocked = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                IsLocked = false;
            }
            finally
            {
                connection.Close();
            }

            return IsLocked;
        }
        public static bool GetLastTestAppointment(
            int LocalDrivingLicenseApplicationID, int TestTypeID,
           ref int TestAppointmentID, ref DateTime AppointmentDate,
           ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"SELECT       top 1 *   FROM            TestAppointments
                WHERE        (TestType_id = @TestTypeID) 
                AND (LocalDrivingLicense_id = @LocalDrivingLicenseApplicationID) 
                order by TestAppointment_ID Desc";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointment_ID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsLocked = (bool)reader["IsLocked"];
                    CreatedByUserID = (int)reader["CreatedByUser_id"];
                    

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];


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
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
