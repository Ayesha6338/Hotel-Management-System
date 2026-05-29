using System.Collections.Generic;
using System.Data.SQLite;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Database
{
    public class PaymentRepository : BaseRepository
    {
        public List<Payment> GetAllPayments()
        {
            string sql = "SELECT Id, CustomerName, RoomCharges, Days, TotalBill FROM Payments ORDER BY Id DESC";
            return ExecuteList(sql, reader => new Payment
            {
                Id = GetInt(reader, "Id"),
                CustomerName = GetString(reader, "CustomerName"),
                RoomCharges = GetString(reader, "RoomCharges"),
                Days = GetString(reader, "Days"),
                TotalBill = GetString(reader, "TotalBill")
            });
        }

        public bool AddPayment(Payment payment)
        {
            string sql = "INSERT INTO Payments (CustomerName, RoomCharges, Days, TotalBill) VALUES (@cust, @charges, @days, @bill)";
            var p = new SQLiteParameter[] {
                new SQLiteParameter("@cust", payment.CustomerName),
                new SQLiteParameter("@charges", payment.RoomCharges),
                new SQLiteParameter("@days", payment.Days),
                new SQLiteParameter("@bill", payment.TotalBill)
            };
            return ExecuteNonQuery(sql, p) > 0;
        }

        public bool UpdatePayment(Payment payment)
        {
            string sql = "UPDATE Payments SET CustomerName=@cust, RoomCharges=@charges, Days=@days, TotalBill=@bill WHERE Id=@id";
            var p = new SQLiteParameter[] {
                new SQLiteParameter("@cust", payment.CustomerName),
                new SQLiteParameter("@charges", payment.RoomCharges),
                new SQLiteParameter("@days", payment.Days),
                new SQLiteParameter("@bill", payment.TotalBill),
                new SQLiteParameter("@id", payment.Id)
            };
            return ExecuteNonQuery(sql, p) > 0;
        }

        public bool DeletePayment(int id)
        {
            string sql = "DELETE FROM Payments WHERE Id=@id";
            return ExecuteNonQuery(sql, new SQLiteParameter[] { new SQLiteParameter("@id", id) }) > 0;
        }
    }
}