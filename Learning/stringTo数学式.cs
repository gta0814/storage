using System;

namespace demo
{

    public class ExpressionParser {
        public int Evaluate(string exp) {
            exp = exp.Replace("**", "^").Trim();
            return Eval(exp);
        }
        private int Eval(string exp){
            //Console.WriteLine($"Evaluating {exp}");
            var bracketCount = 0;
            var opIndex = -1;

            for(int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];
                if(c == '(') bracketCount++;
                else if(c == ')') bracketCount--;
                else if((c == '+' || c == '-') && bracketCount == 0){
                    opIndex = i;
                    break;
                }
                else if((c == '*' || c == '/' || c == '^') && bracketCount == 0 && opIndex < 0){
                    opIndex = i;
                }
            }

            if(opIndex < 0){
                exp = exp.Trim();
                if(exp[0] == '(' && exp[exp.Length - 1] == ')')
                {
                    return Eval(exp.Substring(1, exp.Length - 2));
                }
                else {
                    return int.Parse(exp);
                }
            }
            else{
                switch(exp[opIndex]){
                    case '+':
                        return Eval(exp.Substring(0, opIndex)) + Eval(exp.Substring(opIndex+1));
                    case '-':
                        return Eval(exp.Substring(0, opIndex)) - Eval(exp.Substring(opIndex+1));
                    case '*':
                        return Eval(exp.Substring(0, opIndex)) * Eval(exp.Substring(opIndex+1));
                    case '/':
                        return Eval(exp.Substring(0, opIndex)) / Eval(exp.Substring(opIndex+1));
                    case '^':
                        return (int)Math.Pow(Eval(exp.Substring(0, opIndex)), Eval(exp.Substring(opIndex+1)));
                }
            }
            return 0;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var exp = "(2+(3-1)*3)**3";
            exp = "(2-0)*(6/2)";
            
            var result = new ExpressionParser().Evaluate(exp);
            
            Console.WriteLine($"Result: {result}");
        }
    }
}