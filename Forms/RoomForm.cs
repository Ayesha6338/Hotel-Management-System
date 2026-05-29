using System;
using System.Windows.Forms;
using HotelManagementSystem.Database;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Forms
{
    public partial class RoomForm : Form
    {
        private readonly RoomRepository _repository;
        private int _selectedId = 0;

        public RoomForm()
        {
            InitializeComponent();
            _repository = new RoomRepository();
            LoadRooms();
        }

        private void LoadRooms()
        {
            dataGridView1.Rows.Clear();
            var rooms = _repository.GetAllRooms();
            foreach (var r in rooms)
                dataGridView1.Rows.Add(r.Id, r.RoomNumber, r.RoomType, r.RoomStatus);
        }
        // CRUD operations for managing Rooms

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                MessageBox.Show("Please enter room number.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var room = new Room
            {
                RoomNumber = txtRoomNumber.Text.Trim(),
                RoomType = cmbRoomType.Text,
                RoomStatus = cmbRoomStatus.Text
            };
            if (_repository.AddRoom(room))
            {
                LoadRooms();
                ClearFields();
                MessageBox.Show("Room Added Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Please select a room to update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var room = new Room
            {
                Id = _selectedId,
                RoomNumber = txtRoomNumber.Text.Trim(),
                RoomType = cmbRoomType.Text,
                RoomStatus = cmbRoomStatus.Text
            };
            if (_repository.UpdateRoom(room))
            {
                LoadRooms();
                ClearFields();
                MessageBox.Show("Room Updated Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Please select a room to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show("Delete this room?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                if (_repository.DeleteRoom(_selectedId))
                {
                    LoadRooms();
                    ClearFields();
                    MessageBox.Show("Room Deleted Successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void ClearFields()
        {
            txtRoomNumber.Clear();
            cmbRoomType.SelectedIndex = -1;
            cmbRoomStatus.SelectedIndex = -1;
            _selectedId = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells[0].Value);
                txtRoomNumber.Text = row.Cells[1].Value?.ToString();
                cmbRoomType.Text = row.Cells[2].Value?.ToString();
                cmbRoomStatus.Text = row.Cells[3].Value?.ToString();
            }
        }
    }
}