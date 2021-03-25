using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;


namespace WebApp1.API
{
    public class MonitoringData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MonitoringBD"].ConnectionString;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    string varName = context.Request.Params["nametable"];
                    connection.Open();
                    if (varName == "monitoringdata")
                    {
                        string sql = string.Format("SELECT VALUENUMCPU, VALUENUMRAM, DATETIME::TIMESTAMP::time " +
                        "FROM monitoringdata WHERE DATETIME::TIMESTAMP::DATE = (CURRENT_TIMESTAMP - INTERVAL '3 hour')::date ORDER BY DATETIME");
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                        {
                            NpgsqlDataReader reader = cmd.ExecuteReader();
                            List<object> values = new List<object>();
                            List<object> values2 = new List<object>();
                            List<object> valdate = new List<object>();
                            while (reader.Read())
                            {
                                if ((Convert.IsDBNull(reader["VALUENUMCPU"]) == true &&
                                    Convert.IsDBNull(reader["VALUENUMRAM"]) == true))
                                { }
                                else
                                {
                                    values.Add(reader["VALUENUMCPU"]);
                                    values2.Add(reader["VALUENUMRAM"]);
                                    valdate.Add((Convert.ToString(reader["DATETIME"])).Substring(0, 8));
                                }

                            }
                            List<object>[] reqArray = new List<object>[3];
                            reqArray[0] = values;
                            reqArray[1] = values2;
                            reqArray[2] = valdate;
                            context.Response.ContentType = "application/json";
                            context.Response.Write(serializer.Serialize(reqArray));
                            Thread.Sleep(1000);
                            Log.WriteLog("Получены данные CPU : " + string.Join(", ", reqArray[0]) + " и RAM: " + string.Join(", ", reqArray[1]));
                        }
                    }
                    else if (varName == "monitoringlog")
                    {
                        string sql = string.Format("SELECT logtime::timestamp::time, logstring FROM monitoringlog ORDER BY logtime DESC LIMIT 1");
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                        {
                            NpgsqlDataReader reader = cmd.ExecuteReader();
                            List<string> Result = new List<string>();
                            context.Response.ContentType = "application/json";
                            var dtr = reader.Read();
                            if (dtr == true)
                            {
                                context.Response.Write(serializer.Serialize(Convert.ToString(reader["logtime"]).Substring(0, 11) + "  -  " + Convert.ToString(reader["logstring"]) + "<br \\/>"));
                            }
                        }
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                    Log.WriteLog("Не удалось отобразить значения из бд на страницу");
                    connection.Close();
                    throw;
                }
                connection.Close();
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}