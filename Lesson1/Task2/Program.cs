using System;
using System.Text;
using System.Globalization;

namespace CurrencyConverter
{
    class Program
    {
        // Константи курсів валют (до UAH)
        private const double USD_TO_UAH = 37.50;
        private const double EUR_TO_UAH = 40.80;
        private const double UAH_TO_UAH = 1.00;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== КОНВЕРТОР ВАЛЮТ ===");
            Console.WriteLine("Підтримувані валюти: USD, EUR, UAH");
            Console.WriteLine($"Поточні курси (до UAH):");
            Console.WriteLine($"1 USD = {USD_TO_UAH} UAH");
            Console.WriteLine($"1 EUR = {EUR_TO_UAH} UAH");
            Console.WriteLine();

            while (true)
            {
                try
                {
                    Console.Write("Введіть суму для конвертації: ");
                    string amountInput = Console.ReadLine();

                    if (amountInput?.ToLower() == "exit" || amountInput?.ToLower() == "вихід")
                    {
                        Console.WriteLine("До побачення!");
                        break;
                    }

                    double amount = Convert.ToDouble(amountInput);

                    if (amount < 0)
                    {
                        Console.WriteLine("Помилка: Сума не може бути від'ємною!");
                        Console.WriteLine();
                        continue;
                    }

                    // Введення початкової валюти
                    Console.Write("З якої валюти конвертувати (USD/EUR/UAH): ");
                    string fromCurrency = Console.ReadLine()?.ToUpper().Trim();

                    if (!IsValidCurrency(fromCurrency))
                    {
                        Console.WriteLine("Помилка: Невідома валюта! Використовуйте USD, EUR або UAH");
                        Console.WriteLine();
                        continue;
                    }

                    // Введення цільової валюти
                    Console.Write("У яку валюту конвертувати (USD/EUR/UAH): ");
                    string toCurrency = Console.ReadLine()?.ToUpper().Trim();

                    if (!IsValidCurrency(toCurrency))
                    {
                        Console.WriteLine("Помилка: Невідома валюта! Використовуйте USD, EUR або UAH");
                        Console.WriteLine();
                        continue;
                    }

                    // Виконання конвертації
                    double result = ConvertCurrency(amount, fromCurrency, toCurrency);

                    // Виведення результату
                    Console.WriteLine();
                    Console.WriteLine($"Результат конвертації:");
                    Console.WriteLine($"{amount:F2} {fromCurrency} = {result:F2} {toCurrency}");

                    // Показ використаного курсу
                    if (fromCurrency != toCurrency)
                    {
                        double rate = GetExchangeRate(fromCurrency, toCurrency);
                        Console.WriteLine($"Курс: 1 {fromCurrency} = {rate:F4} {toCurrency}");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка: Введено некоректну суму!");
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

        // Метод для перевірки валідності валюти
        private static bool IsValidCurrency(string currency)
        {
            return currency == "USD" || currency == "EUR" || currency == "UAH";
        }

        // Метод для конвертації валют
        private static double ConvertCurrency(double amount, string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency)
            {
                return amount;
            }

            // Спочатку конвертуємо все в UAH, потім в цільову валюту
            double amountInUAH = ConvertToUAH(amount, fromCurrency);
            double result = ConvertFromUAH(amountInUAH, toCurrency);

            return result;
        }

        // Конвертація в гривні
        private static double ConvertToUAH(double amount, string currency)
        {
            return currency switch
            {
                "USD" => amount * USD_TO_UAH,
                "EUR" => amount * EUR_TO_UAH,
                "UAH" => amount,
                _ => throw new ArgumentException("Невідома валюта")
            };
        }

        // Конвертація з гривень
        private static double ConvertFromUAH(double amount, string currency)
        {
            return currency switch
            {
                "USD" => amount / USD_TO_UAH,
                "EUR" => amount / EUR_TO_UAH,
                "UAH" => amount,
                _ => throw new ArgumentException("Невідома валюта")
            };
        }

        // Отримання курсу обміну між двома валютами
        private static double GetExchangeRate(string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency)
                return 1.0;

            double oneUnitInUAH = ConvertToUAH(1, fromCurrency);
            return ConvertFromUAH(oneUnitInUAH, toCurrency);
        }
    }
}