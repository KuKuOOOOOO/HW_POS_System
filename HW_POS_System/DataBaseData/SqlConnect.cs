using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW_POS_Server.DataBaseData
{
    public class SqlConnect
    {
        public SqlConnection sqlConnection { get; set; }
        private string conString { get; set; }
        public void connectOpen()
        {
            List<ConnectString> connectStrings = new List<ConnectString>();
            JsonCheckFile();
            using (StreamReader r = new StreamReader("ConnectDB.json"))
            {
                string json = r.ReadToEnd();
                List<ConnectString> connectStringJson = JsonConvert.DeserializeObject<List<ConnectString>>(json);
                connectStrings = connectStringJson;
            }
            conString = "Data Source=" + connectStrings[0].DataSource +
                        ";Initial Catalog=" + connectStrings[0].InitCatalog +
                        ";User ID=" + connectStrings[0].UserID +
                        ";Password=" + connectStrings[0].UserPassword;
            sqlConnection = new SqlConnection(conString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void connectionClose()
        {
            sqlConnection.Close();
        }
        private void JsonCheckFile()
        {
            DateTime NowTime = DateTime.Now;
            if (!File.Exists("ConnectDB.json"))
            {
                List<ConnectString> connectStrings = new List<ConnectString>();
                connectStrings.Add(new ConnectString()
                {
                    DataSource = "192.168.17.59",
                    InitCatalog = "POS_System_Server",
                    UserID = "",
                    UserPassword = "",
                });
                string json = JsonConvert.SerializeObject(connectStrings.ToArray());
                File.WriteAllText(@"ConnectDB.json", json);
            }
            else
                Console.WriteLine($@"Database is existed. Execution time:{NowTime.ToString("HH:mm:ss")}");

        }
        private class ConnectString
        {
            public string DataSource;
            public string InitCatalog;
            public string UserID;
            public string UserPassword;
        }
    }
}
