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
    public partial class addGames : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True;");
        viewGameCatalog _viewAllGames;
        viewAvailableItems _viewAvailableItems;

        public addGames(viewGameCatalog viewAllGames, viewAvailableItems viewAvailableItems)
        {
            InitializeComponent();
            _viewAllGames = viewAllGames;
            _viewAvailableItems = viewAvailableItems;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter game name");
            }
            else if(string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter game price");
            }
            else if(string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please enter number of copies");
            }
            else
            {
                try
                {
                    con.Open();
                    string insertGameQuery = "INSERT INTO games_table (game_name, game_price, game_quantity) VALUES (@game_name, @game_price, @game_quantity)";
                    SqlCommand cmdinfo = new SqlCommand(insertGameQuery, con);
                    cmdinfo.Parameters.AddWithValue("@game_name", textBox1.Text);
                    cmdinfo.Parameters.AddWithValue("@game_price", textBox2.Text);
                    cmdinfo.Parameters.AddWithValue("@game_quantity", textBox3.Text);
                    cmdinfo.ExecuteNonQuery();

                    MessageBox.Show("Game successfully added");
                    _viewAllGames.ShowAllGames();
                    _viewAvailableItems.ShowAvailableItems();

                    this.Close();
                }
                catch(Exception ex) 
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
