using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW_POS_Server.Common
{
    public class WebAPIConnect
    {
        HttpClient client = new HttpClient();
        string WebAPIURL;
        string MediaType;
        public void connectOpen()
        {
            JsonCheckFile();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (StreamReader r = new StreamReader("ConnectWebAPI.json"))
            {
                string json = r.ReadToEnd();
                List< WebAPIConnectString> connectStringJson = JsonConvert.DeserializeObject<List<WebAPIConnectString>>(json);
                WebAPIURL = connectStringJson[0].WebAPIURL;
                MediaType = connectStringJson[0].MediaType;
            }
            try
            {
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            }
            catch(InvalidOperationException e)
            {
                return;
            }
        }
        public HttpResponseMessage ResponseGet(string requestURL)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync(requestURL).Result;
                return response;
            }
            catch (AggregateException ex)
            {
                MessageBox.Show("請通知後台人員開啟WebAPI以執行下一步\nDetail:\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public void ResponsePost(string requestURL, string stringJson)
        {
            try
            {
                HttpResponseMessage response = client.PostAsync(requestURL, new StringContent(stringJson, Encoding.UTF8, MediaType)).Result;
            }
            catch (AggregateException ex)
            {
                MessageBox.Show("請通知後台人員開啟WebAPI以執行下一步\nDetail:\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void ResponsePut(string requestURL, string stringJson)
        {
            try
            {
                HttpResponseMessage response = client.PutAsync(requestURL, new StringContent(stringJson, Encoding.UTF8, MediaType)).Result;
            }
            catch (AggregateException ex)
            {
                MessageBox.Show("請通知後台人員開啟WebAPI以執行下一步\nDetail:\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void JsonCheckFile()
        {
            DateTime NowTime = DateTime.Now;
            if (!File.Exists("ConnectWebAPI.json"))
            {
                List<WebAPIConnectString> webAPI = new List<WebAPIConnectString>();
                webAPI.Add(new WebAPIConnectString()
                {
                    WebAPIURL = "https://192.168.17.59:5001/",
                    MediaType = "application/json",
                });
                string json = JsonConvert.SerializeObject(webAPI.ToArray());
                File.WriteAllText(@"ConnectWebAPI.json", json);
            }
            else
                Console.WriteLine($@"WebAPI is existed. Execution time:{NowTime.ToString("HH:mm:ss")}");
        }
        private class WebAPIConnectString
        {
            public string WebAPIURL;
            public string MediaType;
        }
    }
}
