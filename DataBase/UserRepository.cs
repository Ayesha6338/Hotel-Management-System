using System.Data.SQLite;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Database
{
    using BCrypt = BCrypt.Net.BCrypt;

    public class UserRepository : BaseRepository
    {
        public User Authenticate(string username, string password)
        {
            string sql = @"SELECT UserId, Username, PasswordHash, FullName, UserType, IsActive
                           FROM Users WHERE Username = @username AND IsActive = 1";
            var p = new SQLiteParameter[] { new SQLiteParameter("@username", username) };

            var user = ExecuteSingle(sql, reader => new User
            {
                UserId = GetInt(reader, "UserId"),
                Username = GetString(reader, "Username"),
                PasswordHash = GetString(reader, "PasswordHash"),
                FullName = GetString(reader, "FullName"),
                UserType = GetString(reader, "UserType"),
                IsActive = GetInt(reader, "IsActive") == 1
            }, p);

            if (user != null && BCrypt.Verify(password, user.PasswordHash))
                return user;

            return null;
        }
    }
}