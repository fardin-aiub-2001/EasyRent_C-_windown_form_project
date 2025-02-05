using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyRent
{
    public partial class Form14 : Form
    {
        string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";
        public Form14()
        {
            InitializeComponent();
            string query = "SELECT * FROM Payment";
            FillDataGridView(query);
            LoadForm();
        }
        private void FillDataGridView(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }
        public void LoadForm()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Query to sum the Total_Bill
                string sumQuery = "SELECT SUM(Total_bill) AS TotalSum FROM Payment";

                // Queries to count rows in Tenant and Landlord tables
                string tenantCountQuery = "SELECT COUNT(*) AS TenantCount FROM Tenant";
                string landlordCountQuery = "SELECT COUNT(*) AS LandlordCount FROM Landlord";

                try
                {
                    // Calculate Total_Bill sum
                    using (SqlCommand sumCommand = new SqlCommand(sumQuery, con))
                    {
                        object sumResult = sumCommand.ExecuteScalar();
                        if (sumResult != DBNull.Value && sumResult != null)
                        {
                            decimal totalSum = Convert.ToDecimal(sumResult);
                            label5.Text = $"{totalSum:C}"; // Display total in label5
                            decimal fifteenPercent = totalSum * 0.15M;
                            label6.Text = $"{fifteenPercent:C}"; // Display 15% in label6
                        }
                        else
                        {
                            label5.Text = "$0.00";
                            label6.Text = "$0.00";
                        }
                    }

                    // Count rows in Tenant and Landlord tables
                    int tenantCount = 0;
                    int landlordCount = 0;

                    using (SqlCommand tenantCommand = new SqlCommand(tenantCountQuery, con))
                    {
                        tenantCount = Convert.ToInt32(tenantCommand.ExecuteScalar());
                    }

                    using (SqlCommand landlordCommand = new SqlCommand(landlordCountQuery, con))
                    {
                        landlordCount = Convert.ToInt32(landlordCommand.ExecuteScalar());
                    }

                    // Update label9 with the total row count
                    int totalRows = tenantCount + landlordCount;
                    label9.Text = $"{totalRows}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

               
                var idValue = selectedRow.Cells["T_id"].Value;

                if (idValue != null && int.TryParse(idValue.ToString(), out int id))
                {
                    
                    this.Hide();
                    Form5 ff = new Form5(id, 3, 1); // Pass the id and other necessary parameters
                    ff.Show();
                }
                else
                {
                    MessageBox.Show("Invalid ID or no ID found in the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 ff = new Form6();
            ff.Show();
        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form14_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
