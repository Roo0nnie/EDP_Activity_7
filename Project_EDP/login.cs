using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_EDP
{
    public partial class login : Form
    {
        private DatabaseConnection conn;
        public static string id;
        public login()
        {
            InitializeComponent();
            conn = new DatabaseConnection();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var forgot_pass = new forgot_pass();
            this.Hide();
            forgot_pass.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string student_number = text_number.Text;
            string password = text_pass.Text;

            if(string.IsNullOrEmpty(student_number) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Invalid Input", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(conn.OpenConnection())
            {
                try
                {
                    string sql = "SELECT students_id FROM students WHERE student_number = @student_number AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@student_number", student_number);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = reader["students_id"].ToString();
                            var dashboard = new dashboard();
                            this.Hide();
                            dashboard.Show();
                        }
                        else
                        {
                            MessageBox.Show("User not found in the database!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    conn.CloseConnection();
                }


            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void text_pass_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button_show_Click(object sender, EventArgs e)
        {
            if (text_pass.PasswordChar == '\0')
            {
                button_show.BringToFront();
                text_pass.PasswordChar = '*';
            }
        }

        private void button_hide_Click(object sender, EventArgs e)
        {
            if (text_pass.PasswordChar == '*')
            {
                button_hide.BringToFront();
                text_pass.PasswordChar = '\0';
            }

        }

        private void text_number_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
