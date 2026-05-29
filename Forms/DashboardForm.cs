using System;
using System.Windows.Forms;
using HotelManagementSystem.Database;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly User _currentUser;
        private readonly RoomRepository _roomRepo;
        private readonly CustomerRepository _customerRepo;

        public DashboardForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _roomRepo = new RoomRepository();
            _customerRepo = new CustomerRepository();

            lblWelcome.Text = $"Welcome, {_currentUser.FullName}! ({_currentUser.UserType.ToUpper()})";
            LoadStats();
        }

        private void LoadStats()
        {
            try
            {
                lblRoomCount.Text = "Total Rooms: " + _roomRepo.GetRoomCount();
                lblCustomerCount.Text = "Total Customers: " + _customerRepo.GetCustomerCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            RoomForm room = new RoomForm();
            room.ShowDialog();
            LoadStats();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customer = new CustomerForm();
            customer.ShowDialog();
            LoadStats();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            BookingForm booking = new BookingForm();
            booking.ShowDialog();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentForm payment = new PaymentForm();
            payment.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to logout?",
                "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                LoginForm login = new LoginForm();
                login.Show();
                this.Close();
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}