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
    public partial class updateGame : Form
    {
        private SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        viewGameCatalog _viewGameCatalog;

        private string oldPrice;
        private string oldQuantity;


        public updateGame(viewGameCatalog viewGameCatalog)
        {
            InitializeComponent();
            PopulateComboBox();
            _viewGameCatalog = viewGameCatalog;
        }

        public void SetSelectedGame(string gameName)
        {
            comboBox1.SelectedItem = gameName;

          
            MessageBox.Show("Selected game name set to: " + gameName);

       
            PopulateTextBoxes(gameName);
        }


        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedGameName = comboBox1.SelectedItem.ToString();
                PopulateTextBoxes(selectedGameName);

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT game_quantity, game_price FROM games_table WHERE game_name = @GameName", con);
                    cmd.Parameters.AddWithValue("@GameName", comboBox1.SelectedItem);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        textBox1.Text = reader["game_price"].ToString();
                        textBox2.Text = reader["game_quantity"].ToString();
                    }
                    else
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                        MessageBox.Show("Game not found");
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

        private void updateGame_Load(object sender, EventArgs e)
        {
            oldPrice = textBox1.Text.Trim();
            oldQuantity = textBox2.Text.Trim();
            PopulateComboBox();
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

        

        private void PopulateTextBoxes(string gameName)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT game_price, game_quantity FROM games_table WHERE game_name = @GameName", con);
                command.Parameters.AddWithValue("@GameName", gameName);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["game_price"].ToString();
                    textBox2.Text = reader["game_quantity"].ToString();

                  
                    oldPrice = textBox1.Text.Trim();
                    oldQuantity = textBox2.Text.Trim();
                }
                else
                {
                   
                    textBox1.Text = "";
                    textBox2.Text = "";
                    oldPrice = ""; 
                    oldQuantity = ""; 
                    MessageBox.Show("Game not found");
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
            con.Open();
            SqlCommand cmd = null;

            try
            {
                string newGameName = textBox3.Text.Trim();
                string newPrice = textBox1.Text.Trim();
                string newQuantity = textBox2.Text.Trim();

                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("No game selected");
                    return;
                }

                string oldGameName = comboBox1.SelectedItem.ToString().Trim();

               
                bool changesMade = newGameName != oldGameName || newPrice != oldPrice || newQuantity != oldQuantity;

                if (!changesMade)
                {
                    MessageBox.Show("No changes set");
                    return;
                }

                if (!string.IsNullOrEmpty(newGameName))
                {
                    
                    cmd = new SqlCommand("UPDATE games_table SET game_name = @NewGameName, game_price = @Param1, game_quantity = @Param2 WHERE game_name = @OldGameName", con);
                    cmd.Parameters.AddWithValue("@NewGameName", newGameName);
                }
                else
                {
                    
                    cmd = new SqlCommand("UPDATE games_table SET game_price = @Param1, game_quantity = @Param2 WHERE game_name = @OldGameName", con);
                }

                cmd.Parameters.AddWithValue("@Param1", newPrice);
                cmd.Parameters.AddWithValue("@Param2", newQuantity);
                cmd.Parameters.AddWithValue("@OldGameName", oldGameName);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Game information updated successfully");
                    _viewGameCatalog.ShowAllGames();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No rows updated. Please make sure the game exists.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                cmd?.Dispose(); 
            }

        }

       
    }
}

