using Npgsql;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace WebApp1
{
    public partial class Index : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string MonitoringCPU()
        {
            Log.WriteLog("Отображаем загрузку CPU");
            System.Management.ManagementObjectSearcher man =
            new System.Management.ManagementObjectSearcher("SELECT LoadPercentage  FROM Win32_Processor");
            string objec = "";
            foreach (System.Management.ManagementObject obj in man.Get())
            {
                objec += obj["LoadPercentage"].ToString() + "%";
            }
            return objec;
        }

        [WebMethod]
        public static void FillingDB(string datefixed, int valnumcpu, int valnumram, string logstring)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MonitoringBD"].ConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    if (logstring != null)
                    {
                        string sql = string.Format("INSERT INTO monitoringlog (logtime, logstring) VALUES(current_timestamp::timestamp,@message)");
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                        {
                            Thread.Sleep(1000);
                            cmd.Parameters.AddWithValue("message", serializer.Serialize(logstring));
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        string sql = string.Format("INSERT INTO monitoringdata (uidata, datetime, valuenumcpu, valuenumram) VALUES(GEN_RANDOM_UUID(),@datefixed::timestamp, @valnumcpu , @valnumram)");
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("datefixed", serializer.Serialize(datefixed));
                            cmd.Parameters.AddWithValue("valnumcpu", valnumcpu);
                            cmd.Parameters.AddWithValue("valnumram", valnumram);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            Thread.Sleep(1000);
                            Log.WriteLog("Передаем значение CPU или RAM из меню");
                        }
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    Log.WriteLog("Не удалось передать значение из меню в базу данных.");
                    connection.Close();
                    throw;
                }
                connection.Close();
            }
        }
        [WebMethod]
        public static void ClearingDB()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MonitoringBD"].ConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sqldata = string.Format("Delete from monitoringdata");
                    string sqlog = string.Format("Delete from monitoringlog");
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqldata, connection))
                    {
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlog, connection))
                    {
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();                      
                    }
                    Thread.Sleep(1000);
                        Log.WriteLog("Значения полей в базе данных удалены.");
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    Log.WriteLog("Не удалось удалить значения из бд");
                    connection.Close();
                    throw;
                }
                connection.Close();
            }
        }
    }
}
