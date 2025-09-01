using System;
using System.Text;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Калькулятор ===");
            Console.WriteLine("Підтримувані операції: +, -, *, /");
            Console.WriteLine();

            while (true)
            {
                try
                {
                    // Введення першого числа
                    Console.Write("Введіть перше число: ");
                    string input1 = Console.ReadLine();

                    if (input1?.ToLower() == "exit" || input1?.ToLower() == "вихід")
                    {
                        Console.WriteLine("До побачення!");
                        break;
                    }

                    double num1 = Convert.ToDouble(input1);

                    // Введення операції
                    Console.Write("Введіть операцію (+, -, *, /): ");
                    string operation = Console.ReadLine();

                    // Введення другого числа
                    Console.Write("Введіть друге число: ");
                    double num2 = Convert.ToDouble(Console.ReadLine());

                    double result = 0;
                    bool validOperation = true;

                    // Виконання операції
                    switch (operation)
                    {
                        case "+":
                            result = num1 + num2;
                            break;
                        case "-":
                            result = num1 - num2;
                            break;
                        case "*":
                            result = num1 * num2;
                            break;
                        case "/":
                            if (num2 == 0)
                            {
                                Console.WriteLine("Помилка: Ділення на нуль неможливе!");
                                validOperation = false;
                            }
                            else
                            {
                                result = num1 / num2;
                            }
                            break;
                        default:
                            Console.WriteLine("Помилка: Невідома операція! Використовуйте +, -, *, /");
                            validOperation = false;
                            break;
                    }

                    // Виведення результату
                    if (validOperation)
                    {
                        Console.WriteLine($"Результат: {num1} {operation} {num2} = {result}");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка: Введено некоректне число!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("Натисніть Enter для продовження або введіть 'exit' для виходу...");

                string continueInput = Console.ReadLine();
                if (continueInput?.ToLower() == "exit" || continueInput?.ToLower() == "вихід")
                {
                    Console.WriteLine("До побачення!");
                    break;
                }

                Console.WriteLine();
            }
        }
    }
}