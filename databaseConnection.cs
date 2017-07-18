using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;

namespace demo2
{
    class Program
    {
        //public static ConnectionStringSettings testDB = ConfigurationManager.ConnectionStrings["ConnectionString"];
       // public static String testDBString = testDB.ConnectionString;
        public static String connectString = @"Data Source=SEAN;Initial Catalog = mydb; Integrated Security = True";
        static void Main(string[] args)
        {
            /*
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

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            Console.WriteLine(readStream.ReadToEnd());
            response.Close();
            readStream.Close();
            Console.ReadLine();*/


            /*here works 
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                //adapter.SelectCommand = new SqlCommand(String.Format(@"SELECT  stock_name FROM mydb.dbo.stock_table"), connection);
                adapter.SelectCommand = new SqlCommand(String.Format(@"INSERT into mydb.dbo.stock_table(stock_name, openP,highP,lowP)values ('AMZN', 998.09, 999, 997); "), connection);
                adapter.Fill(ds, "mydb.dbo.stock_table");
                dt = ds.Tables["mydb.dbo.stock_table"];
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row);
                }

            

                Console.ReadLine();

            };*/

            DataTable dtTable = new DataTable("mydb.dbo.stock_table");
            using (SqlConnection con = new SqlConnection(connectString))
            {
                String query_Insert = "INSERT into mydb.dbo.stock_table(stock_name, openP,highP,lowP)values ('BBBB', 49.74, 50.8, 48.99)";
                String query_Select = "select stock_name, openP, highP, lowP from stock_table";

                using (SqlCommand cmd = new SqlCommand(query_Select, con))
                {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtTable);

                    //System.Web.Script.Serialization.JavascriptSerializer
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    /*
                    List<Dictionary<string, object>> my_rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> my_row;
                    foreach(DataRow dr in dtTable.Rows)
                    {
                        my_row = new Dictionary<string, object>();
                        foreach(DataColumn dcol in dtTable.Columns)
                        {
                            my_row.Add(dcol.ColumnName, dr[0]);
                        }
                        my_rows.Add(my_row);
                    }
                   
                    for (int i = 0; i < my_rows.Count; i++)
                    {
                        Console.WriteLine(i + " = " + my_rows[i]);
                    }*/
                    var list = new List<Dictionary<string, object>>();
                    foreach(DataRow row in dtTable.Rows)
                    {
                        var dict = new Dictionary<string, object>();
                        foreach(DataColumn col in dtTable.Columns)
                        {
                            dict[col.ColumnName] = (Convert.ToString(row[col]));
                        }
                        list.Add(dict);
                    }
                    String outputString = serializer.Serialize(list);
                    //Console.WriteLine(serializer.Serialize(list));
                    Console.WriteLine(outputString);

                    writeToFile(outputString);
                    Console.WriteLine(list.Count);

                    Console.WriteLine(String.Format("Stock Name" + "\t" + "Open Price" + "\t" +"High Price"+ "\t" + "Low Prince" +  "\t"));
                    foreach (DataRow row in dtTable.Rows)
                    {
                        Console.WriteLine(String.Format(row[0] + "\t" + row[1] + "\t\t" + row[2] + "\t\t" + row[3]));
                    }
                con.Close();
 
                }



            }
            Console.ReadLine();
        }

        private static void writeToFile(String outputString)
        {
            using (StreamWriter writetext = new StreamWriter("output.txt"))
            {
                writetext.WriteLine(outputString);
                Console.WriteLine("Wrote into the file");
            }

        }


    }
    public class Datum
    {
        public string date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
        public double ex_dividend { get; set; }
        public double split_ratio { get; set; }
        public double adj_open { get; set; }
        public double adj_high { get; set; }
        public double adj_low { get; set; }
        public double adj_close { get; set; }
        public double adj_volume { get; set; }
    }
    public class RootObject
    {
        public List<Datum> data { get; set; }
        public int result_count { get; set; }
        public int page_size { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public int api_call_credits { get; set; }
    }

   
}
