namespace HotelManagementSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string RoomNumber { get; set; } = string.Empty;
        public string Days { get; set; } = string.Empty;
    }
}