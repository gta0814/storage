using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var pw = "One23456";
            var d = "doie321";
            Console.WriteLine(Config.Encrypt(pw));
            Console.ReadKey();
        }
    }
}
