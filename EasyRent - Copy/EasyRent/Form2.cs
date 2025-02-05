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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Email = textBox1.Text;
            string Password = textBox2.Text;

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

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter both Id and Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int count=0;
            if (Search == "Tenant")
            {
                string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";

                string query = "SELECT COUNT(*) FROM Tenant WHERE T_email = @T_email AND T_password COLLATE SQL_Latin1_General_CP1_CS_AS = @T_password";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@T_email", Email);
                        command.Parameters.AddWithValue("@T_password", Password);

                        connection.Open();
                        count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            int T_id = 0;
                            query = "SELECT T_id FROM Tenant WHERE T_email = @T_email";

                            try
                            {
                                using (SqlCommand idCommand = new SqlCommand(query, connection))
                                {
                                    idCommand.Parameters.AddWithValue("@T_email", Email);
                                    object result = idCommand.ExecuteScalar();

                                    if (result != null)
                                    {
                                        T_id = Convert.ToInt32(result);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No matching record in Tenant for the given email. Cannot proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"An error occurred while retrieving Tenant: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form3 f3 = new Form3(T_id,1, 1);
                            f3.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Email or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }



            else if (Search == "Landlord")
            {
                string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";

                string query = "SELECT COUNT(*) FROM Landlord WHERE L_email = @L_email AND L_password COLLATE SQL_Latin1_General_CP1_CS_AS = @L_password";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@L_email", Email);
                        command.Parameters.AddWithValue("@L_password", Password);

                        connection.Open();
                        count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            int L_id = 0;
                            query = "SELECT L_id FROM Landlord WHERE L_email = @L_email";

                            try
                            {
                                using (SqlCommand idCommand = new SqlCommand(query, connection))
                                {
                                    idCommand.Parameters.AddWithValue("@L_email", Email);
                                    object result = idCommand.ExecuteScalar();

                                    if (result != null)
                                    {
                                        L_id = Convert.ToInt32(result);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No matching record in Landlord for the given email. Cannot proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"An error occurred while retrieving Landlord: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form3 f3 = new Form3(L_id,2,2);
                            f3.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Email or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }











            else if (Search == "Admin")
            {
                string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";

                string query = "SELECT COUNT(*) FROM Admin WHERE A_email = @A_email AND A_password COLLATE SQL_Latin1_General_CP1_CS_AS = @A_password";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@A_email", Email);
                        command.Parameters.AddWithValue("@A_password", Password);

                        connection.Open();
                        count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            

                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form6 f3 = new Form6();
                            f3.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Email or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }












            else
            {
                MessageBox.Show("Invalid selection. Please select Tenant or Landlord.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
