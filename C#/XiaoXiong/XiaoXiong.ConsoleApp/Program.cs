using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiaoXiong.CheckQOH;

namespace XiaoXiong.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("计算中......稍等......");
            CheckQOH.CheckQOH.ShipCheck();
        }
    }
}
