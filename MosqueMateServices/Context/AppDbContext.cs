using MosqueMateServices.DTOs;
using MosqueMateServices.Helper;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace MosqueMateServices.Context
{
    public class AppDbContext
    {
        private string server = "msoquemate.database.windows.net,1433";
        private string database = "mosquemateDB";
        private string username = "mahmoud74";
        private string password = "mahmoud@2024";
        SqlConnection connection;
        
        public AppDbContext()
        {
            string connString = $"Server={server};Initial Catalog={database};Persist Security Info=False;User ID={username};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            connection = new SqlConnection(connString);    
        }
        public void InsertIssue(DTOContactUs contactUs)
        {
            try
            {
                connection.Open();
                string insertQuery = "INSERT INTO mails(fname, lname, email, issue)  VALUES (@value1, @value2, @value3,@value4)";
                using SqlCommand cmd = new SqlCommand(insertQuery, connection);
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
        public bool SelectVersionUpdate()
        {
            try
            {
                connection.Open();
                string selectQuery = "Select IsUpdated from updateApp;";
                using SqlCommand command = new SqlCommand(selectQuery, connection);
                var result = command.ExecuteScalar();
                return Convert.ToBoolean(result);       

            }
            catch (Exception ex)
            {
                //using NotificationHelper notification = new NotificationHelper(true);
                //notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}
