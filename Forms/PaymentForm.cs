using System;
using System.Windows.Forms;
using HotelManagementSystem.Database;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Forms
{
    public partial class PaymentForm : Form
    {
        private readonly PaymentRepository _repository;
        private int _selectedId = 0;

        public PaymentForm()
        {
            InitializeComponent();
            _repository = new PaymentRepository();
            LoadPayments();
        }

        private void LoadPayments()
        {
            dataGridView1.Rows.Clear();
            var payments = _repository.GetAllPayments();
            foreach (var p in payments)
                dataGridView1.Rows.Add(p.Id, p.CustomerName, p.RoomCharges, p.Days, p.TotalBill);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int charges = Convert.ToInt32(txtCharges.Text);
                int days = Convert.ToInt32(txtDays.Text);
                txtBill.Text = (charges * days).ToString();
            }
            catch
            {
                MessageBox.Show("Please enter valid numbers for Charges and Days.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // CRUD operations for successful payments
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("Please enter customer name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var payment = new Payment
            {
                CustomerName = txtCustomer.Text.Trim(),
                RoomCharges = txtCharges.Text.Trim(),
                Days = txtDays.Text.Trim(),
                TotalBill = txtBill.Text.Trim()
            };
            if (_repository.AddPayment(payment))
            {
                LoadPayments();
                ClearFields();
                MessageBox.Show("Payment Added Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Please select a record to update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var payment = new Payment
            {
                Id = _selectedId,
                CustomerName = txtCustomer.Text.Trim(),
                RoomCharges = txtCharges.Text.Trim(),
                Days = txtDays.Text.Trim(),
                TotalBill = txtBill.Text.Trim()
            };
            if (_repository.UpdatePayment(payment))
            {
                LoadPayments();
                ClearFields();
                MessageBox.Show("Payment Updated Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Please select a record to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show("Delete this payment record?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                if (_repository.DeletePayment(_selectedId))
                {
                    LoadPayments();
                    ClearFields();
                    MessageBox.Show("Payment Deleted Successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ClearFields()
        {
            txtCustomer.Clear();
            txtCharges.Clear();
            txtDays.Clear();
            txtBill.Clear();
            _selectedId = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells[0].Value);
                txtCustomer.Text = row.Cells[1].Value?.ToString();
                txtCharges.Text = row.Cells[2].Value?.ToString();
                txtDays.Text = row.Cells[3].Value?.ToString();
                txtBill.Text = row.Cells[4].Value?.ToString();
            }
        }
    }
}