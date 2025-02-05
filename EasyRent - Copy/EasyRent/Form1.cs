using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;
using System.Text.RegularExpressions;


namespace EasyRent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";
            string Name = textBox1.Text.Trim();
            string Location = textBox2.Text.Trim();
            string Email = textBox3.Text.Trim();
            string PhoneNumber = textBox4.Text.Trim();
            string CreatePassword = textBox5.Text.Trim();
            string ConfirmPassword = textBox6.Text.Trim();

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Location) ||
                string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(PhoneNumber) ||
                string.IsNullOrWhiteSpace(CreatePassword) || string.IsNullOrWhiteSpace(ConfirmPassword))
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
            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Please enter a valid email address (e.g., someone@domain.com).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Name validation (only letters and spaces allowed)
            if (!IsValidName(Name))
            {
                MessageBox.Show("Name must only contain alphabetic characters and spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Location validation (basic check)
            if (Location.Length < 3)
            {
                MessageBox.Show("Location should be at least 3 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Phone Number validation (should be exactly 11 digits)
            if (!IsValidPhoneNumber(PhoneNumber))
            {
                MessageBox.Show("Phone number must be 11 digits long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CreatePassword != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Search == "Tenant")
            {
                // Step 1: Insert PhoneNumber into Tcontact table
                string query = "INSERT INTO Tcontact (T_pnumber) VALUES (@T_pnumber)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@T_pnumber", PhoneNumber);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected <= 0)
                        {
                            MessageBox.Show("Failed to insert phone number into Tcontact. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Step 2: Retrieve TT_conid from Tcontact table
                int ttConid = 0;
                query = "SELECT TT_conid FROM Tcontact WHERE T_pnumber = @T_pnumber";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@T_pnumber", PhoneNumber);

                            connection.Open();

                            object result = command.ExecuteScalar();

                            if (result != null)
                            {
                                ttConid = Convert.ToInt32(result);
                            }
                            else
                            {
                                MessageBox.Show("No matching record in Tenant contact for the given phone number. Cannot proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while retrieving Tenant contact id: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 3: Insert Tenant record using retrieved TT_conid
                query = "INSERT INTO Tenant (T_name, T_location, T_password, T_email, TT_conid) VALUES (@T_name, @T_location, @T_password, @T_email, @TT_conid)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@T_name", Name);
                        command.Parameters.AddWithValue("@T_location", Location);
                        command.Parameters.AddWithValue("@T_password", CreatePassword);
                        command.Parameters.AddWithValue("@T_email", Email);
                        command.Parameters.AddWithValue("@TT_conid", ttConid);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form2 f2 = new Form2();
                            f2.Show();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else if (Search == "Landlord")
            {


                // Step 1: Insert PhoneNumber into Lcontact table
                string query = "INSERT INTO Lcontact (L_pnumber) VALUES (@L_pnumber)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@L_pnumber", PhoneNumber);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected <= 0)
                        {
                            MessageBox.Show("Failed to insert phone number. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Step 2: Retrieve LL_conid from Lcontact table
                int llConid = 0;
                query = "SELECT LL_conid FROM Lcontact WHERE L_pnumber = @L_pnumber";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@L_pnumber", PhoneNumber);

                            connection.Open();

                            object result = command.ExecuteScalar();

                            if (result != null)
                            {
                                llConid = Convert.ToInt32(result);
                            }
                            else
                            {
                                MessageBox.Show("No matching record in Landlord contact for the given phone number. Cannot proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while retrieving Tandlord contact id: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                 query = "INSERT INTO Landlord (L_name, L_location, L_password,L_email,LL_conid) VALUES (@L_name, @L_location, @L_password,@L_email,@LL_conid)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@L_name", Name);
                        command.Parameters.AddWithValue("@L_location", Location);
                        command.Parameters.AddWithValue("@L_password", CreatePassword);
                        command.Parameters.AddWithValue("@L_email", Email);
                        command.Parameters.AddWithValue("@LL_conid", llConid);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form2 f2 = new Form2();
                            f2.Show();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }



        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }

        // Name validation (only alphabetic characters and spaces)
        private bool IsValidName(string name)
        {
            var nameRegex = new Regex(@"^[a-zA-Z\s]+$");
            return nameRegex.IsMatch(name);
        }

        // Phone number validation (must be exactly 11 digits)
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneRegex = new Regex(@"^\d{11}$");
            return phoneRegex.IsMatch(phoneNumber);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form12 ff = new Form12();
            ff.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

