using System.Collections.Generic;
using System.Data.SQLite;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Database
{
    public class CustomerRepository : BaseRepository
    {
        public List<Customer> GetAllCustomers()
        {
            string sql = "SELECT Id, CustomerName, CNIC, Phone FROM Customers ORDER BY CustomerName";
            return ExecuteList(sql, reader => new Customer
            {
                Id = GetInt(reader, "Id"),
                CustomerName = GetString(reader, "CustomerName"),
                CNIC = GetString(reader, "CNIC"),
                Phone = GetString(reader, "Phone")
            });
        }

        public bool AddCustomer(Customer customer)
        {
            string sql = "INSERT INTO Customers (CustomerName, CNIC, Phone) VALUES (@name, @cnic, @phone)";
            var p = new SQLiteParameter[] {
                new SQLiteParameter("@name", customer.CustomerName),
                new SQLiteParameter("@cnic", customer.CNIC),
                new SQLiteParameter("@phone", customer.Phone)
            };
            return ExecuteNonQuery(sql, p) > 0;
        }

        public bool UpdateCustomer(Customer customer)
        {
            string sql = "UPDATE Customers SET CustomerName=@name, CNIC=@cnic, Phone=@phone WHERE Id=@id";
            var p = new SQLiteParameter[] {
                new SQLiteParameter("@name", customer.CustomerName),
                new SQLiteParameter("@cnic", customer.CNIC),
                new SQLiteParameter("@phone", customer.Phone),
                new SQLiteParameter("@id", customer.Id)
            };
            return ExecuteNonQuery(sql, p) > 0;
        }

        public bool DeleteCustomer(int id)
        {
            string sql = "DELETE FROM Customers WHERE Id=@id";
            return ExecuteNonQuery(sql, new SQLiteParameter[] { new SQLiteParameter("@id", id) }) > 0;
        }

        public int GetCustomerCount()
        {
            object result = ExecuteScalar("SELECT COUNT(*) FROM Customers");
            return result != null ? (int)(long)result : 0;
        }
    }
}