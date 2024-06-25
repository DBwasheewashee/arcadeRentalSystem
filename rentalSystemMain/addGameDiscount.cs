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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace rentalSystemMain
{
    public partial class addGameDiscount : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        viewGameCatalog _viewGameCatalog;
        public addGameDiscount(viewGameCatalog viewGameCatalog)
        {
            InitializeComponent();
            PopulateComboBox();
            _viewGameCatalog = viewGameCatalog;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateComboBox()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT game_name FROM games_table", con);
                SqlDataReader reader = command.ExecuteReader();

        
                comboBox1.Items.Clear();

                while (reader.Read())
                {
                    string gameName = reader["game_name"].ToString();
                   
                    Console.WriteLine("Adding game: " + gameName);
                    comboBox1.Items.Add(gameName);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a game to add discount/change");
            }
            else if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please input discount value");
            }
            else
            {
                try
                {
                    con.Open();
                    string updateGameDiscount = "UPDATE games_table SET discount = @gameDiscount WHERE game_name = @gameName";
                    SqlCommand cmdinfo = new SqlCommand(updateGameDiscount, con);
                    cmdinfo.Parameters.AddWithValue("@gameDiscount", textBox1.Text);
                    cmdinfo.Parameters.AddWithValue("@gameName", comboBox1.Text);
                    int rowsAffected = cmdinfo.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Discount successfully added");
                        
                        _viewGameCatalog.ShowAllGames();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No game found with the selected name.");
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
}
