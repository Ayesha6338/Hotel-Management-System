using System;
using System.Data.SQLite;
using System.IO;

namespace HotelManagementSystem.Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            string dbFolder = Path.GetDirectoryName(DbConnection.DatabasePath);
            if (!Directory.Exists(dbFolder))
                Directory.CreateDirectory(dbFolder);

            if (!File.Exists(DbConnection.DatabasePath))
                SQLiteConnection.CreateFile(DbConnection.DatabasePath);

            CreateTables();
            CreateDefaultAdmin();
        }

        private static void CreateTables()
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string[] queries = {
                    @"CREATE TABLE IF NOT EXISTS Rooms (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        RoomNumber TEXT NOT NULL,
                        RoomType TEXT NOT NULL,
                        RoomStatus TEXT NOT NULL
                    )",
                    @"CREATE TABLE IF NOT EXISTS Customers (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerName TEXT NOT NULL,
                        CNIC TEXT NOT NULL,
                        Phone TEXT NOT NULL
                    )",
                    @"CREATE TABLE IF NOT EXISTS Bookings (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerName TEXT NOT NULL,
                        RoomNumber TEXT NOT NULL,
                        Days TEXT NOT NULL
                    )",
                    @"CREATE TABLE IF NOT EXISTS Payments (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerName TEXT NOT NULL,
                        RoomCharges TEXT NOT NULL,
                        Days TEXT NOT NULL,
                        TotalBill TEXT NOT NULL
                    )",
                    @"CREATE TABLE IF NOT EXISTS Users (
                        UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL UNIQUE,
                        PasswordHash TEXT NOT NULL,
                        FullName TEXT NOT NULL,
                        UserType TEXT NOT NULL DEFAULT 'user',
                        IsActive INTEGER NOT NULL DEFAULT 1,
                        CreatedDate TEXT NOT NULL
                    )"
                };

                foreach (var query in queries)
                    using (var cmd = new SQLiteCommand(query, conn))
                        cmd.ExecuteNonQuery();
            }
        }

        private static void CreateDefaultAdmin()
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = 'admin'";
                using (var cmd = new SQLiteCommand(checkSql, conn))
                {
                    long count = (long)cmd.ExecuteScalar();
                    if (count > 0) return;
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword("admin123", workFactor: 11);
                string insertSql = @"INSERT INTO Users (Username, PasswordHash, FullName, UserType, IsActive, CreatedDate)
                                     VALUES ('admin', @hash, 'Administrator', 'admin', 1, @date)";
                using (var cmd = new SQLiteCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@hash", passwordHash);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}