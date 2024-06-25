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
    public partial class viewEmployee : UserControl
    {
       private readonly SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        public viewEmployee()
        {
            InitializeComponent();
            ShowEmployee();

        }

        public void ShowEmployee()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT employee_fullname as Employee, employee_position as Position FROM empl_info", con);
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

        private void searchEmployee_Click(object sender, EventArgs e)
        {
            string searchValue = searchEmployee.Text.Trim();

            if (!string.IsNullOrEmpty(searchValue))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("SELECT employee_fullname as Employee, employee_position as Position FROM empl_info WHERE employee_fullname LIKE @SearchValue", con);
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
                ShowEmployee();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addEmployee addEmployee = new addEmployee(this);
            addEmployee.Show();
            ShowEmployee();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                string fullname = selectedRow.Cells["Employee"].Value.ToString();
                string position = selectedRow.Cells["Position"].Value.ToString();

                
                viewEmployeeInfo employeeInfo = new viewEmployeeInfo(fullname, position, this);
                employeeInfo.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) 
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string fullName = selectedRow.Cells["Employee"].Value.ToString();

                
                deleteEmployee deleteEmployee = new deleteEmployee(fullName, this);
                deleteEmployee.Show();
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            searchEmployee.Clear();
            ShowEmployee();
        }
    }
}