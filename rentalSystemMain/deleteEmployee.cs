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
    public partial class deleteEmployee : Form
    {
        private readonly SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        private readonly viewEmployee _viewEmployee;

        private string employeeFullName;

        public deleteEmployee(string fullName, viewEmployee viewEmployeeInstance)
        {
            InitializeComponent();
            employeeFullName = fullName;
            _viewEmployee = viewEmployeeInstance;

            PopulateComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
               
                try
                {
                    con.Open();

                    
                    string selectedFullName = comboBox1.SelectedItem.ToString();

                  
                    SqlCommand deleteAccCommand = new SqlCommand("DELETE FROM empl_acc WHERE employee_id = (SELECT employee_id FROM empl_info WHERE employee_fullname = @EmployeeFullName)", con);
                    deleteAccCommand.Parameters.AddWithValue("@EmployeeFullName", selectedFullName);
                    deleteAccCommand.ExecuteNonQuery();

                    
                    SqlCommand deleteInfoCommand = new SqlCommand("DELETE FROM empl_info WHERE employee_fullname = @EmployeeFullName", con);
                    deleteInfoCommand.Parameters.AddWithValue("@EmployeeFullName", selectedFullName);
                    deleteInfoCommand.ExecuteNonQuery();

                    MessageBox.Show("Employee deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    _viewEmployee.ShowEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }

                
                this.Close();
            }
        }

        private void PopulateComboBox()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT employee_fullname FROM empl_info", con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["employee_fullname"].ToString());
                }
                reader.Close();
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
    }
}
