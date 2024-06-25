using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rentalSystemMain
{
    public partial class transactionHistory : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        public transactionHistory()
        {
            InitializeComponent();
            ShowTransactionHistory();
        }


        public void ShowTransactionHistory()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT InvoiceID as [Invoice ID], CustomerName as [Customer Name], TransactionStatus as Status FROM TransactionHistory ", con);
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            ShowTransactionHistory();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                string customerName = selectedRow.Cells["Customer Name"].Value.ToString();
                int invoiceID = Convert.ToInt32(selectedRow.Cells["Invoice ID"].Value);

                viewTransactionHistory viewTransactionHistory = new viewTransactionHistory(customerName, invoiceID);
                viewTransactionHistory.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string findInfo = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(findInfo))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("SELECT InvoiceID as [Invoice ID], CustomerName as [Customer Name], TransactionStatus as Status FROM TransactionHistory WHERE CustomerName LIKE @SearchValue", con);
                    command.Parameters.AddWithValue("@SearchValue", "%" + findInfo + "%");
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                ShowTransactionHistory();
            }
        }
    }
}
