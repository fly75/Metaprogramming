using System;
using System.Text;
using System.Linq;

namespace TextAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== АНАЛІЗАТОР ТЕКСТУ ===");
            Console.WriteLine("Введіть текст для аналізу (для багаторядкового тексту натисніть Enter двічі)");
            Console.WriteLine("Для виходу введіть 'exit'");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Введіть текст:");

                string inputText = ReadMultilineInput();

                if (inputText?.ToLower().Trim() == "exit" || inputText?.ToLower().Trim() == "вихід")
                {
                    Console.WriteLine("До побачення!");
                    break;
                }

                if (string.IsNullOrEmpty(inputText))
                {
                    Console.WriteLine("Текст порожній! Спробуйте ще раз.");
                    Console.WriteLine();
                    continue;
                }

                TextAnalysisResult result = AnalyzeText(inputText);

                DisplayResults(inputText, result);

                Console.WriteLine();
                Console.WriteLine("Натисніть Enter для нового аналізу або введіть 'exit' для виходу...");

                string continueInput = Console.ReadLine();
                if (continueInput?.ToLower() == "exit" || continueInput?.ToLower() == "вихід")
                {
                    Console.WriteLine("До побачення!");
                    break;
                }

                Console.WriteLine();
            }
        }

        // Метод для читання багаторядкового тексту
        private static string ReadMultilineInput()
        {
            StringBuilder inputBuilder = new StringBuilder();
            string line;
            int emptyLineCount = 0;

            while ((line = Console.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    emptyLineCount++;
                    if (emptyLineCount >= 2)
                    {
                        break;
                    }
                    inputBuilder.AppendLine(line);
                }
                else
                {
                    emptyLineCount = 0;
                    inputBuilder.AppendLine(line);
                }
            }

            return inputBuilder.ToString().TrimEnd('\r', '\n');
        }

        // Метод для аналізу тексту
        private static TextAnalysisResult AnalyzeText(string text)
        {
            var result = new TextAnalysisResult();

            // Підрахунок загальної кількості символів
            result.TotalCharacters = text.Length;

            // Підрахунок видимих символів (без пробілів, табів, переносів рядків)
            result.VisibleCharacters = text.Count(c => !char.IsWhiteSpace(c));

            // Підрахунок пробілів (включаючи всі види пробільних символів)
            result.SpaceCharacters = text.Count(c => char.IsWhiteSpace(c));

            // Підрахунок звичайних пробілів (тільки символ ' ')
            result.RegularSpaces = text.Count(c => c == ' ');

            // Підрахунок рядків
            result.LineCount = text.Split(new[] { '\r', '\n' }, StringSplitOptions.None).Length;
            if (text.EndsWith("\r") || text.EndsWith("\n"))
            {
                result.LineCount--;
            }

            // Підрахунок слів
            result.WordCount = CountWords(text);

            // Додаткова статистика
            result.ParagraphCount = CountParagraphs(text);
            result.SentenceCount = CountSentences(text);

            return result;
        }

        // Підрахунок слів
        private static int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            // Розділяю за пробільними символами та видаляю порожні елементи
            string[] words = text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            // Фільтрую тільки слова, які містять букви або цифри
            return words.Count(word => word.Any(char.IsLetterOrDigit));
        }

        // Підрахунок абзаців
        private static int CountParagraphs(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            // Розділяю за подвійними переносами рядків
            string[] paragraphs = text.Split(new[] { "\r\n\r\n", "\n\n", "\r\r" },
                StringSplitOptions.RemoveEmptyEntries);

            return paragraphs.Length;
        }

        // Підрахунок речень
        private static int CountSentences(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            // Підраховую речення за знаками пунктуації
            char[] sentenceEnders = { '.', '!', '?' };
            return text.Count(c => sentenceEnders.Contains(c));
        }

        // Відображення результатів
        private static void DisplayResults(string originalText, TextAnalysisResult result)
        {
            Console.WriteLine();
            Console.WriteLine("=== РЕЗУЛЬТАТИ АНАЛІЗУ ===");
            Console.WriteLine();

            Console.WriteLine("ОСНОВНА СТАТИСТИКА:");
            Console.WriteLine($"   • Кількість слів: {result.WordCount}");
            Console.WriteLine($"   • Кількість рядків: {result.LineCount}");
            Console.WriteLine($"   • Кількість абзаців: {result.ParagraphCount}");
            Console.WriteLine($"   • Кількість речень: {result.SentenceCount}");
            Console.WriteLine();

            Console.WriteLine("СИМВОЛИ:");
            Console.WriteLine($"   • Усіх символів: {result.TotalCharacters}");
            Console.WriteLine($"   • Видимих символів: {result.VisibleCharacters}");
            Console.WriteLine($"   • Пробільних символів: {result.SpaceCharacters}");
            Console.WriteLine($"   • Звичайних пробілів (' '): {result.RegularSpaces}");
            Console.WriteLine();

            Console.WriteLine("ДОДАТКОВА ІНФОРМАЦІЯ:");
            Console.WriteLine($"   • Середня довжина слова: {(result.WordCount > 0 ? (double)result.VisibleCharacters / result.WordCount : 0):F1} символів");
            Console.WriteLine($"   • Слів на рядок: {(result.LineCount > 0 ? (double)result.WordCount / result.LineCount : 0):F1}");
            Console.WriteLine($"   • Символів на рядок: {(result.LineCount > 0 ? (double)result.TotalCharacters / result.LineCount : 0):F1}");

            if (result.WordCount > 0)
            {
                double readingTime = result.WordCount / 200.0; // Середня швидкість читання 200 слів/хв
                Console.WriteLine($"   • Приблизний час читання: {readingTime:F1} хвилин");
            }

            Console.WriteLine();
            Console.WriteLine("ВВЕДЕНИЙ ТЕКСТ:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(originalText);
            Console.WriteLine(new string('-', 50));
        }
    }

    // Клас для зберігання результатів аналізу
    public class TextAnalysisResult
    {
        public int WordCount { get; set; }
        public int SpaceCharacters { get; set; }
        public int RegularSpaces { get; set; }
        public int TotalCharacters { get; set; }
        public int VisibleCharacters { get; set; }
        public int LineCount { get; set; }
        public int ParagraphCount { get; set; }
        public int SentenceCount { get; set; }
    }
}