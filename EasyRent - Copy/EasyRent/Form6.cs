using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EasyRent
{
    
    public partial class Form6 : Form
    {
        private int id=903;
        
        string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";
        
        public Form6()
        {
            InitializeComponent();
            //isCollapsed = true; // Initialize as collapsed
            string query = "SELECT * FROM Apartment";
            FillDataGridView(query);
        }
        public Form6(int id)
        {
            this.id = id;
            InitializeComponent();
            //isCollapsed = true; // Initialize as collapsed
            string query = "SELECT * FROM Apartment";
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

        

        private void button1_Click(object sender, EventArgs e)
        {
            //timer1.Start(); // Start the timer to trigger expansion/collapse
        }

        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 ff = new Form8();
            ff.Show();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //timer1.Start();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 ff = new Form2();
            ff.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the first selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the value of T_id from the selected row
                var idValue = selectedRow.Cells["Ap_id"].Value;


                // Check if the idValue is not null or empty and can be converted to an integer
                if (idValue != null && int.TryParse(idValue.ToString(), out int aid))
                {
                    this.Hide();
                    Form10 form10 = new Form10(903, aid, 3, 3);
                    form10.Show();
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
       FROM Apartment 
       WHERE Nearby_institution LIKE @searchTerm 
       OR Ap_location LIKE @searchTerm
       OR Ap_id LIKE @searchTerm
       OR Monthly_rent LIKE @searchTerm";

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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Form9 form9 = new Form9(903,3,3);
            form9.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        
        

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form14 form14 = new Form14();
            form14.Show();

            //make this button corner round
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
