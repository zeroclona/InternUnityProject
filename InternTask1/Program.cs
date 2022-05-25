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

            Console.WriteLine("\nNow try it yourself!");
            var continueString = "yes";
            while (continueString == "yes")
            {
              Console.WriteLine("Calculator reads in RPN and supports: +, -, *, /, sqrt, %, ^ and operations with negative and double numbers");
              Console.WriteLine("Answer is "+Calculator.Calculate(Console.ReadLine()));

              Console.Write("\nDo you want continue? (Type 'yes') ");
              continueString = Console.ReadLine();
            }
        }
    }
}
