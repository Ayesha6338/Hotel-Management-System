using System;
using System.Windows.Forms;
using HotelManagementSystem.Database;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerRepository _repository;
        private int _selectedId = 0;

        public CustomerForm()
        {
            InitializeComponent();
            _repository = new CustomerRepository();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            dataGridView1.Rows.Clear();
            var customers = _repository.GetAllCustomers();
            foreach (var c in customers)
                dataGridView1.Rows.Add(c.Id, c.CustomerName, c.CNIC, c.Phone);
        }
        //CRUD operations
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Please enter customer name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var customer = new Customer
            {
                CustomerName = txtCustomerName.Text.Trim(),
                CNIC = txtCNIC.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };
            if (_repository.AddCustomer(customer))
            {
                LoadCustomers();
                ClearFields();
                MessageBox.Show("Customer Added Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Please select a customer to update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var customer = new Customer
            {
                Id = _selectedId,
                CustomerName = txtCustomerName.Text.Trim(),
                CNIC = txtCNIC.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };
            if (_repository.UpdateCustomer(customer))
            {
                LoadCustomers();
                ClearFields();
                MessageBox.Show("Customer Updated Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show("Delete this customer?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                if (_repository.DeleteCustomer(_selectedId))
                {
                    LoadCustomers();
                    ClearFields();
                    MessageBox.Show("Customer Deleted Successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

       
        private void ClearFields()
        {
            txtCustomerName.Clear();
            txtCNIC.Clear();
            txtPhone.Clear();
            _selectedId = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells[0].Value);
                txtCustomerName.Text = row.Cells[1].Value?.ToString();
                txtCNIC.Text = row.Cells[2].Value?.ToString();
                txtPhone.Text = row.Cells[3].Value?.ToString();
            }
        }

        // Empty handlers — designer ke liye zaroor rakhein
        private void txtCNIC_TextChanged(object sender, EventArgs e) { }
        private void txtPhone_TextChanged(object sender, EventArgs e) { }
        private void txtCustomerName_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}