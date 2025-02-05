using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace EasyRent
{
    public partial class Form11 : Form
    {
        private int aid,id,adv,tot, rentValue,utilityValue,serviceValue;
        private int a, s, u, t;//a=advance;s=service ;u=utility;t=total
        string connectionString = "data source=LAPTOP-47MRR41O\\SQLEXPRESS; database=EasyRent; integrated security=SSPI";
        public Form11()
        {
            InitializeComponent();
           
        }
        public Form11(int id,int aid)
        {
            InitializeComponent();
            this.aid = aid;
            this.id = id;
            LoadDetails();
        }
        private void LoadDetails()
        {
            // Declare variables at the method scope
            string rent = string.Empty, service = string.Empty, utility = string.Empty;

            string query = "SELECT * FROM Apartment WHERE Ap_id=@Ap_id";
            try
            {
                // Step 3: Preparing connection and retrieving data from the server
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Ap_id", aid);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // Populate the variables with retrieved data
                            rent = reader["Monthly_rent"]?.ToString() ?? string.Empty; // Monthly rent
                            utility = reader["Utility_bill"]?.ToString() ?? string.Empty; // Utility bill
                            service = reader["Service_Charge"]?.ToString() ?? string.Empty; // Service charge
                        }
                        else
                        {
                            // Handle no data found
                            MessageBox.Show("No details found for the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close(); // Close the form if no data is found
                            Form2 form2 = new Form2();
                            form2.Show();
                            return; // Exit the method after showing Form2
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred while loading details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method on error
            }

            // Convert strings to integers
             rentValue = 0;
             utilityValue = 0;
             serviceValue = 0;

            if (!int.TryParse(rent, out rentValue))
            {
                MessageBox.Show("Invalid rent value. Setting to 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            if (!int.TryParse(utility, out utilityValue))
            {
                MessageBox.Show("Invalid utility bill value. Setting to 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.u=utilityValue;
            if (!int.TryParse(service, out serviceValue))
            {
                MessageBox.Show("Invalid service charge value. Setting to 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.s=serviceValue;
            label10.Text=utilityValue.ToString();
            label13.Text=serviceValue.ToString();
            adv = rentValue * 2;
            this.a= adv;
            label9.Text=adv.ToString();
            tot = adv + utilityValue + serviceValue;
            label11.Text=tot.ToString();
            this.t=tot; 
            label12.Text=id.ToString();
            // Values are now stored in rentValue, utilityValue, and serviceValue
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form13 form13 = new Form13(id,a,s,u,t);
            form13.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 form10 = new Form10();
            form10.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string updateTenantQuery = "UPDATE Tenant SET Ap_id = @Ap_id, Payment_id = @Payment_id WHERE T_id = @T_id";
            string insertPaymentQuery = "INSERT INTO Payment (Total_bill) OUTPUT INSERTED.Payment_id VALUES (@Total_bill)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert into Payment table and retrieve the generated Payment_id
                        using (SqlCommand insertPaymentCommand = new SqlCommand(insertPaymentQuery, connection, transaction))
                        {
                            insertPaymentCommand.Parameters.AddWithValue("@Total_bill", tot); 
                            int paymentId = (int)insertPaymentCommand.ExecuteScalar();

                            // Update Tenant table with Ap_id and the generated Payment_id
                            using (SqlCommand updateTenantCommand = new SqlCommand(updateTenantQuery, connection, transaction))
                            {
                                updateTenantCommand.Parameters.AddWithValue("@Ap_id", aid);
                                updateTenantCommand.Parameters.AddWithValue("@Payment_id", paymentId);
                                updateTenantCommand.Parameters.AddWithValue("@T_id", id);

                                int rowsAffected = updateTenantCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    transaction.Commit(); // Commit transaction on success
                                    MessageBox.Show("Your Payment Has Been Done Successfully",
                                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    throw new Exception("No record was updated. Please check the Tenant ID.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback transaction on failure
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }




        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }
    }
}
