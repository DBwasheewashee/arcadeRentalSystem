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
using System.Text.RegularExpressions;


namespace rentalSystemMain
{
    public partial class addEmployee : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True;");

        private viewEmployee _viewEmployee;

        public addEmployee(viewEmployee viewEmployeeInstance)
        {
            InitializeComponent();
            _viewEmployee = viewEmployeeInstance;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
               textBox6.UseSystemPasswordChar = false;
            else
                textBox6.UseSystemPasswordChar = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) 
                textBox7.UseSystemPasswordChar = false;
            else
                textBox7.UseSystemPasswordChar = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string phonePattern = @"^\d{11}$";

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter Employee fullname.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }           
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter Employee email address.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
            else if(!Regex.IsMatch(textBox2.Text, emailPattern))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
            }
            else if(string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please enter Employee Phone number.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
            }
            else if(!Regex.IsMatch(textBox3.Text, phonePattern))
            {
                MessageBox.Show("Please enter a valid phone number (11 digits only).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Clear();
                textBox3.Focus();
            }
            else if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please enter Employee Address", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
            }
            else if(string.IsNullOrEmpty (comboBox1.Text))
            {
                MessageBox.Show("Please choose Employee Role", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
            }
            else if(string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please enter Employee account username", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
            }
            else if(string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Please enter Employee account password", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Focus();
            }
            else if(string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Confirm Password is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox7.Focus();
            }
            else if(textBox6.Text != textBox7.Text)
            {
                MessageBox.Show("Passwords do not match", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Clear();
                textBox7.Clear();
                textBox6.Focus();
            }
            else
            {
                try
                {
                    con.Open();

                    
                    string insertInfoQuery = "INSERT INTO empl_info (employee_fullname, employee_email, employee_phone, employee_address, employee_position, hire_date) VALUES (@FullName, @Email, @PhoneNumber, @Address, @Position, @HireDate); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmdInfo = new SqlCommand(insertInfoQuery, con);
                    cmdInfo.Parameters.AddWithValue("@FullName", textBox1.Text);
                    cmdInfo.Parameters.AddWithValue("@Email", textBox2.Text);
                    cmdInfo.Parameters.AddWithValue("@PhoneNumber", textBox3.Text);
                    cmdInfo.Parameters.AddWithValue("@Address", textBox4.Text);
                    cmdInfo.Parameters.AddWithValue("@Position", comboBox1.Text);
                    cmdInfo.Parameters.AddWithValue("@HireDate", DateTime.Now);
                    int employeeId = Convert.ToInt32(cmdInfo.ExecuteScalar());

                    
                    string insertAccQuery = "INSERT INTO empl_acc (employee_id, employee_username, employee_password) VALUES (@EmployeeId, @Username, @Password)";
                    SqlCommand cmdAcc = new SqlCommand(insertAccQuery, con);
                    cmdAcc.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmdAcc.Parameters.AddWithValue("@Username", textBox5.Text);
                    cmdAcc.Parameters.AddWithValue("@Password", textBox6.Text);
                    cmdAcc.ExecuteNonQuery();

                    MessageBox.Show("Employee added successfully!");

                    _viewEmployee.ShowEmployee();
                    this.Close();
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
    }

