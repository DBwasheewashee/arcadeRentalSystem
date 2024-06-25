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
    public partial class viewTransactionHistory : Form
    {
        private string customerName;
        private int invoiceID;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");
        public viewTransactionHistory(string customerName, int invoiceID)
        {
            InitializeComponent();
            this.customerName = customerName;
            this.invoiceID = invoiceID;
            ShowCustomerTransactionHistory(customerName, invoiceID);

        }

        private void ShowCustomerTransactionHistory(string customerName, int invoiceID)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TransactionHistory WHERE InvoiceID = @InvoiceID AND CustomerName = @CustomerName", con);
                command.Parameters.AddWithValue("@InvoiceID", invoiceID);
                command.Parameters.AddWithValue("@CustomerName", customerName);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    label18.Text = reader["InvoiceID"].ToString();
                    label19.Text = reader["TransactionStatus"].ToString();
                    label20.Text = ((DateTime)reader["RentalStart"]).ToString("yyyy-MM-dd");
                    label21.Text = ((DateTime)reader["RentalEnd"]).ToString("yyyy-MM-dd");
                    label22.Text = reader["RentDays"].ToString();
                    label23.Text = reader["CustomerName"].ToString();
                    label24.Text = reader["CustomerPhone"].ToString();
                    label25.Text = reader["CustomerEmail"].ToString();
                    label26.Text = reader["CustomerAddress"].ToString();
                    label27.Text = reader["Subtotal"].ToString();
                    label28.Text = reader["Discount"].ToString();
                    label29.Text = reader["TotalAmount"].ToString();
                    label30.Text = reader["CustomerPayment"].ToString();
                    label31.Text = reader["Penalty"].ToString();
                    label32.Text = reader["EmployeeName"].ToString();
                    label33.Text = reader["ReceivedBy"].ToString();


                    string listBoxItems = reader["ListBoxItems"].ToString();
                    string[] itemsArray = listBoxItems.Split(',');

                    foreach (string item in itemsArray)
                    {
                        listBox1.Items.Add(item.Trim());
                    }

                }

            }
            catch(Exception ex) 
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
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
