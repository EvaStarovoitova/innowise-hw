using System;
namespace ex1
{
    public interface ICalc
    {
        double Calculate(double num1, double num2);
    }
    
    public class Addition : ICalc
    {
        public double Calculate(double num1, double num2)
        {
            return num1 + num2;
        }
    }

    public class Division : ICalc
    {
        public double Calculate(double num1, double num2)
        {
            if (num2 != 0)
            {
                return num1 / num2;
            }
            else
            {
                throw new DivideByZeroException("Нельзя делить на ноль!");
            }                     
        }
    }

    public class Multiplication : ICalc
    {
        public double Calculate(double num1, double num2)
        {
            return num1 * num2;
        }
    }

    public class Subtraction : ICalc
    {
        public double Calculate(double num1, double num2)
        {
            return num1 - num2;
        }
    }
    class Calculator
    {
        public ICalc Operation { get; set; }

        public double Result(double num1, double num2)
        {
            return Operation.Calculate(num1, num2);
        }
    }    
    class Program
    {
        private static double InputFromUser(string message)
        {
            double number;

            while (true)
            {
                Console.WriteLine(message);

                if (double.TryParse(Console.ReadLine(), out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Введите число!");
                }
            }
        }
        private static ICalc GetOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                    return new Addition();                 
                case "-":
                   return new Subtraction();
                case "*":
                   return new Multiplication();
                case "/":
                    return new Division();
                default:
                    return null;
            }
        }
        private static string GetOperationFromUser()
        {
            Console.WriteLine("Введите одну из операций (+, -, /, *)");
            return Console.ReadLine();     
        }
        private static void RunCalculation(Calculator calculator, double firstNum, double secondNum, string operation)
        {
            try
            {
                calculator.Operation = GetOperation(operation);
                if (calculator.Operation == null)
                {
                    Console.WriteLine("Введите одну из предложенных операций! (+, -, /, *)");
                    return;
                }

                double result = calculator.Result(firstNum, secondNum);
                Console.WriteLine($"Результат операции: {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Нельзя делить на ноль!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Найдена ошибка:{ex.Message}");
            }
        }
        static void Main(string[] args)
        {
            Calculator calculator=new Calculator();
            double result = 0;
            
            while(true)
            {
                Console.WriteLine("Хотите ли вы использовать калькулятор (введите y или n)?");
                string answer=Console.ReadLine();

                if (answer.ToLower()=="y")
                {
                    double firstNum = InputFromUser("Введите первое число");
                    string operation = GetOperationFromUser();
                    double secondNum = InputFromUser("Введите второе число");

                    RunCalculation(calculator, firstNum, secondNum, operation);
                }
                else if (answer.ToLower() == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите y или n");
                }
            } 
        }
    }
}
