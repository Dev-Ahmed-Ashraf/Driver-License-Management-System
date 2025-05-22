using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Data_Access_Layer
{
    public static class clsConnection
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
                                // Build configuration from appsettings.json
                                var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
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
                                throw new InvalidOperationException("Failed to load connection string from appsettings.json", ex);
                            }
                        }
                    }
                }
                return _connectionString;
            }
        }
    }
} 