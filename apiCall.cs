using System;
using System.IO;
using System.Net;
using System.Text;


namespace httpClientDemo
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
                (Encoding.Default.GetBytes("My_Keys"));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            String jasonString = readStream.ReadToEnd();
            Console.WriteLine("----------------------------");
            Console.WriteLine(jasonString);
            response.Close();
            readStream.Close();
            Console.ReadLine();

            

        }
    }
    public class Rootobject
    {
        public Datum[] data { get; set; }
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
