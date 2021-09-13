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
            Console.WriteLine("Doesn't support the extension .xls");
            Console.WriteLine(@"Put your file at D:\Downloads and name it accountmoveline.xlsx");
            
            ShipCheck();
        }

    }
}
