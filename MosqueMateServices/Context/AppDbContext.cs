using MosqueMateServices.DTOs;
using MosqueMateServices.Helper;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace MosqueMateServices.Context
{
    public class AppDbContext
    {
        private string server = "localhost";
        private string database = "mosquematedb";
        private string username = "root";
        private string password = "";
        public void InsertIssue(DTOContactUs contactUs)
        {
            string connectionString = $"Server={server};Database={database};User ID={username};Password={password};SslMode=none";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO mails(fname, lname, email, issue)  VALUES (@value1, @value2, @value3,@value4)";

                    using MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@value1", contactUs.fname);
                    cmd.Parameters.AddWithValue("@value2", contactUs.lname);
                    cmd.Parameters.AddWithValue("@value3", contactUs.email);
                    cmd.Parameters.AddWithValue("@value4", contactUs.issue);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thank you,We Will Support you soon", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    using NotificationHelper notification = new NotificationHelper(true);
                    notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public bool SelectVersionUpdate()
        {
            string connectionString = $"Server={server};Database={database};User ID={username};Password={password};SslMode=none";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "Select IsUpdated from updateApp;";
                    using MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    return (bool)command.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    using NotificationHelper notification = new NotificationHelper(true);
                    notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
                return false;
            }
        }
    }
}
