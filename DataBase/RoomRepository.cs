using System.Collections.Generic;
using System.Data.SQLite;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Database
{
    public class RoomRepository : BaseRepository
    {
        public List<Room> GetAllRooms()
        {
            string sql = "SELECT Id, RoomNumber, RoomType, RoomStatus FROM Rooms ORDER BY RoomNumber";
            return ExecuteList(sql, reader => new Room
            {
                Id = GetInt(reader, "Id"),
                RoomNumber = GetString(reader, "RoomNumber"),
                RoomType = GetString(reader, "RoomType"),
                RoomStatus = GetString(reader, "RoomStatus")
            });
        }

        public bool AddRoom(Room room)
        {
            string sql = "INSERT INTO Rooms (RoomNumber, RoomType, RoomStatus) VALUES (@num, @type, @status)";
            var p = new SQLiteParameter[] {
                new SQLiteParameter("@num", room.RoomNumber),
                new SQLiteParameter("@type", room.RoomType),
                new SQLiteParameter("@status", room.RoomStatus)
            };
            return ExecuteNonQuery(sql, p) > 0;
        }

        public bool UpdateRoom(Room room)
        {
            string sql = "UPDATE Rooms SET RoomNumber=@num, RoomType=@type, RoomStatus=@status WHERE Id=@id";
            var p = new SQLiteParameter[] {
                new SQLiteParameter("@num", room.RoomNumber),
                new SQLiteParameter("@type", room.RoomType),
                new SQLiteParameter("@status", room.RoomStatus),
                new SQLiteParameter("@id", room.Id)
            };
            return ExecuteNonQuery(sql, p) > 0;
        }

        public bool DeleteRoom(int id)
        {
            string sql = "DELETE FROM Rooms WHERE Id=@id";
            return ExecuteNonQuery(sql, new SQLiteParameter[] { new SQLiteParameter("@id", id) }) > 0;
        }

        public int GetRoomCount()
        {
            object result = ExecuteScalar("SELECT COUNT(*) FROM Rooms");
            return result != null ? (int)(long)result : 0;
        }
    }
}