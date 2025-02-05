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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace EasyRent
{
    public partial class Form9 : Form
    {
        private int id, b, c;
        public Form9()
        {
            InitializeComponent();
            
        }
        public Form9(int id,int b,int c)
        {
            InitializeComponent();
            this.id = id;
            this.b = b;
            this.c = c;
            if (id == 903)
            {
                button2.Hide();
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";
            string Apartment_Location = textBox6.Text.Trim();
            //string Apartment_id = textBox4.Text.Trim();
           // string L_ID = textBox5.Text.Trim();
            string Nearby_institution = textBox7.Text.Trim();
            string Monthly_rent = textBox1.Text.Trim();
            string Utility_bill = textBox2.Text.Trim();
            string Service_charge = textBox3.Text.Trim();


            if (string.IsNullOrWhiteSpace(Apartment_Location)  ||
                string.IsNullOrWhiteSpace(Nearby_institution) || string.IsNullOrWhiteSpace(Monthly_rent) ||
                string.IsNullOrWhiteSpace(Utility_bill) || string.IsNullOrWhiteSpace(Service_charge))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string Search = "";
            if (radioButton1.Checked)
            {
                Search = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                Search = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                Search = radioButton3.Text;
            }
            else if (radioButton4.Checked)
            {
                Search = radioButton4.Text;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

               

                // Insert into Apartment table
                string query = 
                               "INSERT INTO Apartment (Utility_bill, Nearby_institution, Ap_location, Monthly_rent, Service_charge, L_id,Picture) VALUES (@Utility_bill, @Nearby_institution, @Ap_location, @Monthly_rent, @Service_charge, @L_id,@Picture); ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Utility_bill", Utility_bill);
                        command.Parameters.AddWithValue("@Nearby_institution", Nearby_institution);
                        command.Parameters.AddWithValue("@Ap_location", Apartment_Location);
                        command.Parameters.AddWithValue("@Monthly_rent", Monthly_rent);
                        command.Parameters.AddWithValue("@Service_charge", Service_charge);
                        command.Parameters.AddWithValue("@L_id", id);
                        command.Parameters.AddWithValue("@Picture", getPhoto());

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (b == 3 && c == 3)
                            {
                                this.Hide();
                                Form6 form6 = new Form6(id);
                                form6.Show();
                            }
                            else
                            {
                                this.Hide();
                                Form3 ff = new Form3(id, 2, 2);
                                ff.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        // Handle SQL exceptions (e.g., database connection errors, command errors)
                        MessageBox.Show($"SQL Error: {sqlEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle other general exceptions
                        MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private byte[] getPhoto()
        {
            MemoryStream stream = new MemoryStream();
            pictureBox3.Image.Save(stream,pictureBox3.Image.RawFormat);
            return stream.GetBuffer();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image=new Bitmap(openFileDialog.FileName);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 fh = new Form3(id,b,c);
            fh.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
