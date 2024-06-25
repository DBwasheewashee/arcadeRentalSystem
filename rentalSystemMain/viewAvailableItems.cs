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
    public partial class viewAvailableItems : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        private Timer refreshTimer; // para auto refresh ung ShowAvailableItems table datagridview

        public viewAvailableItems()
        {
            InitializeComponent();
         //   InitializeTimer();
            ShowAvailableItems();
        }
    /*    private void InitializeTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 1000; // 1 second interval
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start(); // Start the timer // para di na need ng maual refresh ng user
         }
    */

     /*   private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            ShowAvailableItems(); 
        }

    */
        public void ShowAvailableItems()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT game_name as Game, game_price as Price FROM games_table WHERE game_quantity > 0", con);
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
            createTransaction createTransaction = new createTransaction((this.ParentForm as adminMainPanel)._adminFullname);
            createTransaction.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchGame.Clear();
            ShowAvailableItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchgame = searchGame.Text.Trim();

            if(!string.IsNullOrEmpty(searchgame))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("SELECT game_name as Game, game_price as Price FROM games_table WHERE game_name LIKE @SearchValue", con);
                    command.Parameters.AddWithValue("@SearchValue", "%" + searchgame + "%");
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    dataGridView1.DataSource = dt;

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
            else
            {
                ShowAvailableItems();
            }

            
        }
    }
}