using Bussiness_Layer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_License_Management_Project.GlobalClasses
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static void RememberUsernameAndPasswordInRegistry(string txtUserName, string txtPassword)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\YourSoftware";
            string UserName = "UserName";
            string Password = "Password";
            string UserNameData = txtUserName;
            string PasswordData = txtPassword;
            //try
            //{
                // Write the value to the Registry
                Registry.SetValue(keyPath, UserName, UserNameData, RegistryValueKind.String);
                Registry.SetValue(keyPath, Password, PasswordData, RegistryValueKind.String);


                // Console.WriteLine($"Value {valueName} successfully written to the Registry.");
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine($"An error occurred: {ex.Message}");
            //}
        }
        public static bool GetStoredCredentialFromRegistry(ref string txtUsername, ref string txtPassword)
        {

            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\YourSoftware";
            string UserName = "UserName";
            string Password = "Password";

           // try
            //{
                // Read the value from the Registry
                string UserNameVal = Registry.GetValue(keyPath, UserName, null) as string;
                string PasswordVal = Registry.GetValue(keyPath, Password, null) as string;


                if (UserNameVal != null && PasswordVal != null)
                {
                txtUsername = UserNameVal;
                txtPassword = PasswordVal;
                    return true;
                    //Console.WriteLine($"The value of {valueName} is: {value}");
                }
                else
                {
                    return false;
                    //Console.WriteLine($"Value {valueName} not found in the Registry.");
                }

            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine($"An error occurred: {ex.Message}");
            //}
        }
        public static void SaveInEventLog(string ErrorMessage, EventLogEntryType eventLogEntryType, string sourceName = "DVLDApp")
        {

            // Create the event source if it does not exist
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
                //Console.WriteLine("Event source created.");
            }
            EventLog.WriteEntry(sourceName, ErrorMessage, eventLogEntryType);
        }






        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }
    }
}
