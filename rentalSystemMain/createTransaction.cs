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
using System.Text.RegularExpressions;

namespace rentalSystemMain
{
    public partial class createTransaction : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        private string _adminFullname;

        decimal customerPaid;
        decimal depositPaid;



        public createTransaction(string adminFullname)
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged; 
            _adminFullname = adminFullname;
            employee_name.Text = _adminFullname;
            PopulateCombobox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateCombobox()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT game_name FROM games_table WHERE game_quantity > 0", con);
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

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select game to rent");
            }
            else
            {
                string selectedGameName = comboBox1.SelectedItem.ToString();
                decimal selectedGamePrice = GetGamePricePerDay(selectedGameName);

                decimal selectedDiscountPercentage = GetGameDiscount(selectedGameName);
                GameInfo gameInfo = new GameInfo(selectedGameName, selectedGamePrice, selectedDiscountPercentage, this);



                listBox1.Items.Add(gameInfo);
                UpdateTotal();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string selectedGame = listBox1.SelectedItem.ToString();

                DialogResult result = MessageBox.Show("Do you want to remove '" + selectedGame + "' from the list?", "Remove Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    UpdateTotal();
                }
                else
                {
                   
                }
            }
        }

        private void clearList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            if (startDate == endDate)
            {
                totalBox.Text = "0.00";
                return;
            }

            int totalDays = (int)(endDate - startDate).TotalDays;
            noOfDays.Text = totalDays.ToString();

            foreach (var selectedItem in listBox1.Items)
            {
                GameInfo gameInfo = selectedItem as GameInfo;
                if (gameInfo != null)
                {
                    decimal gamePricePerDay = gameInfo.Price;
                    decimal discountPercentage = gameInfo.DiscountPercentage;
                    decimal totalPrice = gamePricePerDay * totalDays;
                    decimal discountedPrice = totalPrice * (1 - (discountPercentage / 100));
                    total += discountedPrice;
                }
            }

            totalBox.Text = total.ToString("F2");
        }


        private decimal GetGamePricePerDay(string gameName)
        {
            decimal gamePricePerDay = 0;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT game_price FROM games_table WHERE game_name = @GameName", con);
                command.Parameters.AddWithValue("@GameName", gameName);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    gamePricePerDay = Convert.ToDecimal(result);
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
            return gamePricePerDay;
        }

        private decimal GetGameDiscount(string gameName)
        {
            decimal discountPercentage = 0;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT discount FROM games_table WHERE game_name = @GameName", con);
                command.Parameters.AddWithValue("@GameName", gameName);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    discountPercentage = Convert.ToDecimal(result);
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
            return discountPercentage;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        public class GameInfo
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public decimal DiscountPercentage { get; set; }


            private createTransaction _transactionForm;

            public GameInfo(string name, decimal price, decimal discountPercentage, createTransaction transactionForm)
            {
                Name = name;
                Price = price;
                DiscountPercentage = discountPercentage;
                _transactionForm = transactionForm;
            }

            public override string ToString()
            {
                
                decimal discountPercentage = _transactionForm.GetGameDiscount(Name);

               
                return $"{Name} - Php{Price} (Discount: {discountPercentage}%)";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string phonePattern = @"^\d{11}$";

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("No selected Games to rent", "List Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)

            {
                MessageBox.Show("The rental start and return dates cannot be the same. Please select different dates for rental and return.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Fill Customer Name", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Fill Customer phone number", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
            else if(!Regex.IsMatch(textBox2.Text, phonePattern))
            {
                MessageBox.Show("Phone number must consist of 11 digits and must begin with '09'", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
            }
            else if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Fill Customer email", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
            }
            else if(!Regex.IsMatch(textBox3.Text, emailPattern))
            {
                MessageBox.Show("Please enter a valid email address", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Clear();
                textBox3.Focus();
            }
            else if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Fill Customer address", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
            }
            else if(string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("No payment input", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
            }
            else if (!decimal.TryParse(textBox5.Text, out customerPaid))
            {
                MessageBox.Show("Invalid input in payment. Please enter a valid numeric value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(!decimal.TryParse(textBox6.Text, out depositPaid))
            {
                MessageBox.Show("Invalid input in deposit. Please enter a valid numeric value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

     
                // Initialize the list of rented games
                List<GameInfo> rentedGames = new List<GameInfo>();

                // Iterate through each item in the listBox1
                foreach (var item in listBox1.Items)
                {
                    // Check if the item is of type GameInfo
                    if (item is GameInfo gameInfo)
                    {
                        // If it is, add it to the rentedGames list
                        rentedGames.Add(gameInfo);
                    }
                }

                // Get the total, start date, and end date
                decimal total = decimal.Parse(totalBox.Text);
                DateTime startDate = dateTimePicker1.Value.Date;
                DateTime endDate = dateTimePicker2.Value.Date;
                string customer_name = textBox1.Text;
                string customer_phone = textBox2.Text;
                string customer_email = textBox3.Text;
                string customer_address = textBox4.Text;
                decimal customer_payment = customerPaid;
                decimal customer_deposit = depositPaid;
                string noOfDaysToRent = noOfDays.Text;
                string adminname = _adminFullname;

                // Create and show the invoiceTransaction form
                invoiceTransaction invoiceForm = new invoiceTransaction(this, rentedGames, total, adminname, startDate, endDate, noOfDaysToRent, customer_name, customer_phone, customer_address, customer_email, customer_payment, customer_deposit);
                invoiceForm.ShowDialog();
            }
        }

     
    }
}
