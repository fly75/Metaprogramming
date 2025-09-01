using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryBooks
{
    // Клас Book
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        // Конструктор
        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        // Метод для виведення книг
        public string GetDescription()
        {
            return $"Назва: {Title}, Автор: {Author}, Рік: {Year}";
        }

        // Перевизначення ToString для зручності
        public override string ToString()
        {
            return GetDescription();
        }
    }

    // Клас для управління бібліотекою
    public class Library
    {
        private List<Book> books;

        public Library()
        {
            books = new List<Book>();
        }

        // Додавання книги
        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Книгу додано: {book.GetDescription()}");
        }

        // Отримання всіх книг
        public List<Book> GetAllBooks()
        {
            return new List<Book>(books);
        }

        // Пошук за назвою
        public List<Book> SearchByTitle(string title)
        {
            return books.Where(book =>
                book.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        // Пошук за автором
        public List<Book> SearchByAuthor(string author)
        {
            return books.Where(book =>
                book.Author.ToLower().Contains(author.ToLower())).ToList();
        }

        // Пошук за роком
        public List<Book> SearchByYear(int year)
        {
            return books.Where(book => book.Year == year).ToList();
        }

        // Пошук за діапазоном років
        public List<Book> SearchByYearRange(int fromYear, int toYear)
        {
            return books.Where(book =>
                book.Year >= fromYear && book.Year <= toYear).ToList();
        }

        // Загальна кількість книг
        public int GetBookCount()
        {
            return books.Count;
        }

        // Видалення книги за індексом
        public bool RemoveBook(int index)
        {
            if (index >= 0 && index < books.Count)
            {
                Book removedBook = books[index];
                books.RemoveAt(index);
                Console.WriteLine($"Книгу видалено: {removedBook.GetDescription()}");
                return true;
            }
            return false;
        }
    }

    class Program
    {
        private static Library library = new Library();

        static void Main(string[] args)
        {
            // Налаштування кодування для правильного відображення української мови
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== БІБЛІОТЕКА КНИГ ===");

            // Додаємо тестові книги
            InitializeLibrary();

            // Головний цикл програми
            while (true)
            {
                ShowMenu();

                Console.Write("Оберіть опцію (1-7): ");
                string choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAllBooks();
                        break;
                    case "2":
                        AddNewBook();
                        break;
                    case "3":
                        SearchByTitle();
                        break;
                    case "4":
                        SearchByAuthor();
                        break;
                    case "5":
                        SearchByYear();
                        break;
                    case "6":
                        RemoveBook();
                        break;
                    case "7":
                        Console.WriteLine("До побачення!");
                        return;
                    default:
                        Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                        break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void InitializeLibrary()
        {
            // Додаємо тестові книги
            library.AddBook(new Book("Кобзар", "Тарас Шевченко", 1840));
            library.AddBook(new Book("Тіні забутих предків", "Михайло Коцюбинський", 1911));
            library.AddBook(new Book("Місто", "Валер'ян Підмогильний", 1928));
            library.AddBook(new Book("Земля", "Ольга Кобилянська", 1902));
            library.AddBook(new Book("Захар Беркут", "Іван Франко", 1883));
            library.AddBook(new Book("1984", "Джордж Орвелл", 1949));
            library.AddBook(new Book("Гаррі Поттер і філософський камінь", "Дж. К. Роулінг", 1997));
            library.AddBook(new Book("Майстер і Маргарита", "Михайло Булгаков", 1967));

            Console.WriteLine($"\nІніціалізовано бібліотеку з {library.GetBookCount()} книгами.\n");
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1. Показати всі книги");
            Console.WriteLine("2. Додати нову книгу");
            Console.WriteLine("3. Пошук за назвою");
            Console.WriteLine("4. Пошук за автором");
            Console.WriteLine("5. Пошук за роком");
            Console.WriteLine("6. Видалити книгу");
            Console.WriteLine("7. Вихід");
            Console.WriteLine();
        }

        static void ShowAllBooks()
        {
            var allBooks = library.GetAllBooks();

            if (allBooks.Count == 0)
            {
                Console.WriteLine("Бібліотека порожня.");
                return;
            }

            Console.WriteLine($"Всього книг у бібліотеці: {allBooks.Count}");
            Console.WriteLine();

            for (int i = 0; i < allBooks.Count; i++)
            {
                Console.WriteLine($"{i + 1:D2}. {allBooks[i].GetDescription()}");
            }
        }

        static void AddNewBook()
        {
            try
            {
                Console.Write("Введіть назву книги: ");
                string title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Назва не може бути порожньою!");
                    return;
                }

                Console.Write("Введіть автора: ");
                string author = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Автор не може бути порожнім!");
                    return;
                }

                Console.Write("Введіть рік видання: ");
                if (!int.TryParse(Console.ReadLine(), out int year) || year < 1 || year > DateTime.Now.Year)
                {
                    Console.WriteLine("Некоректний рік видання!");
                    return;
                }

                Book newBook = new Book(title.Trim(), author.Trim(), year);
                library.AddBook(newBook);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при додаванні книги: {ex.Message}");
            }
        }

        static void SearchByTitle()
        {
            Console.Write("Введіть назву (або частину назви) для пошуку: ");
            string searchTitle = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchTitle))
            {
                Console.WriteLine("Рядок пошуку не може бути порожнім!");
                return;
            }

            var results = library.SearchByTitle(searchTitle);
            DisplaySearchResults(results, $"назвою \"{searchTitle}\"");
        }

        static void SearchByAuthor()
        {
            Console.Write("Введіть автора (або частину імені) для пошуку: ");
            string searchAuthor = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchAuthor))
            {
                Console.WriteLine("Рядок пошуку не може бути порожнім!");
                return;
            }

            var results = library.SearchByAuthor(searchAuthor);
            DisplaySearchResults(results, $"автором \"{searchAuthor}\"");
        }

        static void SearchByYear()
        {
            Console.WriteLine("1. Пошук за конкретним роком");
            Console.WriteLine("2. Пошук за діапазоном років");
            Console.Write("Оберіть тип пошуку (1-2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Введіть рік: ");
                if (int.TryParse(Console.ReadLine(), out int year))
                {
                    var results = library.SearchByYear(year);
                    DisplaySearchResults(results, $"роком {year}");
                }
                else
                {
                    Console.WriteLine("Некоректний рік!");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Введіть початковий рік: ");
                if (!int.TryParse(Console.ReadLine(), out int fromYear))
                {
                    Console.WriteLine("Некоректний початковий рік!");
                    return;
                }

                Console.Write("Введіть кінцевий рік: ");
                if (!int.TryParse(Console.ReadLine(), out int toYear))
                {
                    Console.WriteLine("Некоректний кінцевий рік!");
                    return;
                }

                if (fromYear > toYear)
                {
                    Console.WriteLine("Початковий рік не може бути більшим за кінцевий!");
                    return;
                }

                var results = library.SearchByYearRange(fromYear, toYear);
                DisplaySearchResults(results, $"роками {fromYear}-{toYear}");
            }
            else
            {
                Console.WriteLine("Невірний вибір!");
            }
        }

        static void RemoveBook()
        {
            ShowAllBooks();

            if (library.GetBookCount() == 0)
                return;

            Console.WriteLine();
            Console.Write($"Введіть номер книги для видалення (1-{library.GetBookCount()}): ");

            if (int.TryParse(Console.ReadLine(), out int bookNumber) &&
                bookNumber >= 1 && bookNumber <= library.GetBookCount())
            {
                library.RemoveBook(bookNumber - 1);
            }
            else
            {
                Console.WriteLine("Некоректний номер книги!");
            }
        }

        static void DisplaySearchResults(List<Book> results, string searchCriteria)
        {
            if (results.Count == 0)
            {
                Console.WriteLine($"Не знайдено жодної книги за {searchCriteria}.");
                return;
            }

            Console.WriteLine($"Знайдено {results.Count} книг(и) за {searchCriteria}:");
            Console.WriteLine();

            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {results[i].GetDescription()}");
            }
        }
    }
}