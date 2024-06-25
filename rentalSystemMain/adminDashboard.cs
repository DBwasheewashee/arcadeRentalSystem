using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace rentalSystemMain
{
    public partial class adminDashboard : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True");

        public adminDashboard()
        {
            InitializeComponent();
            LoadChart();
            UpdateLabels();
            LoadChart2();

        }

        public void LoadChart()
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(@"
            SELECT TransactionStatus, COUNT(*) AS StatusCount
            FROM TransactionHistory
            WHERE TransactionStatus IN ('returned', 'late', 'missing', 'void')
            GROUP BY TransactionStatus", con);

                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);

               
                DataView dv = dt.DefaultView;
                dv.Sort = "StatusCount DESC";
                DataTable sortedDt = dv.ToTable();

                chart1.Series.Clear();
                Series series = new Series("TransactionStatus");
                series.ChartType = SeriesChartType.Pie;

                foreach (DataRow row in sortedDt.Rows)
                {
                    string status = row["TransactionStatus"].ToString();
                    int count = Convert.ToInt32(row["StatusCount"]);
                    series.Points.AddXY(status, count);
                }

                chart1.Series.Add(series);
                chart1.Invalidate();
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

        private void UpdateLabels()
        {
            try
            {
                con.Open();

                
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM empl_info", con);
                int employeeCount = (int)cmd1.ExecuteScalar();
                label10.Text = employeeCount.ToString();

                
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM InvoiceTransactions", con);
                int invoiceTransactionCount = (int)cmd2.ExecuteScalar();
                label7.Text = invoiceTransactionCount.ToString();

            
                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM TransactionHistory WHERE TransactionStatus = 'Returned'", con);
                int returnCount = (int)cmd3.ExecuteScalar();
                label8.Text = returnCount.ToString();

              
                SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM TransactionHistory WHERE TransactionStatus = 'Missing'", con);
                int missingCount = (int)cmd4.ExecuteScalar();
                label9.Text = missingCount.ToString();

                SqlCommand cmd5 = new SqlCommand("SELECT COUNT(*) FROM games_table", con);
                int gamesCount = (int)cmd5.ExecuteScalar();
                label6.Text = gamesCount.ToString();
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

        public void LoadChart2()
        {
            string connectionString = "Data Source=DESKTOP-CK1RN4R\\SQLEXPRESS;Initial Catalog=rentalSystemDB;Integrated Security=True";

            string query = @"
                            SELECT 
                                YEAR(RentalStart) AS Year,
                                MONTH(RentalStart) AS Month,
                                SUM(TotalAmount) AS TotalRevenue
                            FROM 
                                TransactionHistory
                            WHERE 
                                TransactionStatus NOT IN ('Missing', 'Void')
                            GROUP BY 
                                YEAR(RentalStart),
                                MONTH(RentalStart)
                            ORDER BY 
                                Year,
                                Month;";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

           
            chart2.Series.Clear();
            chart2.Titles.Clear();
            chart2.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea();
            chart2.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                Name = "TotalRevenue",
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double
            };

            chart2.Series.Add(series);

            foreach (DataRow row in dataTable.Rows)
            {
                int month = Convert.ToInt32(row["Month"]);
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                string monthYear = $"{monthName} {row["Year"]}";
                double totalRevenue = Convert.ToDouble(row["TotalRevenue"]);
                series.Points.AddXY(monthYear, totalRevenue);
            }

            chart2.ChartAreas[0].AxisX.Title = "Month/Year";
            chart2.ChartAreas[0].AxisY.Title = "Total Revenue";
            chart2.Titles.Add("Total Revenue Per Month");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadChart();
            UpdateLabels();
            LoadChart2();

        }

    }
}
