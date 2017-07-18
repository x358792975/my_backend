using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace exportExcel
{
    // This program is working for:
    // Connecting to database
    // running queries and return data from database
    // show them on the screen and save to an excel file
    // Note: NOT Working for
    // Input part(Query part)
    // Excel process keeps running in the background
    // Excel files are Read-Only
    // Files naming part need to optimaized.
    class Program
    {

        public static String connectString = @"Data Source=SEAN;Initial Catalog = mydb; Integrated Security = True";

        static void Main(string[] args)
        {


            System.Data.DataTable dtTable = new System.Data.DataTable("mydb.dbo.stock_historical");

            var list = new List<Dictionary<string, object>>();
            using (SqlConnection con = new SqlConnection(connectString))
            {
                String query_Insert = "INSERT into mydb.dbo.stock_table(stock_name, openP,highP,lowP)values ('BBBB', 49.74, 50.8, 48.99)";
                String query_Select = "select stock_name, openP, highP, lowP from stock_table";
                String query_SelectHis = "select ticker, stock_date, open_market, high, low, close_market, volumn FROM stock_historical ";

                String query_SelectAAA = "SELECT ticker, stock_date, open_market, high, low, close_market, volumn FROM stock_historical where ticker = 'AAPL'";

                String query_SelectBBB = "SELECT ticker, stock_date, close_market FROM stock_historical where ticker = 'A' ";
                using (SqlCommand cmd = new SqlCommand(query_SelectBBB, con))
                {
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtTable);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();



                    foreach (DataRow row in dtTable.Rows)
                    {
                        var dict = new Dictionary<string, object>();
                        foreach (DataColumn col in dtTable.Columns)
                        {
                            dict[col.ColumnName] = (Convert.ToString(row[col]));
                        }
                        list.Add(dict);
                    }
                    String outputString = serializer.Serialize(list);
                    //Console.WriteLine(serializer.Serialize(list));
                    Console.WriteLine(outputString);

                    //writeToFile(outputString);
                    Console.WriteLine(list.Count);

                   /* Console.WriteLine(String.Format("Stock Name" + "\t" + "Date" + "\t" + "Open Price" + "\t" + "High Price" + "\t" + "Low Prince" + "\t" + "Close Price" + "\t" + "Volumn" + "\t"));
                    foreach (DataRow row in dtTable.Rows)
                    {
                        //Console.WriteLine(String.Format(row[0] + "\t" + row[1] + "\t\t" + row[2] + "\t\t" + row[3] + "\t" +row[4] +"\t" + row[5] + "\t"+row[6]));
                    }*/
                   
                    writeToFile(outputString);
                     excelFile(dtTable, list);
                    con.Close();
                }

                Console.WriteLine("Done!");

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
        private static void excelFile(System.Data.DataTable dtTable, List<Dictionary<string, object>> list)
        {
            // File save dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";

            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.FileName = null;
            saveFileDialog.Title = "Save path of the file to be exported";
            
            //string filepath = AppDomain.CurrentDomain.BaseDirectory;

            Excel.Application xlApp = null;
            Excel.Workbooks wkbooks = null;
            Excel.Workbook wkbook = null;
            Excel.Sheets wksheets = null;
            Excel.Worksheet wksheet = null;

            try
            {
                xlApp = new Excel.Application();
                wkbooks = xlApp.Workbooks;
                wkbook = wkbooks.Add();
                wksheets = wkbook.Sheets;
                wksheet = wksheets.Add();
           


                Console.WriteLine(list[0].Values);
                wksheet.Name = "APPLE";

                try
                {
                Console.WriteLine("It is working.");
                for (var i = 0; i < dtTable.Columns.Count; i++)
                {
                    wksheet.Cells[1, i + 1] = dtTable.Columns[i].ColumnName;
                }

                //rows
                    for (var i = 0; i < dtTable.Rows.Count; i++)
                    {
                        for (var j = 0; j < dtTable.Columns.Count; j++)
                        {
                            wksheet.Cells[i + 2, j + 1] = dtTable.Rows[i][j];
                        }
                    }
                //wkbook.SaveAs(saveFileDialog, XlFileFormat.xlExcel8,false,
                //false, false,false, XlSaveAsAccessMode.xlNoChange, Type.Missing,
                //Type.Missing, Type.Missing,Type.Missing, Type.Missing);

                    Console.WriteLine("Processing!");

                    Console.WriteLine("File saved.");
    
                    wkbooks.Close();
                    wkbook.Close(false, Missing.Value, Missing.Value);
                    xlApp.Quit();      
               
                }
                catch(Exception exp)
                {
                   
                }
             }
            catch (Exception e)
            {

                //Console.WriteLine(e.ToString());
            }

            finally
            {
                if (wksheets != null) Marshal.ReleaseComObject(wksheets);
                if (wkbook != null) Marshal.ReleaseComObject(wkbook);
                if (wkbooks != null) Marshal.ReleaseComObject(wkbooks);
                if (xlApp != null) Marshal.ReleaseComObject(xlApp);
            }
        }
    }
}