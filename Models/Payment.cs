namespace HotelManagementSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string RoomCharges { get; set; } = string.Empty;
        public string Days { get; set; } = string.Empty;
        public string TotalBill { get; set; } = string.Empty;
    }
}