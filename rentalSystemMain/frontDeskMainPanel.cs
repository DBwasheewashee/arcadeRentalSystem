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
    public partial class frontDeskMainPanel : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        public string _adminFullname;
        public frontDeskMainPanel(string adminFullname)
        {
            InitializeComponent();
            _adminFullname = adminFullname;
            label2.Text = _adminFullname;
            viewAvailableItems1.BringToFront();
            panel3.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Logout?", "Logout Account", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            createTransaction createTransactionForm = new createTransaction(_adminFullname);

            // Show the createTransaction form
            createTransactionForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            onGoingTransactionFrontDesk1.BringToFront();        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewAvailableItems1.BringToFront();
            panel3.BringToFront();
        }
    }
}
