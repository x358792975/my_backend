using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;


namespace jsonParsingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            String pre = @"https://api.intrinio.com/prices?identifier=";
            String mid = "SNAP";

            String url2 = @"https://api.intrinio.com/prices?identifier=SNAP&start_date=2017-06-27";
            String url = pre + mid;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);

            request.Method = "GET";
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String
                (Encoding.Default.GetBytes("My_Key"));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            String jsonString = "";

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            jsonString = readStream.ReadToEnd();
            // Console.WriteLine(readStream.ReadToEnd());
            //Console.WriteLine(jsonString);
            response.Close();
            readStream.Close();


            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //var jsonObject = serializer.Deserialize<Datum>(jsonString);

            //var dataDictionary = serializer.Deserialize<Dictionary<string, int>>(jsonString);

            var obj = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Rootobject>(jsonString);

            Console.WriteLine("The open price on " +　obj.data[0].date +  " is " + obj.data[0].open);
            Console.WriteLine("The low price on " + obj.data[0].date + " is " + obj.data[0].low);
            Console.WriteLine("The high price on " + obj.data[0].date + " is " + obj.data[0].high);
            Console.WriteLine("The close price on " + obj.data[0].date + " is " + obj.data[0].close);
            Console.WriteLine("The adj_volume price on " + obj.data[0].date + " is " + obj.data[0].adj_volume);

            Console.WriteLine("The open price price on " + obj.data[1].date + " is " + obj.data[1].open);
            Console.WriteLine("The low price on " + obj.data[1].date + " is " + obj.data[1].low);
            Console.WriteLine("The high price on " + obj.data[1].date + " is " + obj.data[1].high);
            Console.WriteLine("The close price on " + obj.data[1].date + " is " + obj.data[1].close);
            Console.WriteLine("The adj_volume price on " + obj.data[1].date + " is " + obj.data[1].adj_volume);



            Console.WriteLine(obj.result_count);
            Console.WriteLine(obj.page_size);
            Console.WriteLine(obj.current_page);
            Console.WriteLine(obj.total_pages);
            Console.WriteLine(obj.api_call_credits);


            //DataContractJsonSerializer abc = new DataContractJsonSerializer();
            Console.ReadLine();
        }
    }
    public class Rootobject
    {
        public List<Datum> data { get; set; }
        public int result_count { get; set; }
        public int page_size { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public int api_call_credits { get; set; }
    }

    public class Datum
    {
        public string date { get; set; }
        public float open { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float close { get; set; }
        public float volume { get; set; }
        public float ex_dividend { get; set; }
        public float split_ratio { get; set; }
        public float adj_open { get; set; }
        public float adj_high { get; set; }
        public float adj_low { get; set; }
        public float adj_close { get; set; }
        public float adj_volume { get; set; }
    }
}
