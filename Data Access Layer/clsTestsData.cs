using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class clsTestsData
    {
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"SELECT PassedTestCount = count(TestType_id)
                         FROM Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointment_id = TestAppointments.TestAppointment_ID
						 where LocalDrivingLicense_id = @LocalDrivingLicenseApplicationID and Result=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
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

            return PassedTestCount;
        }

        public static bool GetTestInfoByID(int TestID,
           ref int TestAppointmentID, ref bool TestResult,
           ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "SELECT * FROM Tests WHERE Test_ID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointment_id"];
                    TestResult = (bool)reader["Result"];
                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

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
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
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

            return Result;

        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult,
             string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"Insert Into Tests (TestAppointment_id,Result,
                                                Notes,   CreatedByUser_id)
                            Values (@TestAppointmentID,@TestResult,
                                                @Notes,   @CreatedByUserID);
                            
                                UPDATE TestAppointments 
                                SET IsLocked=1 where TestAppointment_ID = @TestAppointmentID;

                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);



            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
        // public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
        //     (int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
        //       ref int TestAppointmentID, ref bool TestResult,
        //       ref string Notes, ref int CreatedByUserID)
        // {
        //     bool isFound = false;

        //     SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //     string query = @"SELECT  top 1 Tests.TestID, 
        //         Tests.TestAppointmentID, Tests.TestResult, 
        //Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
        //         FROM            LocalDrivingLicenseApplications INNER JOIN
        //                                  Tests INNER JOIN
        //                                  TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
        //                                  Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
        //         WHERE        (Applications.ApplicantPersonID = @PersonID) 
        //                 AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
        //                 AND ( TestAppointments.TestTypeID=@TestTypeID)
        //         ORDER BY Tests.TestAppointmentID DESC";

        //     SqlCommand command = new SqlCommand(query, connection);

        //     command.Parameters.AddWithValue("@PersonID", PersonID);
        //     command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
        //     command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

        //     try
        //     {
        //         connection.Open();
        //         SqlDataReader reader = command.ExecuteReader();

        //         if (reader.Read())
        //         {

        //             // The record was found
        //             isFound = true;
        //             TestID = (int)reader["TestID"];
        //             TestAppointmentID = (int)reader["TestAppointmentID"];
        //             TestResult = (bool)reader["TestResult"];
        //             if (reader["Notes"] == DBNull.Value)

        //                 Notes = "";
        //             else
        //                 Notes = (string)reader["Notes"];

        //             CreatedByUserID = (int)reader["CreatedByUserID"];

        //         }
        //         else
        //         {
        //             // The record was not found
        //             isFound = false;
        //         }

        //         reader.Close();


        //     }
        //     catch (Exception ex)
        //     {
        //         //Console.WriteLine("Error: " + ex.Message);
        //         isFound = false;
        //     }
        //     finally
        //     {
        //         connection.Close();
        //     }

        //     return isFound;
        // }


        // public static DataTable GetAllTests()
        // {

        //     DataTable dt = new DataTable();
        //     SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //     string query = "SELECT * FROM Tests order by TestID";

        //     SqlCommand command = new SqlCommand(query, connection);

        //     try
        //     {
        //         connection.Open();

        //         SqlDataReader reader = command.ExecuteReader();

        //         if (reader.HasRows)

        //         {
        //             dt.Load(reader);
        //         }

        //         reader.Close();


        //     }

        //     catch (Exception ex)
        //     {
        //         // Console.WriteLine("Error: " + ex.Message);
        //     }
        //     finally
        //     {
        //         connection.Close();
        //     }

        //     return dt;

        // }


        //public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult,
        //     string Notes, int CreatedByUserID)
        //{

        //    int rowsAffected = 0;
        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"Update  Tests  
        //                    set TestAppointmentID = @TestAppointmentID,
        //                        TestResult=@TestResult,
        //                        Notes = @Notes,
        //                        CreatedByUserID=@CreatedByUserID
        //                        where TestID = @TestID";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@TestID", TestID);
        //    command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
        //    command.Parameters.AddWithValue("@TestResult", TestResult);
        //    command.Parameters.AddWithValue("@Notes", Notes);
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
