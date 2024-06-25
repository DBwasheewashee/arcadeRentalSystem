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
    public partial class viewTransaction : Form
    {
        private string customerName;
        private int invoiceID;
        private string _adminFullname;

        ongoingTransactions _ongoingTransactions;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        public viewTransaction(string customerName, int invoiceID, ongoingTransactions ongoingTransactions, string adminFullname)
        {
            InitializeComponent();
            this.label6.Text = customerName;
            this.label28.Text = invoiceID.ToString();
            ShowCustomerTransaction(customerName, invoiceID);
            _ongoingTransactions = ongoingTransactions;
            _adminFullname = adminFullname;
            textBox2.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowCustomerTransaction(string customerName, int invoiceID)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM InvoiceTransactions WHERE InvoiceID = @InvoiceID AND CustomerName = @CustomerName", con);
                command.Parameters.AddWithValue("@InvoiceID", invoiceID);
                command.Parameters.AddWithValue("@CustomerName", customerName);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    label28.Text = reader["InvoiceID"].ToString();
                    label6.Text = reader["CustomerName"].ToString();
                    label7.Text = reader["CustomerPhone"].ToString();
                    label8.Text = reader["CustomerEmail"].ToString();
                    label9.Text = reader["CustomerAddress"].ToString();
                    label13.Text = ((DateTime)reader["RentalStart"]).ToString("yyyy-MM-dd");
                    label14.Text = ((DateTime)reader["RentalEnd"]).ToString("yyyy-MM-dd");
                    label15.Text = reader["RentDays"].ToString();
                    label20.Text = reader["Subtotal"].ToString();
                    label24.Text = reader["Discount"].ToString();
                    label25.Text = reader["CustomerPayment"].ToString();
                    label26.Text = reader["TotalAmount"].ToString();
                    label30.Text = reader["TransactionStatus"].ToString();
                    label32.Text = reader["DepositAmount"].ToString();
                    label34.Text = reader["EmployeeName"].ToString();

                    string listBoxItems = reader["ListBoxItems"].ToString();
                    string[] itemsArray = listBoxItems.Split(',');

                    foreach (string item in itemsArray)
                    {
                        listBox1.Items.Add(item.Trim());
                    }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a rental status");
            }
            else if (!decimal.TryParse(textBox2.Text, out _))
            {
                MessageBox.Show("Please enter numeric values only in both textboxes");
            }
            else
            {
                // Get the selected rental status from the ComboBox
                string selectedStatus = comboBox1.SelectedItem.ToString();
                DialogResult result = MessageBox.Show("Are you sure you want to issue the invoice?", "Issue Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (selectedStatus == "Returned")
                    {
                        StoreinTransactionHistory();
                    }
                    else if (selectedStatus == "Late")
                    {
                        if (decimal.TryParse(textBox2.Text, out decimal penalty) && penalty <= 0)
                        {
                            MessageBox.Show("Please put value in penalty");
                        }
                        else
                        {
                            StoreinTransactionHistory();
                        }
                    }
                    else if (selectedStatus == "Missing" || selectedStatus == "Void")
                    {
                        StoreinTransactionHistory();
                    }
                }
            }
        }

        public void StoreinTransactionHistory()
        {
            try
            {
                // Extracting data from labels and textboxes
                int invoiceID = int.Parse(label28.Text);
                DateTime rentalStart = DateTime.Parse(label13.Text);
                DateTime rentalEnd = DateTime.Parse(label14.Text);
                string employeeName = label34.Text;
                string customerName = label6.Text;
                string customerPhone = label7.Text;
                string customerEmail = label8.Text;
                string customerAddress = label9.Text;
                decimal customerPayment = decimal.Parse(label25.Text);
                decimal penalty = decimal.Parse(textBox2.Text);
                int rentDays = int.Parse(label15.Text);
                decimal subtotal = decimal.Parse(label20.Text);
                decimal discount = decimal.Parse(label24.Text);
                decimal totalAmount = decimal.Parse(label26.Text);
                decimal depositAmount = decimal.Parse(label32.Text);
                string transactionStatus = comboBox1.SelectedItem.ToString(); // Assuming comboBox1 contains transaction status options

                // Get the selected game from the ListBox
                var listBoxItems = listBox1.Items.Cast<string>().ToList();

                // Write the SQL insert statement
                string sqlInsert = @"INSERT INTO TransactionHistory (InvoiceID, RentalStart, RentalEnd, EmployeeName, CustomerName, CustomerPhone, 
                CustomerEmail, CustomerAddress, CustomerPayment, DepositAmount, RentDays, Subtotal, Discount, TotalAmount, Penalty, 
                TransactionStatus, ListBoxItems, ReceivedBy) 
                VALUES (@InvoiceID, @RentalStart, @RentalEnd, @EmployeeName, @CustomerName, @CustomerPhone, @CustomerEmail, 
                @CustomerAddress, @CustomerPayment, @DepositAmount, @RentDays, @Subtotal, @Discount, @TotalAmount, @Penalty, 
                @TransactionStatus, @ListBoxItems, @ReceivedBy)";

                // Write the SQL delete statement
                string sqlDelete = @"DELETE FROM InvoiceTransactions WHERE InvoiceID = @InvoiceID";

                // Write the SQL update statement for game quantities
                string sqlUpdateGameQuantity = @"UPDATE games_table SET game_quantity = game_quantity + 1 WHERE game_name LIKE '%' + @GameName + '%'";



                // Create connection and command objects
                using (SqlConnection conn = new SqlConnection(con.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, transaction))
                            {
                                // Add parameters to the insert command
                                cmdInsert.Parameters.AddWithValue("@InvoiceID", invoiceID);
                                cmdInsert.Parameters.AddWithValue("@RentalStart", rentalStart);
                                cmdInsert.Parameters.AddWithValue("@RentalEnd", rentalEnd);
                                cmdInsert.Parameters.AddWithValue("@EmployeeName", employeeName);
                                cmdInsert.Parameters.AddWithValue("@CustomerName", customerName);
                                cmdInsert.Parameters.AddWithValue("@CustomerPhone", customerPhone);
                                cmdInsert.Parameters.AddWithValue("@CustomerEmail", customerEmail);
                                cmdInsert.Parameters.AddWithValue("@CustomerAddress", customerAddress);
                                cmdInsert.Parameters.AddWithValue("@CustomerPayment", customerPayment);
                                cmdInsert.Parameters.AddWithValue("@DepositAmount", depositAmount);
                                cmdInsert.Parameters.AddWithValue("@RentDays", rentDays);
                                cmdInsert.Parameters.AddWithValue("@Subtotal", subtotal);
                                cmdInsert.Parameters.AddWithValue("@Discount", discount);
                                cmdInsert.Parameters.AddWithValue("@TotalAmount", totalAmount);
                                cmdInsert.Parameters.AddWithValue("@Penalty", penalty);
                                cmdInsert.Parameters.AddWithValue("@TransactionStatus", transactionStatus);
                                cmdInsert.Parameters.AddWithValue("@ListBoxItems", string.Join(",", listBoxItems));
                                cmdInsert.Parameters.AddWithValue("@ReceivedBy", _adminFullname);

                                // Execute the insert command
                                cmdInsert.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdDelete = new SqlCommand(sqlDelete, conn, transaction))
                            {
                                // Add parameters to the delete command
                                cmdDelete.Parameters.AddWithValue("@InvoiceID", invoiceID);

                                // Execute the delete command
                                cmdDelete.ExecuteNonQuery();
                            }

                            // Update game quantities for each game in the listBoxItems
                            foreach (string listItem in listBoxItems)
                            {
                                string gameName = listItem.Split(new string[] { "Php" }, StringSplitOptions.None)[0].Trim(); // Extract game name from list item
                                using (SqlCommand cmdUpdateGameQuantity = new SqlCommand("UPDATE games_table SET game_quantity = game_quantity + 1 WHERE game_name = @GameName", conn, transaction))
                                {
                                    cmdUpdateGameQuantity.Parameters.AddWithValue("@GameName", gameName);

                                    // Execute the update command
                                    int rowsAffected = cmdUpdateGameQuantity.ExecuteNonQuery();

                                    // Check if the update was successful
                                    if (rowsAffected == 0)
                                    {
                                        // Log the issue for debugging purposes
                                        Console.WriteLine("No rows updated for game: " + gameName);
                                    }
                                }
                            }


                            // Commit the transaction
                            transaction.Commit();

                            _ongoingTransactions.ShowOngoingTransactions();
                            MessageBox.Show("Transactions successfully updated");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if an error occurs
                            transaction.Rollback();
                            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
