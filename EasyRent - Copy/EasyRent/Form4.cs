using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EasyRent
{
    public partial class Form4 : Form
    {
        private int id, b, c,aaid;
        private string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";
        public Form4()
        {
            InitializeComponent();
            //isCollapsed = true; // Initialize as collapsed
            string query = "SELECT * FROM Apartment";
            FillDataGridView(query);
            dataGridView1.Hide();
            button9.Hide();
            LoadForm();
        }
        private void FillDataGridView(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }
        public Form4(int id,int b,int c)
        {
            InitializeComponent();
            this.id = id;
            this.b = b;
            this.c = c;
            string query = "SELECT * FROM Apartment";
            FillDataGridView(query);
            button9.Hide();
            dataGridView1.Hide();

            LoadForm();

        }



        private void LoadForm()
        {
            flowLayoutPanel1.Controls.Clear(); // Clear existing controls before loading new ones
            string query = "SELECT Ap_id, Ap_location, Monthly_rent, Picture FROM Apartment"; // Query with 'Picture' column

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Create a new panel for each record
                        Panel itemPanel = new Panel
                        {
                            Width = 180,
                            Height = 250,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(10),
                            Margin = new Padding(10),
                            BackColor = Color.PeachPuff

                        };

                        // Add an image to the panel using PictureBox
                        PictureBox pictureBox = new PictureBox
                        {
                            Width = 180,
                            Height = 150,
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };

                        // Load the image from the database
                        if (reader["Picture"] != DBNull.Value)
                        {
                            byte[] imageData = (byte[])reader["Picture"];
                            pictureBox.Image = LoadImageFromDatabase(imageData);
                        }
                        else
                        {
                            pictureBox.Image = null; // No image available
                        }
                        itemPanel.Controls.Add(pictureBox);

                        // Add apartment details to the panel
                        Label detailsLabel = new Label
                        {
                            Text = $@"ID: {reader["Ap_id"]}
Location: {reader["Ap_location"]}
Rent: {reader["Monthly_rent"]}",
                            Font = new Font("Arial", 10, FontStyle.Regular),
                            AutoSize = true,
                            Top = pictureBox.Bottom + 10,
                           
                            ForeColor = Color.Sienna
                        };
                        itemPanel.Controls.Add(detailsLabel);

                        // Add a 'Select' button to the panel
                        Button selectButton = new Button
                        {
                            Text = "Select",
                            Dock = DockStyle.Bottom,
                            Tag = reader["Ap_id"], // Store the ID in the button's Tag property
                            Height = 30,
                            BackColor = Color.Maroon,
                            ForeColor = Color.White,
                            
                        };

                        // Handle the click event
                        selectButton.Click += (s, e) =>
                        {
                            aaid = Convert.ToInt32(selectButton.Tag); // Retrieve the ID from the Tag property
                            MessageBox.Show($"Selected Apartment ID: {aaid}", "Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        };
                        itemPanel.Controls.Add(selectButton);

                        // Add the panel to the flow layout panel
                        flowLayoutPanel1.Controls.Add(itemPanel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private Image LoadImageFromDatabase(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null; // Return null if no image is stored

            using (MemoryStream ms = new MemoryStream(imageData))
            {
                return Image.FromStream(ms);
            }
        }



        private void SelectButton_Click(object sender, EventArgs e, int apartmentId)
        {
            MessageBox.Show($"Apartment ID {apartmentId} selected.", "Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Example: Redirect to another form with the selected apartment ID
            this.Hide();
            Form10 form10 = new Form10(id, apartmentId, b, c);
            form10.Show();
        }


        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchValue =comboBox1.Text.Trim();



            string query = @"
SELECT * 
FROM Apartment 
WHERE Nearby_institution LIKE @searchTerm 
OR Ap_location LIKE @searchTerm
OR Ap_id LIKE @searchTerm
OR Monthly_rent LIKE @searchTerm
OR Service_charge LIKE @searchTerm
OR L_id LIKE @searchTerm
OR Ap_id LIKE @searchTerm";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@searchTerm", SqlDbType.NVarChar).Value = "%" + searchValue + "%";

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        flowLayoutPanel1.Controls.Clear(); // Clear existing items

                        while (reader.Read())
                        {
                            // Create a new panel for each record
                            Panel itemPanel = new Panel
                            {
                                Width = 180,
                                Height = 250,
                                BorderStyle = BorderStyle.FixedSingle,
                                Padding = new Padding(10),
                                Margin = new Padding(10),
                                BackColor = Color.PeachPuff
                            };

                            // Add an image to the panel using PictureBox
                            PictureBox pictureBox = new PictureBox
                            {
                                Width = 180,
                                Height = 150,
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };

                            if (reader["Picture"] != DBNull.Value)
                            {
                                byte[] imageData = (byte[])reader["Picture"];
                                pictureBox.Image = LoadImageFromDatabase(imageData);
                            }
                            else
                            {
                                pictureBox.Image = null; // No image available
                            }
                            itemPanel.Controls.Add(pictureBox);

                            // Add apartment details to the panel
                            Label detailsLabel = new Label
                            {
                                Text = $@"ID: {reader["Ap_id"]}
Location: {reader["Ap_location"]}
Rent: {reader["Monthly_rent"]}",
                                Font = new Font("Arial", 10, FontStyle.Regular),
                                AutoSize = true,
                                Top = pictureBox.Bottom + 10,
                                ForeColor = Color.Sienna
                            };
                            itemPanel.Controls.Add(detailsLabel);

                            // Add a 'Select' button to the panel
                            Button selectButton = new Button
                            {
                                Text = "Select",
                                Dock = DockStyle.Bottom,
                                Tag = reader["Ap_id"], // Store the ID in the button's Tag property
                                Height = 30,
                                BackColor = Color.Maroon,
                                ForeColor = Color.White,
                            };

                            selectButton.Click += (s, ev) =>
                            {
                                aaid = Convert.ToInt32(selectButton.Tag);
                                MessageBox.Show($"Selected Apartment ID: {aaid}", "Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                            itemPanel.Controls.Add(selectButton);

                            // Add the panel to the flow layout panel
                            flowLayoutPanel1.Controls.Add(itemPanel);
                        }

                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No matching rows found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }



            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

             query = @"
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                //MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadForm();
                return;
            }

            string query = @"
SELECT * 
FROM Apartment 
WHERE Nearby_institution LIKE @searchTerm 
OR Ap_location LIKE @searchTerm
OR Ap_id LIKE @searchTerm
OR Monthly_rent LIKE @searchTerm
OR Service_charge LIKE @searchTerm
OR L_id LIKE @searchTerm
OR Ap_id LIKE @searchTerm";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@searchTerm", SqlDbType.NVarChar).Value = "%" + searchValue + "%";

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        flowLayoutPanel1.Controls.Clear(); // Clear existing items

                        while (reader.Read())
                        {
                            // Create a new panel for each record
                            Panel itemPanel = new Panel
                            {
                                Width = 180,
                                Height = 250,
                                BorderStyle = BorderStyle.FixedSingle,
                                Padding = new Padding(10),
                                Margin = new Padding(10),
                                BackColor = Color.PeachPuff
                            };

                            // Add an image to the panel using PictureBox
                            PictureBox pictureBox = new PictureBox
                            {
                                Width = 180,
                                Height = 150,
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };

                            if (reader["Picture"] != DBNull.Value)
                            {
                                byte[] imageData = (byte[])reader["Picture"];
                                pictureBox.Image = LoadImageFromDatabase(imageData);
                            }
                            else
                            {
                                pictureBox.Image = null; // No image available
                            }
                            itemPanel.Controls.Add(pictureBox);

                            // Add apartment details to the panel
                            Label detailsLabel = new Label
                            {
                                Text = $@"ID: {reader["Ap_id"]}
Location: {reader["Ap_location"]}
Rent: {reader["Monthly_rent"]}",
                                Font = new Font("Arial", 10, FontStyle.Regular),
                                AutoSize = true,
                                Top = pictureBox.Bottom + 10,
                                ForeColor = Color.Sienna
                            };
                            itemPanel.Controls.Add(detailsLabel);

                            // Add a 'Select' button to the panel
                            Button selectButton = new Button
                            {
                                Text = "Select",
                                Dock = DockStyle.Bottom,
                                Tag = reader["Ap_id"], // Store the ID in the button's Tag property
                                Height = 30,
                                BackColor = Color.Maroon,
                                ForeColor = Color.White,
                            };

                            selectButton.Click += (s, ev) =>
                            {
                                aaid = Convert.ToInt32(selectButton.Tag);
                                MessageBox.Show($"Selected Apartment ID: {aaid}", "Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                            itemPanel.Controls.Add(selectButton);

                            // Add the panel to the flow layout panel
                            flowLayoutPanel1.Controls.Add(itemPanel);
                        }

                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No matching rows found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                //MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadForm();
                return;
            }

             query = @"
       SELECT * 
       FROM Apartment 
       WHERE Nearby_institution LIKE @searchTerm 
       OR Ap_location LIKE @searchTerm
       OR Ap_id LIKE @searchTerm
       OR Monthly_rent LIKE @searchTerm
       OR Service_charge LIKE @searchTerm
       OR L_id LIKE @searchTerm
       OR Ap_id LIKE @searchTerm";

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

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
                    Form10 form10 = new Form10(id,aid,b,c);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form = new Form2();
            form.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int idd = this.id;
            this.Close();
            Form3 fh = new Form3(idd, 1, 1);
            fh.Show();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Check if id and aaid have valid values
                if (id == 0 || aaid == 0) // Assuming id and aaid are integers and default to 0
                {
                    MessageBox.Show("ID or Apartment ID is not set. Please ensure a selection is made.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Navigate to Form10
                this.Hide();
                Form10 form10 = new Form10(id, aaid, b, c);
                form10.Show();
            }
            catch (Exception ex)
            {
                // Log and display any unexpected errors
                MessageBox.Show($"An error occurred: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 ff = new Form3(id,1,1);
            ff.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
