using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_EDP
{
    public partial class student_information : Form
    {
        private DatabaseConnection conn;
        public student_information()
        {
            InitializeComponent();
            conn = new DatabaseConnection();
            panel_add.Hide();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            DateTime currTime = DateTime.Now;
            label_time.Text = currTime.ToString("dddd, dd/MM/yyyy hh:mm tt");
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_add_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text_cod = text_code.Text;
            string text_sub = text_subject.Text;
            string text_rom = text_room.Text;
            string text_tim = text_time.Text;

            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "INSERT INTO `newschema`.`subjects` (`sub_name`, `code`, `time`, `room`) " +
                                 "VALUES (@text_sub, @text_cod, @text_time, @text_room);";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@text_sub", text_sub);
                    cmd.Parameters.AddWithValue("@text_cod", text_cod);
                    cmd.Parameters.AddWithValue("@text_time", text_tim);
                    cmd.Parameters.AddWithValue("@text_room", text_rom);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Insert successful.");
                        panel_add.Hide();
                       
                    }
                    else
                    {
                        MessageBox.Show("No rows were inserted.");
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

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            var dashboard = new dashboard();
            this.Hide();
            dashboard.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel_add.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panel_add.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel_add.Hide();
        }

        private void student_information_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BindData()
        {
            try
            {
                // Open database connection
                if (conn.OpenConnection())
                {
                    // Create SQL query to select all users
                    string sqlQuery = "SELECT * FROM newschema.subjects";

                    // Create MySqlCommand object with SQL query and connection
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, conn.connection);

                    // Create DataAdapter to fetch data from database
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                    // Create DataTable to store the fetched data
                    DataTable originalDataTable = new DataTable();

                    // Fill the DataTable with data from the DataAdapter
                    adapter.Fill(originalDataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = originalDataTable;

                    // Optionally, you can hide specific columns by name or index
                    // Example: Hide the password column if it exists
                    if (originalDataTable.Columns.Contains("subject_id"))
                        dataGridView1.Columns["subject_id"].Visible = false;

                    // Close database connection
                    conn.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Failed to connect to database");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            string text_delete = textBox_delete.Text;

            if (conn.OpenConnection())
            {
                try
                {
                    string sql = "DELETE FROM `newschema`.`subjects` WHERE (`subject_id` = text_delete);";
                    MySqlCommand cmd = new MySqlCommand(sql, conn.connection);
                    cmd.Parameters.AddWithValue("@text_delete", text_delete);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Subject Deleted");

                    }
                    else
                    {
                        MessageBox.Show("No rows were deleted.");
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

        private void textBox_delete_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
