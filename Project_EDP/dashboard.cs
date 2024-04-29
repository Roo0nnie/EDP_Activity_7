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

namespace Project_EDP
{
    public partial class dashboard : Form
    {
        private DatabaseConnection conn;
        string user_id = login.id;
        string username = "";
        string number = "";
        string address = "";
        string email = "";
        string password = "";
        string id_number = "";

        public dashboard()
        {
            InitializeComponent();
            conn = new DatabaseConnection();
            panel_profile.Hide();

            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "SELECT * FROM students WHERE students_id = @user_id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username = reader["student_name"].ToString();
                            number = reader["number"].ToString();
                            address = reader["address"].ToString();
                            email = reader["email"].ToString();
                            password = reader["password"].ToString();
                            id_number = reader["student_number"].ToString();

                            label_username.Text = username;
                            text_update_name.Text = username;
                            text_update_number.Text = number;
                            text_update_address.Text = address;
                            text_update_email.Text = email;
                            text_update_password.Text = password;
                            text_update_student_number.Text = id_number;
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

            DateTime currTime = DateTime.Now;
            label_time.Text = currTime.ToString("dddd, dd/MM/yyyy hh:mm tt");
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = new login();
            this.Hide();
            login.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_time_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {
            panel_profile.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel_profile.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string complete_name = text_update_name.Text;
            string number = text_update_number.Text;
            string address = text_update_address.Text;
            string email = text_update_email.Text;
            string password = text_update_password.Text;

            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "UPDATE `newschema`.`students` SET `student_name` = @complete_name, " +
                     "`number` = @number, `address` = @address, `password` = @password," +
                     " `email` = @email WHERE (`students_id` = @user_id);";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@complete_name", complete_name);
                    cmd.Parameters.AddWithValue("@number", number);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update successful.");
                        panel_profile.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            panel_profile.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var student_Information = new student_information();
            this.Hide();
            student_Information.Show();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
