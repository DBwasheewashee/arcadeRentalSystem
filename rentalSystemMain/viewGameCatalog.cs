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
    public partial class viewGameCatalog : UserControl
    {
       
        viewAvailableItems viewAvailableItems = new viewAvailableItems();

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");
        public viewGameCatalog()
        {
            InitializeComponent();
            ShowAllGames();
      
        }

        public void ShowAllGames()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT game_name, game_price, game_quantity, discount FROM games_table ", con);
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();
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
            addGames addGames = new addGames(this, viewAvailableItems);
            addGames.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateGame updateGame = new updateGame(this);
            updateGame.Show();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                string selectedGameName = dataGridView1.SelectedRows[0].Cells["game_name"].Value.ToString();

                
                Console.WriteLine("Selected game name: " + selectedGameName);

                
                updateGame updateForm = new updateGame(this);

               
                updateForm.SetSelectedGame(selectedGameName);

               
                updateForm.Show();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteGame deleteGame = new deleteGame(this);
            deleteGame.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addGameDiscount addGameDiscount = new addGameDiscount(this);
            addGameDiscount.Show();
        }
    }
}
