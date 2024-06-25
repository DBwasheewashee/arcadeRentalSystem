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
    public partial class onGoingTransactionFrontDesk : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");


        public onGoingTransactionFrontDesk()
        {
            InitializeComponent();
            ShowOngoingTransaction();
            dataGridView1.CellMouseDoubleClick -= dataGridView1_CellMouseDoubleClick;
            dataGridView1.CellMouseDoubleClick += dataGridView1_CellMouseDoubleClick;
        }

        public void ShowOngoingTransaction()
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            ShowOngoingTransaction();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(searchValue))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("SELECT InvoiceID as [Invoice ID], CustomerName as [Customer Name], TransactionStatus as Status FROM InvoiceTransactions WHERE CustomerName LIKE @SearchValue", con);
                    command.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");
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
                ShowOngoingTransaction();
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                if (selectedRow.Cells["Customer Name"].Value != null && selectedRow.Cells["Invoice ID"].Value != null)
                {
                    string customerName = selectedRow.Cells["Customer Name"].Value.ToString();
                    int invoiceID = Convert.ToInt32(selectedRow.Cells["Invoice ID"].Value);

                    viewTransactionFrontDesk viewTransactionFrontDesk = new viewTransactionFrontDesk(customerName, invoiceID, this, (this.ParentForm as frontDeskMainPanel)._adminFullname);
                    viewTransactionFrontDesk.Show();
                }
            }
        }
    }
}
