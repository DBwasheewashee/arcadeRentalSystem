using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace rentalSystemMain
{
    public partial class adminLoginForm : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        public adminLoginForm()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string adminFullname = null;

            try
            {
                con.Open();
                SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM admin_tbl WHERE admin_username = @Username AND admin_password = @Password", con);
                cmdCount.Parameters.AddWithValue("@Username", username);
                cmdCount.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(cmdCount.ExecuteScalar());

                if (count > 0)
                {
                    SqlCommand cmdFullname = new SqlCommand("SELECT admin_fullname FROM admin_tbl WHERE admin_username = @Username AND admin_password = @Password", con);
                    cmdFullname.Parameters.AddWithValue("@Username", username);
                    cmdFullname.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmdFullname.ExecuteReader();
                    if (reader.Read())
                    {
                        adminFullname = reader["admin_fullname"].ToString();
                    }

                    reader.Close();

                    MessageBox.Show("Login successful!");

                 
                    adminMainPanel adminMainPanel = new adminMainPanel(adminFullname);
                    this.Hide();
                    adminMainPanel.Show();
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
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
