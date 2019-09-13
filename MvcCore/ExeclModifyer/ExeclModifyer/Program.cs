using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExeclModifyer
{
    class Program
    {
        //HashTable和Dictionary的区别：
        //(1).HashTable不支持泛型，而Dictionary支持泛型。
        //(2). Hashtable 的元素属于 Object 类型，所以在存储或检索值类型时通常发生装箱和拆箱的操作，所以你可能需要进行一些类型转换的操作，而且对于int,float这些值类型还需要进行装箱等操作，非常耗时。
        //(3).单线程程序中推荐使用 Dictionary, 有泛型优势, 且读取速度较快, 容量利用更充分。多线程程序中推荐使用 Hashtable, 默认的 Hashtable 允许单线程写入, 多线程读取, 对 Hashtable 进一步调用 Synchronized() 方法可以获得完全线程安全的类型.而 Dictionary 非线程安全, 必须人为使用 lock 语句进行保护, 效率大减。
        //(4)在通过代码测试的时候发现key是整数型Dictionary的效率比Hashtable快，如果key是字符串型，Dictionary的效率没有Hashtable快。

        static void Main(string[] args)
        {
            Dictionary<string, int> letterIndex = new Dictionary<string, int>() { { "A", 1 }, { "B", 2 }, { "C", 3 }, { "D", 4 }, { "E", 5 }, { "F", 6 }, { "G", 7 }, { "H", 8 }, { "I", 9 }, { "J", 10 }, { "K", 11 }, { "L", 12 }, { "M", 13 }, { "N", 14 } };

            // SpreadsheetLight works on the idea of a currently selected worksheet.
            // If no worksheet name is provided on opening an existing spreadsheet,
            // the first available worksheet is selected.
            Console.WriteLine("Doesn't support the extension .xls");
            Console.WriteLine(@"Put your file at D:\Downloads and name it accountmoveline.xlsx");
            Console.WriteLine("Press ANY key");
            Console.ReadKey();
            SLDocument sl = new SLDocument(@"D:\Downloads\accountmoveline.xlsx");
            int row = 969;
            int column = 10;

            //Get debit 如果重复 debit.value += 1
            Dictionary<string, int> debit = new Dictionary<string, int>();
            for (int i = 2; i < row; i++)
            {
                string currentDebit = sl.GetCellValueAsString(i, letterIndex["G"]);
                if (!string.IsNullOrEmpty(currentDebit))
                {
                    if (debit.Keys.Contains(currentDebit))
                    {
                        debit[currentDebit] += 1;
                    }
                    else
                    {
                        debit.Add(currentDebit, 1);
                    }
                }
            }

            //Get credit 如果重复 credit.value += 1
            Dictionary<string, int> credit = new Dictionary<string, int>();
            for (int i = 2; i < row; i++)
            {
                string currentCredit = sl.GetCellValueAsString(i, letterIndex["H"]);
                if (!string.IsNullOrEmpty(currentCredit))
                {
                    if (credit.Keys.Contains(currentCredit))
                    {
                        credit[currentCredit] += 1;
                    }
                    else
                    {
                        credit.Add(currentCredit, 1);
                    }
                }
            }

            // 可以删的debit数
            List<string> needToDelete = new List<string>();
            foreach (var item in credit)
            {
                //debit里有这个credit
                if (debit.ContainsKey(item.Key))
                {
                    Console.WriteLine(item.Key);
                    for (int i = 2; i < row; i++)
                    {
                        string currentDebit = sl.GetCellValueAsString(i, letterIndex["G"]);
                        string currentCredit = sl.GetCellValueAsString(i, letterIndex["H"]);

                        //如果item.Key出现在debit或credit行
                        if (item.Key == currentDebit || item.Key == currentCredit)
                        {
                            //如果这个数只在debit和credit里出现一次 - 标记行为红色
                            if (debit[item.Key] == 1 && item.Value == 1)
                            {
                                SLStyle style = sl.CreateStyle();
                                style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightCoral, System.Drawing.Color.CornflowerBlue);
                                sl.SetRowStyle(i, style);
                                //sl.DeleteRow(i, 1);
                            }
                            else if (debit[item.Key] > 1 || item.Value > 1)
                            {
                                SLStyle style = sl.CreateStyle();
                                style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightSkyBlue, System.Drawing.Color.CornflowerBlue);
                                sl.SetRowStyle(i, style);
                            }
                        }

                    }
                }
            }
            sl.SaveAs(@"D:\Downloads\accountmoveline Cleaned.xlsx");
        }
    }
}
