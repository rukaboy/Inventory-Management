using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp1
{
    public partial class add_new_user : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\inventorymanagementsystem\WindowsFormsApp1\WindowsFormsApp1\inventory.mdf;Integrated Security=True");
        public add_new_user()   
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Create SQL Command to check for existing username
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM registration WHERE username = @username";

                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@username", textBox3.Text);

                // Fill DataTable
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count == 0) // Username not found, proceed to insert
                {
                    // Create SQL Command to insert new record
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "INSERT INTO registration (firstname, lastname, username, password, email, contact) " +
                                       "VALUES (@firstname, @lastname, @username, @password, @email, @contact)";

                    // Add parameters for insertion
                    cmd1.Parameters.AddWithValue("@firstname", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@lastname", textBox2.Text);
                    cmd1.Parameters.AddWithValue("@username", textBox3.Text);
                    cmd1.Parameters.AddWithValue("@password", textBox4.Text);
                    cmd1.Parameters.AddWithValue("@email", textBox5.Text);
                    cmd1.Parameters.AddWithValue("@contact", textBox6.Text);

                    // Execute Insert Command
                    cmd1.ExecuteNonQuery();

                    // Clear input fields
                    textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
                    textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = "";

                    MessageBox.Show("User record inserted successfully.");

                    // Refresh DataGridView after insertion
                    RefreshGridView();
                }
                else
                {
                    MessageBox.Show("This username is already registered. Please choose another.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

private void add_new_user_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            // Load data into DataGridView on form load
            RefreshGridView();
        }

        // Method to refresh DataGridView
        private void RefreshGridView()
        {
            try
            {
                // Fetch all data from registration table
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM registration";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                // Bind data to DataGridView
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while refreshing data grid: " + ex.Message);
            }
        }

    }
}
