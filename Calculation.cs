using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Numeraxial.Downloader
{
    public class Calculation
    {


        public void CalucationData()
        {
            try
            {
                //Database connection
                DataTable lDtStocks60RealTimeVals;

                //double bb1 = Math.Round(0.009874, 3, MidpointRounding.AwayFromZero);

                //double jj = bb1;

                DataTable lDtStockTable = new DataTable();
                string connstring = (System.Configuration.ConfigurationSettings.AppSettings["DBPath"]);

                SqlConnection lSqlConnection = new SqlConnection(connstring);
                lSqlConnection.Open();
                SqlCommand lCmdStock = new SqlCommand("GetTotalDistinctStock", lSqlConnection);
                lCmdStock.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter lADPFillStock = new SqlDataAdapter(lCmdStock);
                lADPFillStock.Fill(lDtStockTable);
                lADPFillStock.Dispose();
                lCmdStock.Dispose();

                SqlParameter lSQLPrmStockID;
                for (int j = 0; j < lDtStockTable.Rows.Count; j++)
                {

                    int STid = Convert.ToInt32(lDtStockTable.Rows[j]["StockId"]);

                    //  STid = 37;

                    if (lSqlConnection.State == ConnectionState.Closed)
                    {
                        lSqlConnection.Open();
                    }



                    //SqlCommand lCmd60StockValues = new SqlCommand("GetTop60bySotockID", lSqlConnection);

                    SqlCommand lCmd60StockValues = new SqlCommand("testData", lSqlConnection);
                    lCmd60StockValues.CommandType = CommandType.StoredProcedure;

                    //# - Parameter 1
                    lSQLPrmStockID = new SqlParameter("@StockId", SqlDbType.Int);
                    lSQLPrmStockID.Value = STid;
                    lCmd60StockValues.Parameters.Add(lSQLPrmStockID);


                    lDtStocks60RealTimeVals = new DataTable();
                    SqlDataAdapter lADPFillRealTimeVal = new SqlDataAdapter(lCmd60StockValues);
                    lADPFillRealTimeVal.Fill(lDtStocks60RealTimeVals);
                    lADPFillRealTimeVal.Dispose();


                    // List<int> lScal = new List<int>();
                    List<decimal> lRcal = new List<decimal>();
                    List<decimal> localList = new List<decimal>();

                    #region Old Logic
                    //if (lDtStocks60RealTimeVals.Rows.Count >= 60)
                    //{
                    //    for (int i = 1; i <= lDtStocks60RealTimeVals.Rows.Count - 1; i++)
                    //    {

                    //        //Calculation Formula
                    //        if (i % 59 == 0)
                    //        {
                    //            localList.Add(Convert.ToDecimal(lDtStocks60RealTimeVals.Rows[i]["AdjClose"]));

                    //            //Calculation
                    //            int k = 0;
                    //            while (k < localList.Count())
                    //            {
                    //                decimal num1 = localList[k];
                    //                k++;
                    //                if (k <= localList.Count() - 1)
                    //                {
                    //                    decimal num2 = localList[k];

                    //                    decimal Rnum = (num2 - num1) / num1;
                    //                    lRcal.Add(Rnum);
                    //                }
                    //            }

                    //            lRcal.Sort();
                    //            decimal pos = (lRcal[3] + lRcal[4]) / 2;
                    //            decimal VaR = 500 * (lRcal[3] + lRcal[4]);
                    //            int rid = Convert.ToInt32(lDtStocks60RealTimeVals.Rows[i]["RealTimeId"]);

                    //            SqlCommand lCmdVARCalc = new SqlCommand("usp_SaveCalculation", lSqlConnection);
                    //            lCmdVARCalc.CommandType = CommandType.StoredProcedure;

                    //            lCmdVARCalc.Parameters.AddWithValue("@StockId", STid);
                    //            lCmdVARCalc.Parameters.AddWithValue("@Rpercentile", pos);
                    //            lCmdVARCalc.Parameters.AddWithValue("@ValueatRisk", VaR);
                    //            lCmdVARCalc.Parameters.AddWithValue("@Ttime", DateTime.Now);
                    //            lCmdVARCalc.Parameters.AddWithValue("@RealTimeId", rid);
                    //            lCmdVARCalc.ExecuteNonQuery();
                    //            lCmdVARCalc.Dispose();
                    //            localList = new List<decimal>();

                    //        }
                    //        else
                    //        {
                    //            if (localList.Count <= lDtStocks60RealTimeVals.Rows.Count)
                    //            {
                    //                localList.Add(Convert.ToDecimal(lDtStocks60RealTimeVals.Rows[i]["AdjClose"]));
                    //            }
                    //        }
                    //        //End Calculation

                    //    }
                    //}
                    #endregion


                    if (lDtStocks60RealTimeVals.Rows.Count == 100)
                    {
                        DataView view = lDtStocks60RealTimeVals.DefaultView;
                        view.Sort = "Diff ASC";
                        DataTable sortedDate = view.ToTable();

                        int intCount = Convert.ToInt32(sortedDate.Rows.Count);
                        decimal decFirst = Convert.ToDecimal(sortedDate.Rows[0]["AdjClose"]);
                        decimal decLast = Convert.ToDecimal(sortedDate.Rows[intCount - 1]["AdjClose"]);
                        DateTime dtimeLast = Convert.ToDateTime(lDtStocks60RealTimeVals.Rows[intCount - 1]["CreatedDateTime"]);
                        DateTime dtimeFirst = Convert.ToDateTime(lDtStocks60RealTimeVals.Rows[0]["CreatedDateTime"]);

                        //  decimal NParVaR = Math.Round(Convert.ToDecimal(sortedDate.Rows[4]["R"]), 6);

                        decimal decfinal = 0;
                        decimal decFValue = 0;
                        decimal decCValue = 0;
                        double dd = 0;
                        string strss = "";
                        //for (int i = 0; i <= sortedDate.Rows.Count - 1; i++)
                        //{
                        //    decimal decff = 0;


                        //     decFValue = Math.Round(Convert.ToDecimal(sortedDate.Rows[i]["Diff"]), 6);

                        //    //if (decFValue != 0)
                        //    //{
                        //    //    dd = Convert.ToDouble(sortedDate.Rows[i]["Diff"]);
                        //    //}
                        //    //else
                        //    //{
                        //    //    dd = 0;
                        //    //}
                        //    //decfinal = decfinal + decFValue;


                        //    //decFValue = Math.Round(Convert.ToDecimal(sortedDate.Rows[i]["Diff"]), 6);
                        //    // decff = decFValue;

                        //    //else
                        //    //{
                        //    //    decCValue = Math.Round(Convert.ToDecimal(lDtStocks60RealTimeVals.Rows[i]["Diff"]), 3);
                        //    //    decff = Math.Round(Convert.ToDecimal(decCValue / decFValue), 6);
                        //    //    decFValue = decCValue;
                        //    //}

                        //  //  decfinal = decfinal + Convert.ToDecimal(decFValue);
                        //     if (decFValue != 0)
                        //    {
                        //        dd = Math.Log(Convert.ToDouble(decFValue));
                        //    }

                        //    if (strss == "")
                        //    {
                        //        strss = Convert.ToString(dd);
                        //    }
                        //    else
                        //    {
                        //        strss = strss + "," + Convert.ToString(dd).Replace("-", "");
                        //    }
                        //}



                        for (int i = 0; i <= sortedDate.Rows.Count - 1; i++)
                        {
                            decimal decff = 0;
                            decff = Convert.ToDecimal(sortedDate.Rows[i]["Diff"]);


                            if (strss == "")
                            {
                                strss = Convert.ToString(decff);
                            }
                            else
                            {
                                strss = strss + "," + Convert.ToString(decff);
                            }
                        }

                        string[] tempArray = strss.Split(',');

                        List<double> temps = new List<double>();
                        double[] ddff = new double[100];

                        foreach (string temp in tempArray)
                            temps.Add(double.Parse(temp));

                        for (int y = 0; y < tempArray.Length; y++)
                        {
                            ddff[y] = Convert.ToDouble(tempArray[y]);
                        }


                        Extend objclass = new Extend();

                        Array.Sort(ddff);

                        double ff = objclass.percentile(ddff, 1);
                        ff = -ff;

                        // double gg = objclass.percentile(ddff, 0.01);
                        // double gg = objclass.percentile(ddff, 0.01);

                        // double ddd = objclass.getStandardDeviation(temps);

                        // decimal ww= Convert.ToDecimal(gg);

                        double hh = 10 * Convert.ToDouble(ff);

                        decimal decCC = Math.Round((1000 * decfinal), 6);


                        // double dblcc =Convert.ToDouble(-decCC) * 0.01;
                        double dblcc = Convert.ToDouble(decCC) * 0.01;

                        double bb = Math.Round(hh, 3, MidpointRounding.AwayFromZero);


                        //int rid = Convert.ToInt32(lDtStocks60RealTimeVals.Rows[i]["RealTimeId"]);



                        SqlCommand lCmdVARCalc = new SqlCommand("SP_StockCalculation", lSqlConnection);
                        lCmdVARCalc.CommandType = CommandType.StoredProcedure;

                        lCmdVARCalc.Parameters.AddWithValue("@StockId", STid);
                        lCmdVARCalc.Parameters.AddWithValue("@StartValue", decFirst);
                        lCmdVARCalc.Parameters.AddWithValue("@LastValue", decLast);
                        lCmdVARCalc.Parameters.AddWithValue("@StartDate", dtimeFirst);
                        lCmdVARCalc.Parameters.AddWithValue("@EndDate", dtimeLast);
                        // lCmdVARCalc.Parameters.AddWithValue("@NParVaR", dblcc);
                        lCmdVARCalc.Parameters.AddWithValue("@NParVaR", bb);
                        lCmdVARCalc.ExecuteNonQuery();
                        lCmdVARCalc.Dispose();


                    }
                }
                lSqlConnection.Close();
                //lSqlConnection.Dispose();
                //lSqlConnection = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Read File Catch Exc: " + ex.Message);
                Console.WriteLine(ex.StackTrace.ToString());

            }

        }



    }

    public struct VP
    {
        public double Value;
        public double Proportion;
    }

    public class Extend
    {
        //public double percentile(double[] sequence, double excelpercentile)
        //{
        //Array.Sort(sequence);
        //int N = sequence.Length;
        //double n = (N - 1) * excelpercentile + 1;
        //// Another method: double n = (N + 1) * excelpercentile;

        //if (n == 1d) return sequence[0];
        //else if (n == N) return sequence[N - 1];
        //else
        //{
        //    int k = (int)n;
        //    double d = n - k;
        //    return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
        //}
        public double percentile(double[] sortedData, double p)
        {
            if (p >= 100.0d) return sortedData[sortedData.Length - 1];

            double position = (double)(sortedData.Length + 1) * p / 100.0;
            double leftNumber = 0.0d, rightNumber = 0.0d;

            double n = p / 100.0d * (sortedData.Length - 1) + 1.0d;

            if (position >= 1)
            {
                leftNumber = sortedData[(int)System.Math.Floor(n) - 1];
                rightNumber = sortedData[(int)System.Math.Floor(n)];
            }
            else
            {
                leftNumber = sortedData[0]; // first data
                rightNumber = sortedData[1]; // first data
            }

            if (leftNumber == rightNumber)
                return leftNumber;
            else
            {
                double part = n - System.Math.Floor(n);
                return leftNumber + part * (rightNumber - leftNumber);
            }

        }
        public double getStandardDeviation(List<float> doubleList)
        {
            double average = doubleList.Average();
            double sumOfDerivation = 0;
            foreach (double value in doubleList)
            {
                sumOfDerivation += (value) * (value);
            }
            double sumOfDerivationAverage = sumOfDerivation / (doubleList.Count - 1);
            return Math.Sqrt(sumOfDerivationAverage - (average * average));
        }
    }

}
