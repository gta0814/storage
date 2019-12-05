using System;

namespace test
{
    class Program : calculation
    {
        static void Main(string[] args)
        {
            //var pw = "FP8NEsilUH19BzSvmSb8Yw==";
            //var d = "doie321";

            //Console.WriteLine(pw.GetType().GetProperty);
            var a = new nulber();
            a.print();
            Console.ReadKey();
        }
    }
    class calculation
    {
        public double add (double a, double b)
        {
            return a + b;
        }
    }
    class nulber : calculation
    {
        public void print()
        {
            Console.WriteLine(base.add(1, 2));
        }
        
    }
}
