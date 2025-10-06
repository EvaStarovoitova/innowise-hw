using System;


namespace ex1
{
    public interface ICalc
    {
        double Calc(double num1, double num2);
    }

    public class Sum : ICalc
    {
        public double Calc(double num1, double num2)
        {
            return num1 + num2;
        }
    }

    public class Dev : ICalc
    {
        public double Calc(double num1, double num2)
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

    public class Mul : ICalc
    {
        public double Calc(double num1, double num2)
        {
            return num1 * num2;
        }
    }

    public class Sub : ICalc
    {
        public double Calc(double num1, double num2)
        {
            return num1 - num2;
        }
    }

    class Calculator
    {

        public ICalc Calculate { get; set; }

        public double Result(double num1, double num2)
        {
            return Calculate.Calc(num1, num2);
        }
    }

    
    
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator=new Calculator();
            double result = 0;
            double num1, num2;

            while(true)
            {
                Console.WriteLine("Хотите ли вы использовать калькулятор (введите y или n)?");
                string answer=Console.ReadLine();

                if (answer.ToLower()=="y")
                {
                    while (true)
                    {
                        Console.WriteLine("Введите первое число:");

                        if (double.TryParse(Console.ReadLine(), out num1))
                        {
                           
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Введите число!");
                        }
                    }
                   

                    Console.WriteLine("Введите одну из операций  +, -, *, /:");
                    string operation = Console.ReadLine();

                    while (true)
                    {
                        Console.WriteLine("Введите второе число:");

                        if (double.TryParse(Console.ReadLine(), out num2))
                        {
                           
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Введите число!");
                        }
                    }
                    

                    try
                    {
                        switch (operation)
                        {
                            case "+":
                                calculator.Calculate = new Sum();
                                result = calculator.Result(num1, num2);
                                Console.WriteLine($"Результат сложения {result}");
                                break;
                            case "-":
                                calculator.Calculate = new Sub();
                                result = calculator.Result(num1, num2);
                                Console.WriteLine($"Результат вычитания {result}");
                                break;
                            case "*":
                                calculator.Calculate = new Mul();
                                result = calculator.Result(num1, num2);
                                Console.WriteLine($"Результат умножения {result}");
                                break;
                            case "/":
                                calculator.Calculate = new Dev();
                                result = calculator.Result(num1, num2);
                                Console.WriteLine($"Результат деления {result}");
                                break;
                            default:
                                Console.WriteLine("Нет такой операции! Введите одну из предложенных операций!");
                                break;
                        }
                    }
                    catch(DivideByZeroException)
                    {
                        Console.WriteLine("Нельзя делить на ноль!");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Найдена ошибка:{ex.Message}");
                    }
                     
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