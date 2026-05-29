using System;
using System.Data.SQLite;
using System.IO;

namespace HotelManagementSystem.Database
{
    public static class DbConnection
    {
        private static string _connectionString = string.Empty;

        public static string DatabasePath
        {
            get
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(baseDirectory, "Database", "hms.db");
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = $"Data Source={DatabasePath};Version=3;";
                return _connectionString;
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}