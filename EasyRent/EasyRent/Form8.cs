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

namespace EasyRent
{
    public partial class Form8 : Form
    {
        string ConnectionString= "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";
        public Form8()
        {
            InitializeComponent();
            string query = "SELECT * FROM Landlord";
            FillDataGridView(query);
        }
        private void FillDataGridView(string query)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 ff = new Form6();
            ff.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Form2 ff = new Form2();
            ff.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 ff = new Form6();
            ff.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
       FROM Landlord 
       WHERE L_id LIKE @searchTerm 
       OR L_name LIKE @searchTerm
       OR L_email LIKE @searchTerm
       OR L_location LIKE @searchTerm";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the first selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the value of L_id from the selected row
                var idValue = selectedRow.Cells["L_id"].Value;

                // Check if the idValue is not null and can be converted to an integer
                if (idValue != null && int.TryParse(idValue.ToString(), out int id))
                {
                    // Hide the current form and open Form5 with the selected L_id
                    this.Hide();
                    Form5 fff = new Form5(id, 3,2); // Pass the id and other necessary parameters
                    fff.Show();
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
    }
}
