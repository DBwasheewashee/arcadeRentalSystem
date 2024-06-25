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
    public partial class ongoingTransactions : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        private Timer refreshTimer;

        public ongoingTransactions()
        {
            InitializeComponent();
            ShowOngoingTransactions();
        //    InitializeTimer();
        }

        /*   private void InitializeTimer()
           {
               refreshTimer = new Timer();
               refreshTimer.Interval = 1000; // 1 second interval
               refreshTimer.Tick += RefreshTimer_Tick;
               refreshTimer.Start(); // Start the timer // para di na need ng maual refresh ng user
           }
        */

        /*   private void RefreshTimer_Tick(object sender, EventArgs e)
           {
               ShowOngoingTransactions();
         } */
        public void ShowOngoingTransactions()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT InvoiceID as [Invoice ID], CustomerName as [Customer Name], TransactionStatus as Status FROM InvoiceTransactions ", con);
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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                string customerName = selectedRow.Cells["Customer Name"].Value.ToString();
                int invoiceID = Convert.ToInt32(selectedRow.Cells["Invoice ID"].Value);

                viewTransaction viewTransaction = new viewTransaction(customerName, invoiceID, this, (this.ParentForm as adminMainPanel)._adminFullname);
                viewTransaction.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchOngoingTransaction.Clear();
            ShowOngoingTransactions();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string findInfo = searchOngoingTransaction.Text.Trim();

            if (!string.IsNullOrEmpty(findInfo))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("SELECT InvoiceID as [Invoice ID], CustomerName as [Customer Name], TransactionStatus as Status FROM InvoiceTransactions WHERE CustomerName LIKE @SearchValue", con);
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
                ShowOngoingTransactions();
            }
        }
    }

    
}
