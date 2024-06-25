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
    public partial class viewEmployeeInfo : Form
    {
        private readonly SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True;");
        private readonly string fullName;
        private readonly viewEmployee viewEmployeeControl;
        public viewEmployeeInfo(string fullname, string position, viewEmployee viewEmployeeControl)
        {
            InitializeComponent();
            fullName = fullname;
            ViewEmployeeInfo(fullname);
            label7.Text = fullname;
            label11.Text = position;
            this.viewEmployeeControl = viewEmployeeControl;
        }

        public void ViewEmployeeInfo(string fullname)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT employee_email, employee_phone, employee_address FROM empl_info WHERE employee_fullname = @FullName", con);
                command.Parameters.AddWithValue("@FullName", fullname);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    label8.Text = reader["employee_email"].ToString();
                    label9.Text = reader["employee_phone"].ToString();
                    label10.Text = reader["employee_address"].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            editEmployee editForm = new editEmployee(fullName, label8.Text, label9.Text, label10.Text, label11.Text);
            editForm.ShowDialog();
            viewEmployeeControl.ShowEmployee();
            this.Close();
            

        }

        public void UpdateEmployeeInfo(string fullName)
        {
            
            ViewEmployeeInfo(fullName);
        }
    }
}
