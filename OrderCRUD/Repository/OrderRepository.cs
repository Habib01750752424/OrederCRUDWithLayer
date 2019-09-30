using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderCRUD.Repository
{
    public class OrderRepository
    {
        string connectionString = @"Server = HABIB; Database = CoffeeShop; Integrated Security = true";
        public bool CheckIfNumeric(string input)
        {
            return input.IsNumeric();
        }

        public bool Add(string customerName, string itemName, double price, int qty)
        {
            bool isAdd = false;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                string commandString = "INSERT INTO Orders(CustomerName, ItemName, Price, Quantity) VALUES('" + customerName + "', '" + itemName + "'," + price + "," + qty + ")";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlConnection.Open();
                int isExecute = sqlCommand.ExecuteNonQuery();
                if (isExecute > 0)
                {
                    isAdd = true;
                }
                sqlConnection.Close();
            }
            catch (Exception exep)
            {
                //MessageBox.Show(exep.Message);
            }
            return isAdd;
        }

        public DataTable Display()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string commandString = @"SELECT ID,CustomerName,ItemName,Price,Quantity,Quantity*Price AS TotalCost FROM Orders";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool Update(int id, string customerName, string itemName, double price, int qty)
        {
            bool isUpdate = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string commandString = "UPDATE Orders SET CustomerName = '" + customerName + "', ItemName = '" + itemName + "',Price = " + price + ",Quantity = " + qty + "" +
                "WHERE ID = " + id + "";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int isExecute = sqlCommand.ExecuteNonQuery();

            if (isExecute > 0)
            {
                isUpdate = true;
            }
            sqlConnection.Close();
            return isUpdate;
        }

        public bool IsNameExist(string customerName)
        {
            bool existName = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string commandString = @"SELECT CustomerName FROM Orders WHERE CustomerName = '" + customerName + "'";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);


            if (isFill > 0)
            {
                existName = true;
            }

            return existName;
        }

        public bool Delete(int id)
        {
            bool isDelete = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string commandString = "DELETE FROM Orders WHERE ID = " + id + "";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlConnection.Open();
            int isExecute = sqlCommand.ExecuteNonQuery();

            if (isExecute > 0)
            {
                isDelete = true;
            }
            sqlConnection.Close();
            return isDelete;
        }

        public DataTable Search(string name)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string commandString = @"SELECT ID,CustomerName,ItemName,Price,Quantity,Quantity*Price AS TotalCost FROM Orders WHERE CustomerName = '" + name + "' OR ItemName = '" + name + "'";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }



    public static class StringExtensions
    {
        public static bool IsNumeric(this string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }
    }
}
