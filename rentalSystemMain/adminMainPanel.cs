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
    public partial class adminMainPanel : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        public string _adminFullname;

    
        public adminMainPanel(string adminFullname)
        {
            
            InitializeComponent();
            _adminFullname = adminFullname;
            label2.Text = _adminFullname; 
            adminDashboard2.BringToFront();
           
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Logout?", "Logout Account", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewEmployee2.BringToFront();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
             adminDashboard2.BringToFront();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            viewAvailableItems2.BringToFront();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            viewGameCatalog2.BringToFront();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // passed _adminfullname value here in user control ongoingTransaction()
            ongoingTransactions1.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            transactionHistory1.BringToFront();
        }
    }
}
