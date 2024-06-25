using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace rentalSystemMain
{
    public partial class editEmployee : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True;");

        private readonly string oldFullName;

        public editEmployee(string oldFullName, string email, string phone, string address, string position)
        {

            InitializeComponent();

            this.oldFullName = oldFullName;
            textBox1.Text = oldFullName;
            textBox2.Text = email;
            textBox3.Text = phone;
            textBox4.Text = address;
            comboBox1.Text = position;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("No position selected");
            }
            else
            {



                try
                {

                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True;"))
                    {
                        con.Open();


                        string sql = "UPDATE empl_info SET employee_email = @Email, employee_phone = @Phone, employee_address = @Address, employee_position = @Position, employee_fullname = @NewFullName WHERE employee_fullname = @OldFullName";
                        SqlCommand command = new SqlCommand(sql, con);


                        command.Parameters.AddWithValue("@Email", textBox2.Text);
                        command.Parameters.AddWithValue("@Phone", textBox3.Text);
                        command.Parameters.AddWithValue("@Address", textBox4.Text);
                        command.Parameters.AddWithValue("@Position", comboBox1.Text);
                        command.Parameters.AddWithValue("@NewFullName", textBox1.Text);
                        command.Parameters.AddWithValue("@OldFullName", oldFullName);


                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("No updates");
                        }
                        else
                        {
                            MessageBox.Show("Employee Updated");
                        }
                    }



                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}