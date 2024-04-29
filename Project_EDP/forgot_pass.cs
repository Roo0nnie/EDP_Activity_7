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
using System.Net;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_EDP
{
    public partial class forgot_pass : Form

    {
        private DatabaseConnection conn;
        string sendCode;
        string password;
        string email = "";
        string to = "";
       
        public forgot_pass()
        {
            InitializeComponent();
            conn = new DatabaseConnection();
            verify_code.Hide();
            panel_password.Hide();
        }

        private void forgot_pass_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var login = new login();
            this.Hide();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string student_number = text_number.Text;
            bool verify = false;

            if (string.IsNullOrEmpty(student_number))
            {
                MessageBox.Show("Invalid Input", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (conn.OpenConnection())
            {
                string sql = "SELECT email FROM students WHERE student_number = @student_number";
                MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                cmd.Parameters.AddWithValue("@student_number", student_number);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        email = reader["email"].ToString();
                        verify = true;
                    }
                }

                if(verify == true) 
                {
                    string from, pass, messageBody;
                    Random rand = new Random();
                    MailMessage message = new MailMessage();
                    sendCode = (rand.Next(999999)).ToString();
                    to = email;
                    from = email;
                    pass = "lskyddlzjabamiwq";
                    messageBody = "Your password recovery code: " + sendCode;

                    message.To.Add(to);
                    message.From = new MailAddress(from);
                    message.Body = messageBody;
                    message.Subject = "Password Recovery Code";

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, pass);

                    try
                    {
                        smtp.Send(message);
                        MessageBox.Show("Code send successfully");
                        verify_code.Show();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
 
                } else
                {
                    verify_code.Hide();
                }
  
                conn.CloseConnection();

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            var login = new login();
            this.Hide();
            login.Show();
        }

        private void text_code_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string verify_text = text_code.Text;
            conn.connection.Open();

            if (verify_text == sendCode)
            {
               
                string sql = "SELECT password FROM students WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn.connection))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            password = reader["password"].ToString();
                            text_recoverPass.Text = password;
                            panel_password.Show();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Code not match!");
            }
            conn.CloseConnection();
        }

        private void text_recoverPass_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
