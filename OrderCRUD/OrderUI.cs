using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderCRUD.BLL;

namespace OrderCRUD
{
    public partial class OrderUI : Form
    {
        OrderManager _orderManager = new OrderManager();
        string connectionString = @"Server = HABIB; Database = CoffeeShop; Integrated Security = true";

        public OrderUI()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            cNameComboBox.Text = "";
            iComboBox.Text = "";
            priceTextBox.Clear();
            quantityTextBox.Clear();
        }



        private void saveButton_Click(object sender, EventArgs e)
        {
            string customerName = cNameComboBox.Text;
            string itemName = iComboBox.Text;
            string price = priceTextBox.Text;
            string qty = quantityTextBox.Text;

            if (customerName == "" || itemName == "" || priceTextBox.Text == "" || quantityTextBox.Text == "")
            {
                MessageBox.Show("Field must not be empty..");
                return;
            }
            else if (_orderManager.CheckIfNumeric(customerName))
            {
                MessageBox.Show("Please enter Customer name, not numeric value.");
                cNameComboBox.Text = "";
                return;
            }
            if (_orderManager.IsNameExist(cNameComboBox.Text))
            {
                MessageBox.Show("This Customer is Already exist..");
                cNameComboBox.Text = "";
                return;
            }

            if (!_orderManager.CheckIfNumeric(priceTextBox.Text))
            {
                MessageBox.Show("Please enter numeric price value.");
                priceTextBox.Text = "";
                return;
            }
            else if (!_orderManager.CheckIfNumeric(quantityTextBox.Text))
            {
                MessageBox.Show("Please enter numeric Quantity value.");
                quantityTextBox.Text = "";
                return;
            }

            if (_orderManager.Add(customerName, itemName, Convert.ToDouble(price), Convert.ToInt32(qty)))
            {
                MessageBox.Show("Saved");
                showDataGridView.DataSource = _orderManager.Display();
                return;
            }
            else
            {
                MessageBox.Show("Not Save");
                return;
            }

        }

        private void showButton_Click(object sender, EventArgs e)
        {
            showDataGridView.DataSource = _orderManager.Display();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string customerName = cNameComboBox.Text;
            string itemName = iComboBox.Text;
            string price = priceTextBox.Text;
            string qty = quantityTextBox.Text;
            string id = idTextBox.Text;

            if (cNameComboBox.Text == "" || iComboBox.Text == "" || priceTextBox.Text == "" || quantityTextBox.Text == "")
            {
                MessageBox.Show("Field must not be empty..");
                return;
            }
            else if (_orderManager.CheckIfNumeric(cNameComboBox.Text))
            {
                MessageBox.Show("Please enter Customer name, not numeric value.");
                cNameComboBox.Text = "";
                return;
            }

            if (!_orderManager.CheckIfNumeric(priceTextBox.Text))
            {
                MessageBox.Show("Please enter numeric price value.");
                priceTextBox.Text = "";
                return;
            }
            else if (!_orderManager.CheckIfNumeric(quantityTextBox.Text))
            {
                MessageBox.Show("Please enter numeric Quantity value.");
                quantityTextBox.Text = "";
                return;
            }

            if (_orderManager.Update(Convert.ToInt32(id), customerName, itemName, Convert.ToDouble(price), Convert.ToInt32(qty)))
            {
                MessageBox.Show("Updated");
                showDataGridView.DataSource = _orderManager.Display();
                return;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idTextBox.Text);
            if (idTextBox.Text == "")
            {
                MessageBox.Show("Please Select Id Field..");
                return;
            }

            if (_orderManager.Delete(id))
            {
                MessageBox.Show("Deleted");
            }
            else
            {
                MessageBox.Show("Not Deleted");
            }
        }

        //int id;
        private void showDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Clear();
            idTextBox.Text = showDataGridView[0, e.RowIndex].Value.ToString();
            cNameComboBox.Text = showDataGridView[1, e.RowIndex].Value.ToString();
            iComboBox.Text = showDataGridView[2, e.RowIndex].Value.ToString();
            priceTextBox.Text = showDataGridView[3, e.RowIndex].Value.ToString();
            quantityTextBox.Text = showDataGridView[4, e.RowIndex].Value.ToString();
            //id = Convert.ToInt32(showDataGridView[0, e.RowIndex].Value);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string name = searchTextBox.Text;
            if (searchTextBox.Text == "")
            {
                MessageBox.Show("Field must not be empty..");
                return;
            }

            showDataGridView.DataSource = "";
            showDataGridView.DataSource = _orderManager.Search(name);
            return;
        }
    }

}
