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
    public partial class deleteGame : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        viewGameCatalog _viewGameCatalog;
        public deleteGame(viewGameCatalog viewGameCatalog)
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
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["game_name"].ToString());
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
                MessageBox.Show("Please choose a game to remove");
            }
            else
            {

            
            DialogResult result = MessageBox.Show("Are you sure you want to delete this game?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) 
            {
                try
                {
                    con.Open();

                    string selectedGameName = comboBox1.SelectedItem.ToString();
                    SqlCommand deleteInfoCommand = new SqlCommand("DELETE FROM games_table WHERE game_name = @GameName", con);
                    deleteInfoCommand.Parameters.AddWithValue("@GameName", selectedGameName);
                    deleteInfoCommand.ExecuteNonQuery();

                    MessageBox.Show("Game " + selectedGameName + " deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _viewGameCatalog.ShowAllGames();
                    this.Close();
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
               }
            }
        }

      
    }
}
