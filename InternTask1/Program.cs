using System;

namespace InternTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trying to calculate: "+"12 42 2 * 5 / + 35 -");
            Console.WriteLine(Calculator.Calculate("12 42 2 * 5 / + 35 -"));

            Console.WriteLine("Trying to calculate: "+"3 4.76 -");
            Console.WriteLine(Calculator.Calculate("3 4.76 -"));

            Console.WriteLine("Trying to calculate: "+"3 - 4 +");
            Console.WriteLine(Calculator.Calculate("3 - 4 +"));

            Console.WriteLine("Trying to calculate: "+"3 - 4 *");
            Console.WriteLine(Calculator.Calculate("3 - 4 *"));

            Console.WriteLine("Trying to calculate: "+"3 4.5 ^");
            Console.WriteLine(Calculator.Calculate("3 4.5 ^"));

            Console.WriteLine("Trying to calculate: "+"3 6.7 + sqrt");
            Console.WriteLine(Calculator.Calculate("3 6.7 + sqrt"));

            Console.WriteLine("Now try it yourself! \nCalculator reads in RPN and supports: +, -, *, /, sqrt, %, ^ and operations with negative and double numbers");
            Console.WriteLine(Calculator.Calculate(Console.ReadLine()));
        }
    }
}
