using Npgsql;
using System;

namespace WebApp1
{
    public class Log
    {
        public static void WriteLog(string Message)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MonitoringBD"].ConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                try
                {
                    connection.Open();
                    string sql = string.Format("INSERT INTO monitoringlog (logtime, logstring) VALUES(current_timestamp::timestamp,@message)");
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("message", serializer.Serialize(Message));
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    throw;

                }
                connection.Close();
            }
        }
    }
}