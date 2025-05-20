using System;
using System.Data;
using System.Data.SqlClient;

namespace Data_Access_Layer.Users
{
    public class clsUserData
    {
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "SELECT        Users.UserID, Users.Person_id, ( People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) as FullName," +
                " Users.UserName, Users.IsActive\r\nFROM            Users INNER JOIN\r\n                         People ON Users.Person_id = People.PersonID";
            SqlCommand Command = new SqlCommand(query, Connection);

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

        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"INSERT INTO Users (Person_id,UserName,Password,IsActive)
                           VALUES (@PersonId, @UserName, @Password, @IsActive);
                           SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonId", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }

            return UserID;
        }
        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"Update  Users  
                            set Person_id = @PersonID,
                                UserName = @UserName,                               
                                Password = @Password, 
                                IsActive = @IsActive                               
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);


#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : ", ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }
#pragma warning restore CS0168 // Variable is declared but never used

            return (RowsAffected > 0);
        }
        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @"Delete Users 
                                        where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }
#pragma warning restore CS0168 // Variable is declared but never used

            return (rowsAffected > 0);

        }
        public static bool ChangePassword(int UserID, string NewPassword)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = @" Update Users
                                set Password = @NewPassword
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NewPassword", NewPassword);
            command.Parameters.AddWithValue("@UserID", UserID);


#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                connection.Close();
            }
#pragma warning restore CS0168 // Variable is declared but never used

            return (RowsAffected > 0);
        }

        public static int IsUserExist(string Username, string Password)
        {
            bool IsFound = false;
            int UserID = -1;
            SqlConnection Connection = new SqlConnection(clsConnection.ConnectionString);
            string query = "select * from Users where UserName = @UserName and Password = @Password and IsActive = 'true'";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserName", Username);
            Command.Parameters.AddWithValue("@Password", Password);

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
#pragma warning restore CS0168 // Variable is declared but never used

            return UserID;
        }
        public static bool IsUserExist(string Username)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsConnection.ConnectionString);
            string query = "select Found = 1 from Users where UserName = @UserName";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserName", Username);

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                IsFound = reader.HasRows;

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
#pragma warning restore CS0168 // Variable is declared but never used

            return IsFound;
        }
        public static bool IsPersonUser(int PersonID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsConnection.ConnectionString);
            string query = "select * from Users where Person_id = @PersonID";
            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                IsFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
#pragma warning restore CS0168 // Variable is declared but never used

            return IsFound;
        }

        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password,
           ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserName = @Username and Password=@Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["Person_id"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


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
        public static bool GetUserInfoByID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsConnection.ConnectionString);

            string query = "SELECT * FROM Users where UserID = @ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", UserID);

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    IsFound = true;

                    PersonID = (int)reader["Person_id"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
#pragma warning restore CS0168 // Variable is declared but never used

            return IsFound;
        }

    }
}
