using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Data_Access_Layer
{
    internal class clsConnection
    {
        private static string _connectionString;
        private static readonly object _lock = new object();

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    lock (_lock)
                    {
                        if (_connectionString == null)
                        {
                            try
                            {
                                // Get the base directory of the application
                                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                                // Go up three levels to reach the project root (from bin/Debug)
                                string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\.."));

                                // Build configuration from appsettings.json
                                var configuration = new ConfigurationBuilder()
                                    .SetBasePath(projectRoot)
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .Build();

                                // Get connection string from configuration
                                _connectionString = configuration.GetConnectionString("DefaultConnection");

                                if (string.IsNullOrEmpty(_connectionString))
                                {
                                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json");
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new InvalidOperationException($"Failed to load connection string from appsettings.json. Error: {ex.Message}", ex);
                            }
                        }
                    }
                }
                return _connectionString;
            }
        }
    }
}
