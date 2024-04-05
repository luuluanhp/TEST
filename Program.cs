using System.Net;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;


namespace TEST
{
    class ABC
    {
        public class clsResult
        {
            public string ERROR_CODE { get; set; }
            public string MESSAGE { get; set; }
            public string DATA { get; set; }
        }

        public class clsInput
        {
            public string token { get; set; }
            public string ticket { get; set; }
            public string servicePath { get; set; }
        }
        public static string MakePostRequest(string baseUrl, string parameters)
        {
            try
            {
                string kq = "";
                WebRequest request = WebRequest.Create(baseUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                byte[] postData = Encoding.UTF8.GetBytes(parameters);
                request.ContentLength = postData.Length;
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(postData, 0, postData.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            kq = reader.ReadToEnd();
                        }
                    }
                }

                return kq;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static void Main()
        {
            string ticket = "ST-200502-bch9pGKxtbCSiBfMmc1x-cas";
            string servicePath = "http://10.165.11.65/Login.aspx";
            string baseUrl = "http://toolone.vnptit.io.vn/api/User/report_getuser_id";

            clsInput cin = new clsInput();
            cin.ticket = ticket;
            cin.servicePath = servicePath;
            cin.token = "e6LdVveCNfAYGxWrzq5knA==";
            var parameters = JsonContent.SerializeObject(cin);
            

            string kq = MakePostRequest(baseUrl, parameters);

            var ds = JsonConverter.DeserializeObject<clsResult>(kq);
            if (ds.ERROR_CODE == "0")
            {
                //Thành công
                msg = ds.MESSAGE;
                return ds.DATA;

            }
        }
    }
}