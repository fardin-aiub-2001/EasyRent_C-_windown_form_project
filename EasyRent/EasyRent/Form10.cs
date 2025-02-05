using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EasyRent
{
    public partial class Form10 : Form
    {
        private int id, aid,b,c;
        string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI"; // Default automatically called at the first.Initialize with your database connection string
        public Form10()
        {
            InitializeComponent();
            

        }
        public Form10(int id,int aid,int b, int c)//viewing as tenant
        {
            InitializeComponent();
            this.id= id;
            this.aid = aid;
            this.b = b;
            this.c = c;
            LoadDetails();
            if (id == 903)
            {
                button2.Hide();
                button3.Hide();
            }
            

        }
        private void LoadDetails()
        {
            string lid=null;
            //preparing query
            string query = "SELECT * FROM Apartment WHERE Ap_id=@Ap_id";
            try//step 3: preparing connection and retriving data from server 
            {
                using (SqlConnection connection = new SqlConnection(connectionString))//
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Ap_id", aid);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // Populate the text boxes with the retrieved data

                            label8.Text = reader["Ap_id"]?.ToString() ?? string.Empty;//8 ap id
                            label10.Text = reader["Ap_location"]?.ToString() ?? string.Empty;//10 loc
                            label11.Text = reader["Monthly_rent"]?.ToString() ?? string.Empty;//11 rent 
                            label12.Text = reader["Utility_bill"]?.ToString() ?? string.Empty;//12 utility
                            label13.Text = reader["Service_Charge"]?.ToString() ?? string.Empty;//13 ser char
                            label14.Text = reader["Nearby_institution"]?.ToString() ?? string.Empty;//14 nearby inst
                            lid = reader["L_id"]?.ToString();
                            if (reader["Picture"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])reader["Picture"];
                                using (var ms = new MemoryStream(imageBytes))
                                {
                                    pictureBox2.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureBox2.Image = null; // Clear the PictureBox if no image is found
                            }

                        }
                        else
                        {
                            MessageBox.Show("No details found for the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close(); // Close the form if no data is found
                            Form2 form2 = new Form2();
                            form2.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (int.TryParse(lid, out int llid))
            {
                query = "SELECT * FROM Landlord WHERE L_id=@L_id";
                try//step 3: preparing connection and retriving data from server 
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))//
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@L_id",llid);
                            connection.Open();

                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                // Populate the text boxes with the retrieved data

                                label15.Text = reader["L_name"]?.ToString() ?? string.Empty;//15 landlord
                                label17.Text = reader["L_email"]?.ToString() ?? string.Empty;//17 landlord email

                            }
                            else
                            {
                                MessageBox.Show("No details found for the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Close(); // Close the form if no data is found
                                Form2 form2 = new Form2();
                                form2.Show();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 form = new Form3(id, 1, 1);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form11 ff = new Form11(id,aid);
            ff.Show();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id != 0 && id!=903)
            {
                this.Close();
                Form4 form = new Form4(id, 1, 1);
                form.Show();
            }
            

            else
            {
                this.Close();
                Form6 form = new Form6();
                form.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
