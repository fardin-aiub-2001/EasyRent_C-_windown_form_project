using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyRent
{
    public partial class Form7 : Form
    {
        string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";
        public Form7()
        {
            InitializeComponent();
            //InitializeComponent();
            string query = "SELECT * FROM Tenant";
            FillDataGridView(query);
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 ff = new Form2();
            ff.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 ff = new Form6();
            ff.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
       SELECT * 
       FROM Tenant 
       WHERE T_id LIKE @searchTerm 
       OR T_name LIKE @searchTerm
       OR T_email LIKE @searchTerm
       OR T_location LIKE @searchTerm";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Explicitly add the parameter with its data type
                    command.Parameters.Add("@searchTerm", SqlDbType.NVarChar).Value = "%" + searchValue + "%";

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching rows found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the first selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the value of T_id from the selected row
                var idValue = selectedRow.Cells["T_id"].Value;

                // Check if the idValue is not null or empty and can be converted to an integer
                if (idValue != null && int.TryParse(idValue.ToString(), out int id))
                {
                    // Hide the current form and open Form5 with the selected T_id
                    this.Hide();
                    Form5 ff = new Form5(id, 3,1); // Pass the id and other necessary parameters
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

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    } 
}