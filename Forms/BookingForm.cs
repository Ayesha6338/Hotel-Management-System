using System;
using System.Windows.Forms;
using HotelManagementSystem.Database;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Forms
{
    public partial class BookingForm : Form
    {
        private readonly BookingRepository _repository;
        private int _selectedId = 0;

        public BookingForm()
        {
            InitializeComponent();
            _repository = new BookingRepository();
            LoadBookings();
        }

        private void LoadBookings()
        {
            dataGridView1.Rows.Clear();
            var bookings = _repository.GetAllBookings();
            foreach (var b in bookings)
                dataGridView1.Rows.Add(b.Id, b.CustomerName, b.RoomNumber, b.Days);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("Please enter customer name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var booking = new Booking
            {
                CustomerName = txtCustomer.Text.Trim(),
                RoomNumber = txtRoom.Text.Trim(),
                Days = txtDays.Text.Trim()
            };
            if (_repository.AddBooking(booking))
            {
                LoadBookings();
                ClearFields();
                MessageBox.Show("Booking Added Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Please select a booking to update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var booking = new Booking
            {
                Id = _selectedId,
                CustomerName = txtCustomer.Text.Trim(),
                RoomNumber = txtRoom.Text.Trim(),
                Days = txtDays.Text.Trim()
            };
            if (_repository.UpdateBooking(booking))
            {
                LoadBookings();
                ClearFields();
                MessageBox.Show("Booking Updated Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Please select a booking to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show("Delete this booking?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                if (_repository.DeleteBooking(_selectedId))
                {
                    LoadBookings();
                    ClearFields();
                    MessageBox.Show("Booking Deleted Successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ClearFields()
        {
            txtCustomer.Clear();
            txtRoom.Clear();
            txtDays.Clear();
            _selectedId = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells[0].Value);
                txtCustomer.Text = row.Cells[1].Value?.ToString();
                txtRoom.Text = row.Cells[2].Value?.ToString();
                txtDays.Text = row.Cells[3].Value?.ToString();
            }
        }
    }
}