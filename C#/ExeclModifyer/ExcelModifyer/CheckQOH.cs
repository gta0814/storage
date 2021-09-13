using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelModifyer
{
    public class CheckQOH
    {
        //public void oldXiaoXiong()
        //{
        //    Dictionary<string, int> letterIndex = new Dictionary<string, int>() { { "A", 1 }, { "B", 2 }, { "C", 3 }, { "D", 4 }, { "E", 5 }, { "F", 6 }, { "G", 7 }, { "H", 8 }, { "I", 9 }, { "J", 10 }, { "K", 11 }, { "L", 12 }, { "M", 13 }, { "N", 14 } };
        //    // SpreadsheetLight works on the idea of a currently selected worksheet.
        //    // If no worksheet name is provided on opening an existing spreadsheet,
        //    // the first available worksheet is selected.
        //    Console.WriteLine("Doesn't support the extension .xls");
        //    Console.WriteLine(@"Put your file at D:\Downloads and name it accountmoveline.xlsx");
        //    SLDocument sl = new SLDocument(@"D:\Open Orders Report.xlsm");
        //    int row = 969;
        //    int column = 10;

        //    //Get debit 如果重复 debit.value += 1
        //    Dictionary<string, int> debit = new Dictionary<string, int>();
        //    for (int i = 2; i < row; i++)
        //    {
        //        string currentDebit = sl.GetCellValueAsString(i, letterIndex["F"]);
        //        if (!string.IsNullOrEmpty(currentDebit))
        //        {
        //            if (debit.Keys.Contains(currentDebit))
        //            {
        //                debit[currentDebit] += 1;
        //            }
        //            else
        //            {
        //                debit.Add(currentDebit, 1);
        //            }
        //        }
        //    }

        //    //Get credit 如果重复 credit.value += 1
        //    Dictionary<string, int> credit = new Dictionary<string, int>();
        //    for (int i = 2; i < row; i++)
        //    {
        //        string currentCredit = sl.GetCellValueAsString(i, letterIndex["H"]);
        //        if (!string.IsNullOrEmpty(currentCredit))
        //        {
        //            if (credit.Keys.Contains(currentCredit))
        //            {
        //                credit[currentCredit] += 1;
        //            }
        //            else
        //            {
        //                credit.Add(currentCredit, 1);
        //            }
        //        }
        //    }

        //    // 可以删的debit数
        //    List<string> needToDelete = new List<string>();
        //    foreach (var item in credit)
        //    {
        //        //debit里有这个credit
        //        if (debit.ContainsKey(item.Key))
        //        {
        //            Console.WriteLine(item.Key);
        //            for (int i = 2; i < row; i++)
        //            {
        //                string currentDebit = sl.GetCellValueAsString(i, letterIndex["G"]);
        //                string currentCredit = sl.GetCellValueAsString(i, letterIndex["H"]);

        //                //如果item.Key出现在debit或credit行
        //                if (item.Key == currentDebit || item.Key == currentCredit)
        //                {
        //                    //如果这个数只在debit和credit里出现一次 - 标记行为红色
        //                    if (debit[item.Key] == 1 && item.Value == 1)
        //                    {
        //                        SLStyle style = sl.CreateStyle();
        //                        style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightCoral, System.Drawing.Color.CornflowerBlue);
        //                        sl.SetRowStyle(i, style);
        //                        //sl.DeleteRow(i, 1);
        //                    }
        //                    else if (debit[item.Key] > 1 || item.Value > 1)
        //                    {
        //                        SLStyle style = sl.CreateStyle();
        //                        style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightSkyBlue, System.Drawing.Color.CornflowerBlue);
        //                        sl.SetRowStyle(i, style);
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    sl.SaveAs(@"D:\Downloads\accountmoveline Cleaned.xlsx");
        //}
        public static void ShipCheck()
        {
            Dictionary<string, int> letterIndex = new Dictionary<string, int>() { { "A", 1 }, { "B", 2 }, { "C", 3 }, { "D", 4 }, { "E", 5 }, { "F", 6 }, { "G", 7 }, { "H", 8 }, { "I", 9 }, { "J", 10 }, { "K", 11 }, { "L", 12 }, { "M", 13 }, { "N", 14 } };

            SLDocument dD = new SLDocument(@"C:\Github\storage\C#\ExeclModifyer\ExcelModifyer\aaa.xlsx");
            //SLDocument pO = new SLDocument(@"D:\SO billing based on Inventory Dates V1.xlsx", "PO");
            //SLDocument qOH = new SLDocument(@"D:\SO billing based on Inventory Dates V1.xlsx", "Inventory QoH Sep 09");


            //var a = dD.GetCellValueAsString("B5");
            //var b = pO.GetCellValueAsString("A5");
            //var c = qOH.GetCellValueAsString("A5");
            //int row = 9999;
            //int column = 20;

            //dD.SetCellValue("A6", "5555");
            //pO.SetCellValue("A6", "6666");
            //qOH.SetCellValue("A6", "7777");
            dD.SaveAs(@"Open Orders Report1.xlsx");
            //pO.SaveAs(@"D:\Open Orders Report1.xlsx");
            //qOH.SaveAs(@"D:\Open Orders Report1.xlsx");
            Console.WriteLine("Press ANY key");
        }
    }
}
