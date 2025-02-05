using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EasyRent
{
    public partial class Form5 : Form
    {
        private int id, b,c,a;
        private readonly string connectionString;

        public Form5()
        {
            InitializeComponent();
            connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI"; // Default automatically called at the first.Initialize with your database connection string
        }

        public Form5(int id, int b,int c) : this() 
        {
            this.id = id;
            this.b = b;
            this.c = c;
            LoadDetails();
            label6.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
            label13.Hide();
            label14.Hide();
            if (c == 1)
            {
                label15.Hide();

            }
            else if (c == 2)
            {
                label2.Hide();
            }
           // dateTimePicker1.Hide();
            if (b == 3)
            {
                label15.Hide();
                label2.Hide();
            }

        }
        public Form5(int id, int b, int c, int a) : this()
        {
            this.id = id;
            this.b = b;
            this.c = c;
            this.a = a;
            LoadDetails();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox7.Hide();
            if (b != 3) 
            { 
            button2.Hide();
            button3.Hide(); 
            }
            
            if (c == 1)
            {
                label15.Hide();

            }
            else if (c == 2)
            {
                label2.Hide();
            }
            dateTimePicker1.Hide();
            if (b == 3)
            {
                label15.Hide();
                label2.Hide();
            }
        }

        private void LoadDetails()
        {
            //loading tenant
            if( (b == 1 && c==1)||(b == 3 && c == 1))
            {
                string query = "SELECT T_id, T_name, T_location, T_email, T_nid, T_dob FROM Tenant WHERE T_id = @T_id";//Preparing query:step2 to connect database
                                                                                                                       //the query type is string 

                try//step 3: preparing connection and retriving data from server 
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))//
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@T_id", id);
                            connection.Open();

                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                // Populate the text boxes with the retrieved data
                                textBox2.Text = reader["T_id"]?.ToString() ?? string.Empty;
                                textBox1.Text = reader["T_name"]?.ToString() ?? string.Empty;
                                textBox3.Text = reader["T_location"]?.ToString() ?? string.Empty;
                                textBox4.Text = reader["T_email"]?.ToString() ?? string.Empty;
                                textBox7.Text = reader["T_nid"]?.ToString() ?? string.Empty;

                                label6.Text = reader["T_id"]?.ToString() ?? string.Empty;
                                label10.Text = reader["T_name"]?.ToString() ?? string.Empty;
                                label11.Text = reader["T_location"]?.ToString() ?? string.Empty;
                                label12.Text = reader["T_email"]?.ToString() ?? string.Empty;
                                label13.Text = reader["T_dob"]?.ToString() ?? string.Empty;
                                label14.Text = reader["T_nid"]?.ToString() ?? string.Empty;

                                if (DateTime.TryParse(reader["T_dob"]?.ToString(), out DateTime dob))
                                {
                                    dateTimePicker1.Value = dob;
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
            }
           
            //loading landlord
            else if( (b == 2 || c == 3)|| (b == 2 || c == 2))
            {
                
                {
                    string query = "SELECT L_id, L_name, L_location, L_email, L_nid, L_dob FROM Landlord WHERE L_id = @L_id";//Preparing query:step2 to connect database
                                                                                                                           //the query type is string 

                    try//step 3: preparing connection and retriving data from server 
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))//
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@L_id", id);
                                connection.Open();

                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.Read())
                                {
                                    // Populate the text boxes with the retrieved data
                                    textBox2.Text = reader["L_id"]?.ToString() ?? string.Empty;
                                    textBox1.Text = reader["L_name"]?.ToString() ?? string.Empty;
                                    textBox3.Text = reader["L_location"]?.ToString() ?? string.Empty;
                                    textBox4.Text = reader["L_email"]?.ToString() ?? string.Empty;
                                    textBox7.Text = reader["L_nid"]?.ToString() ?? string.Empty;


                                    label6.Text = reader["L_id"]?.ToString() ?? string.Empty;
                                    label10.Text = reader["L_name"]?.ToString() ?? string.Empty;
                                    label11.Text = reader["L_location"]?.ToString() ?? string.Empty;
                                    label12.Text = reader["L_email"]?.ToString() ?? string.Empty;
                                    label13.Text = reader["L_dob"]?.ToString() ?? string.Empty;
                                    label14.Text = reader["L_nid"]?.ToString() ?? string.Empty;

                                    if (DateTime.TryParse(reader["L_dob"]?.ToString(), out DateTime dob))
                                    {
                                        dateTimePicker1.Value = dob;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No details found for the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Close(); // Close the form if no data is found
                                    Form6 form2 = new Form6();
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
            
        }

        // Update Button (button2)
        private void button2_Click(object sender, EventArgs e)
        {
            //update tenant
            if ((b == 1 && c == 1) || (b == 3 && c == 1))
            {
                try
                {
                    string query = "UPDATE Tenant SET T_name = @T_name, T_location = @T_location, T_email = @T_email, T_nid = @T_nid, T_dob = @T_dob WHERE T_id = @T_id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@T_id", id);
                            command.Parameters.AddWithValue("@T_name", textBox1.Text);
                            command.Parameters.AddWithValue("@T_location", textBox3.Text);
                            command.Parameters.AddWithValue("@T_email", textBox4.Text);
                            command.Parameters.AddWithValue("@T_nid", textBox7.Text);
                            command.Parameters.AddWithValue("@T_dob", dateTimePicker1.Value);

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record was updated.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //update landlord
            else if ((b == 2 && c == 2) || (b == 3 && c == 2))
            {
                try
                {
                    string query = "UPDATE Landlord SET L_name = @L_name, L_location = @L_location, L_email = @L_email, L_nid = @L_nid, L_dob = @L_dob WHERE L_id = @L_id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@L_id", id);
                            command.Parameters.AddWithValue("@L_name", textBox1.Text);
                            command.Parameters.AddWithValue("@L_location", textBox3.Text);
                            command.Parameters.AddWithValue("@L_email", textBox4.Text);
                            command.Parameters.AddWithValue("@L_nid", textBox7.Text);
                            command.Parameters.AddWithValue("@L_dob", dateTimePicker1.Value);

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record was updated.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //deleting tenant
            if ((b == 3 &&c==1)||(b == 1 && c == 1))
            {
                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete account?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Tenant WHERE T_id = @T_id";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@T_id", id);

                                connection.Open();//opening connection
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    if (b == 1)//go back to login page
                                    {
                                        MessageBox.Show("Tenant deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close(); // Close the form after deletion
                                        Form2 ff = new Form2();
                                        ff.Show();
                                    }
                                    if (b == 3)//go back to admin dashboard
                                    {
                                        MessageBox.Show("Tenant deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close(); // Close the form after deletion
                                        Form6 ff = new Form6();
                                        ff.Show();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No record was deleted.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
           //deleting landlord
            else if (b == 2 || c == 2)
            {
                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete account?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Landlord WHERE L_id = @L_id";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@L_id", id);

                                connection.Open();//opening connection
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Landlord deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close(); // Close the form after deletion
                                    Form2 ff = new Form2();
                                    ff.Show();
                                }
                                else
                                {
                                    MessageBox.Show("No record was deleted.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (b == 3 || c == 2)
            {
                // Confirm deletion 
                DialogResult result = MessageBox.Show("Are you sure you want to delete account?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //deleting landlord
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Landlord WHERE L_id = @L_id";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@L_id", id);

                                connection.Open();//opening connection
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Landlord deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close(); // Close the form after deletion
                                    Form6 ff = new Form6();
                                    ff.Show();
                                }
                                else
                                {
                                    MessageBox.Show("No record was deleted.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (b == 1) 
            {
            this.Close();
            Form3 form3 = new Form3(id, 1, 1);
            form3.Show(); 
            }

            else if (b == 2)
            {
                this.Close();
                Form3 form3 = new Form3(id, 2, 2);
                form3.Show();
            }
            else if (b == 3)
            {
                this.Close();
                Form6 ff=new Form6();
                ff.Show();
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
        }
    }
}
