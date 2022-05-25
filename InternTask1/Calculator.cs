using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;


namespace InternTask1
{
  public class Calculator
  {
    private Calculator(){}

    public static double Calculate(string inStr){
      var stack = new Stack<double>();
      var calcStringSeparator = new char[1]{' '};
      foreach (string word in inStr.Split(calcStringSeparator, System.StringSplitOptions.RemoveEmptyEntries))
      {
        double op2;
        double num;
        if (double.TryParse(word,NumberStyles.Any, CultureInfo.InvariantCulture, out num)){
          stack.Push(num);
        }
        else{
          switch (word)
          {
            case "+":
              stack.Push(stack.Pop() + stack.Pop());
              break;
            case "-":
              op2 = stack.Pop();
              try{
                stack.Push(stack.Pop() - op2);
              }
              catch (InvalidOperationException) {
                stack.Push(-op2);
              }
              break;
            case "*":
              stack.Push(stack.Pop() * stack.Pop());
              break;
            case "/":
              op2 = stack.Pop();
              if (op2 != 0.0)
                stack.Push(stack.Pop() / op2);
              else
                Console.WriteLine("Ошибка! Попытка деления на ноль.");
              break;

            case "sqrt":
              op2 = stack.Pop();
              if (op2 >= 0.0)
                stack.Push(Math.Sqrt(op2));
              else
                Console.WriteLine("Ошибка! Попытка взятия корня из отрицательного числа.");
              break;
            case "^":
              op2 = stack.Pop();
              stack.Push(Math.Pow(stack.Pop(),op2));
              break;
            case "%":
              stack.Push(stack.Pop() * 0.01);
              break;
            default:
              Console.WriteLine("Ошибка! Введена нераспознанная подстрока: "+word);
              break;
          }
        }
      }
      return stack.Peek();
    }
  }
}
