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
    public partial class inventoryLoginForm : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        public inventoryLoginForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show(); 
        }

  
        private void button1_Click_1(object sender, EventArgs e)
        {

            string username = textBox1.Text;
            string password = textBox2.Text;
            string adminFullname = null;

            try
            {
                con.Open();
                SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM empl_acc WHERE employee_username = @Username AND employee_password = @Password", con);
                cmdCount.Parameters.AddWithValue("@Username", username);
                cmdCount.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(cmdCount.ExecuteScalar());

                if (count > 0)
                {
                    SqlCommand cmdFullname = new SqlCommand("SELECT employee_fullname FROM empl_info INNER JOIN empl_acc ON empl_info.employee_id = empl_acc.employee_id WHERE empl_acc.employee_username = @Username AND empl_acc.employee_password = @Password AND (empl_info.employee_position = 'Inventory Manager' OR empl_info.employee_position = 'Operations Supervisor')", con);
                    cmdFullname.Parameters.AddWithValue("@Username", username);
                    cmdFullname.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmdFullname.ExecuteReader();
                    if (reader.Read())
                    {
                        adminFullname = reader["employee_fullname"].ToString();
                    }

                    reader.Close();

                    if (!string.IsNullOrEmpty(adminFullname))
                    {
                        MessageBox.Show("Login successful!");

                        // Pass the employee's fullname to the main panel constructor
                        inventoryMainPanel inventoryMain = new inventoryMainPanel(adminFullname);
                        this.Hide();
                        inventoryMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("You are not authorized to login as an Inventory Manager.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
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
