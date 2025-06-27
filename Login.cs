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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=D:\inventorymanagementsystem\WindowsFormsApp1\WindowsFormsApp1\inventory.mdf;Integrated Security = True");
        private int i;

        public Login()
        {
            InitializeComponent();
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Create SQL Command
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM registration WHERE username = @username AND password = @password";

                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@username", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);

                // Fill DataTable
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                // Check if login details are valid
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("This username and password do not match.");
                }
                else
                {
                    this.Hide();
                    MDIParent1 mdi = new MDIParent1();
                    mdi.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            // Ensure connection is open
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


