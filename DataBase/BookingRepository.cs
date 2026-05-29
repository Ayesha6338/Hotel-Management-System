using System.Collections.Generic;
using System.Data.SQLite;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Database
{
    public class BookingRepository : BaseRepository
    {
        public List<Booking> GetAllBookings()
        {
            string sql = "SELECT Id, CustomerName, RoomNumber, Days FROM Bookings ORDER BY Id DESC";
            return ExecuteList(sql, reader => new Booking
            {
                Id = GetInt(reader, "Id"),
                CustomerName = GetString(reader, "CustomerName"),
                RoomNumber = GetString(reader, "RoomNumber"),
                Days = GetString(reader, "Days")
            });
        }

        public bool AddBooking(Booking booking)
        {
            string sql = "INSERT INTO Bookings (CustomerName, RoomNumber, Days) VALUES (@cust, @room, @days)";
            var p = new SQLiteParameter[] {
                new SQLiteParameter("@cust", booking.CustomerName),
                new SQLiteParameter("@room", booking.RoomNumber),
                new SQLiteParameter("@days", booking.Days)
            };
            return ExecuteNonQuery(sql, p) > 0;
        }

        public bool UpdateBooking(Booking booking)
        {
            string sql = "UPDATE Bookings SET CustomerName=@cust, RoomNumber=@room, Days=@days WHERE Id=@id";
            var p = new SQLiteParameter[] {
                new SQLiteParameter("@cust", booking.CustomerName),
                new SQLiteParameter("@room", booking.RoomNumber),
                new SQLiteParameter("@days", booking.Days),
                new SQLiteParameter("@id", booking.Id)
            };
            return ExecuteNonQuery(sql, p) > 0;
        }

        public bool DeleteBooking(int id)
        {
            string sql = "DELETE FROM Bookings WHERE Id=@id";
            return ExecuteNonQuery(sql, new SQLiteParameter[] { new SQLiteParameter("@id", id) }) > 0;
        }
    }
}