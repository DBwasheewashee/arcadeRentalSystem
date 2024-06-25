using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using com.itextpdf.text.pdf;
using System.Data.SqlClient;
using System.Drawing.Printing;



namespace rentalSystemMain
{


    public partial class invoiceTransaction : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        private createTransaction _createTransactionForm;
        public invoiceTransaction(createTransaction createTransactionForm, List<createTransaction.GameInfo> rentedGames, decimal total, string adminFullname, DateTime startDate, DateTime endDate, string noOfDaysToRent, string customer_name, string customer_phone, string customer_address, string customer_email, decimal customer_payment, decimal customer_deposit)
        {
            InitializeComponent();

            _createTransactionForm = createTransactionForm;

            // Populate form fields with data
            this.rentalStart.Text = startDate.ToShortDateString();
            this.rentalEnd.Text = endDate.ToShortDateString();
            this.customerName.Text = customer_name;
            this.customerPhone.Text = customer_phone;
            this.customerEmail.Text = customer_email;
            this.customerAddress.Text = customer_address;
            this.customerPayment.Text = customer_payment.ToString();
            this.depositAmount.Text = customer_deposit.ToString();
            this.rentDays.Text = noOfDaysToRent.ToString();
            this.employeeName.Text = adminFullname;

            decimal subtotalValue = 0;
            decimal totalDiscount = 0;

            foreach (var game in rentedGames)
            {
                decimal discountAmount = game.Price * (game.DiscountPercentage / 100);
                decimal discountedPrice = game.Price - discountAmount;
                decimal totalPrice = discountedPrice * int.Parse(rentDays.Text);
                subtotalValue += totalPrice;
                totalDiscount += discountAmount;

                string formattedString = $"{game.Name.PadRight(30)}" +
                                         $"Php{game.Price:F2}".PadRight(15) +
                                         $"(Discount: {game.DiscountPercentage}%)".PadRight(20) +
                                         $"Php{discountedPrice:F2}".PadRight(15);

                this.listOfGamesToRent.Items.Add(formattedString);
            }

    

            this.subtotal.Text = subtotalValue.ToString("F2");
            this.discount.Text = totalDiscount.ToString("F2");

            // Subtract customer payment from the total
            decimal finalTotal = total - customer_payment;
            this.total.Text = finalTotal.ToString("F2");
        }

        private void Print(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            panelPrint = pnl;
            getprintarea(pnl);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.ShowDialog();
        }

        private Bitmap memoryimg;

        private void getprintarea(Panel pnl)
        {
            memoryimg = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memoryimg, new System.Drawing.Rectangle(0,0, pnl.Width, pnl.Height));
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memoryimg, (pagearea.Width / 2) - (this.panelPrint.Width / 2), this.panelPrint.Location.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to issue the invoice?", "Issue Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Print(this.panelPrint);

                // Generate the invoice (you can use a library like iTextSharp for PDF generation)
                // For demonstration purposes, let's assume the invoice is generated successfully

                // Store the invoice data in the database
                StoreInvoiceData();

                // Update the rental transaction status to "Invoiced"


                // Provide feedback to the user
                MessageBox.Show("Invoice issued successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StoreInvoiceData()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(@"INSERT INTO InvoiceTransactions 
                            (RentalStart, RentalEnd, EmployeeName, CustomerName, CustomerPhone, 
                            CustomerEmail, CustomerAddress, CustomerPayment, DepositAmount, 
                            RentDays, Subtotal, Discount, TotalAmount, TransactionStatus, ListBoxItems)
                            VALUES
                            (@RentalStart, @RentalEnd, @EmployeeName, @CustomerName, @CustomerPhone, 
                            @CustomerEmail, @CustomerAddress, @CustomerPayment, @DepositAmount, 
                            @RentDays, @Subtotal, @Discount, @TotalAmount, @TransactionStatus, @ListBoxItems)", con);

                command.Parameters.AddWithValue("@RentalStart", rentalStart.Text);
                command.Parameters.AddWithValue("@RentalEnd", rentalEnd.Text);
                command.Parameters.AddWithValue("@EmployeeName", employeeName.Text);
                command.Parameters.AddWithValue("@CustomerName", customerName.Text);
                command.Parameters.AddWithValue("@CustomerPhone", customerPhone.Text);
                command.Parameters.AddWithValue("@CustomerEmail", customerEmail.Text);
                command.Parameters.AddWithValue("@CustomerAddress", customerAddress.Text);
                command.Parameters.AddWithValue("@CustomerPayment", decimal.Parse(customerPayment.Text));
                command.Parameters.AddWithValue("@DepositAmount", decimal.Parse(depositAmount.Text));
                command.Parameters.AddWithValue("@RentDays", int.Parse(rentDays.Text));
                command.Parameters.AddWithValue("@Subtotal", decimal.Parse(subtotal.Text));
                command.Parameters.AddWithValue("@Discount", decimal.Parse(discount.Text));
                command.Parameters.AddWithValue("@TotalAmount", decimal.Parse(total.Text));
                command.Parameters.AddWithValue("@TransactionStatus", "Ongoing");

                // Convert ListBox items to comma-separated string
                string listBoxItemsValue = string.Join(",", listOfGamesToRent.Items.Cast<string>());
                command.Parameters.AddWithValue("@ListBoxItems", listBoxItemsValue);

                command.ExecuteNonQuery();

                foreach (string listItem in listOfGamesToRent.Items)
                {
                    string gameName = listItem.Split(new string[] { "Php" }, StringSplitOptions.None)[0].Trim(); // Extract game name from list item
                    SqlCommand updateCommand = new SqlCommand("UPDATE games_table SET game_quantity = game_quantity - 1 WHERE game_name = @GameName", con);
                    updateCommand.Parameters.AddWithValue("@GameName", gameName);
                    updateCommand.ExecuteNonQuery();
                }

                this.Close();
                _createTransactionForm.Close();
                // add .close for createTransaction
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error storing invoice data: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}
