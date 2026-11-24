// Проєкт: Library (тема 1 — Бібліотека)
// Автор: Бєлєцький Артем

using System;
using System.Text;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Сховище книг: по масиву на кожне поле, усі однакової довжини.
            // Книга номер i — це значення з індексом i в кожному масиві.
            const int capacity = 100;

            int[] ids = new int[capacity];            // інвентарний номер (призначає програма)
            string[] titles = new string[capacity];   // назва
            string[] authors = new string[capacity];  // автор
            int[] years = new int[capacity];          // рік видання
            int[] genreCodes = new int[capacity];     // код жанру (довідник буде пізніше)
            int[] copies = new int[capacity];         // кількість примірників
            bool[] issued = new bool[capacity];       // чи видана зараз

            int count = 0;   // скільки комірок зайнято
            int nextId = 1;  // наступний інвентарний номер
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                    case "1":
                        PrintBooks(ids, titles, authors, years, genreCodes, copies, issued, count);
                        Pause();
                        break;
                    case "2":
                        AddBook(ids, titles, authors, years, genreCodes, copies, issued,
                                ref count, ref nextId, capacity);
                        Pause();
                        break;
                    case "10":
                        FillDemoBooks(ids, titles, authors, years, genreCodes, copies, issued,
                                      ref count, ref nextId, capacity);
                        Pause();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }

            Console.WriteLine("До побачення.");
        }

        // Виводить головне меню
        static void ShowMenu()
        {
            Console.WriteLine("=== Бібліотека ===");
            Console.WriteLine("1 — показати всі книги");
            Console.WriteLine("2 — додати книгу");
            Console.WriteLine("3 — знайти");
            Console.WriteLine("4 — змінити");
            Console.WriteLine("5 — видалити");
            Console.WriteLine("6 — зведена таблиця");
            Console.WriteLine("7 — зберегти у файл");
            Console.WriteLine("8 — завантажити з файлу");
            Console.WriteLine("9 — експорт у CSV");
            Console.WriteLine("10 — заповнити готовими записами");
            Console.WriteLine("0 — вихід");
            Console.Write("Ваш вибір: ");
        }

        // Пауза перед поверненням у меню
        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // Питає ціле число в межах [min; max], доки не отримає прийнятне
        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string text = Console.ReadLine() ?? "";

                if (!int.TryParse(text, out int value))
                {
                    Console.WriteLine("Помилка: потрібно ввести ціле число.");
                    continue;
                }
                if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: число має бути від {min} до {max}.");
                    continue;
                }
                return value;
            }
        }
        // Питає непорожній текст не довший за maxLength, доки не отримає прийнятний
        static string ReadText(string prompt, int maxLength)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string text = (Console.ReadLine() ?? "").Trim();

                if (text.Length == 0)
                {
                    Console.WriteLine("Помилка: значення не може бути порожнім.");
                    continue;
                }
                if (text.Length > maxLength)
                {
                    Console.WriteLine($"Помилка: не більше {maxLength} символів.");
                    continue;
                }
                return text;
            }
        }

        // Питає «так» або «ні», доки не отримає одну з цих відповідей
        static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (так/ні): ");
                string text = (Console.ReadLine() ?? "").Trim().ToLower();

                if (text == "так") return true;
                if (text == "ні") return false;
                Console.WriteLine("Помилка: введіть «так» або «ні».");
            }
        }
        // Додає книгу в кінець сховища. Спершу перевіряє, чи є місце, потім збирає
        // всі поля в локальні змінні й лише після цього записує їх у масиви.
        // Масиви передаються без ref (вони посилальні), лічильники count і nextId — з ref
        static void AddBook(int[] ids, string[] titles, string[] authors, int[] years,
                           int[] genreCodes, int[] copies, bool[] issued,
                           ref int count, ref int nextId, int capacity)
        {
            if (count == capacity)
            {
                Console.WriteLine("Сховище заповнене, видаліть щось зайве.");
                return;
            }

            string title = ReadText("Назва", 30);
            string author = ReadText("Автор", 20);
            int year = ReadInt("Рік видання", 1450, DateTime.Now.Year);
            int genre = ReadInt("Код жанру (1–8)", 1, 8);
            int amount = ReadInt("Кількість примірників", 1, 100);
            bool isIssued = ReadYesNo("Чи видана зараз");

            ids[count] = nextId;
            titles[count] = title;
            authors[count] = author;
            years[count] = year;
            genreCodes[count] = genre;
            copies[count] = amount;
            issued[count] = isIssued;

            count++;
            nextId++;

            Console.WriteLine($"Додано книгу №{ids[count - 1]}.");
        }
        // Виводить усі книги таблицею. Перебирає перші count комірок, а не весь масив
        // усе приходить параметрами
        static void PrintBooks(int[] ids, string[] titles, string[] authors, int[] years,
                              int[] genreCodes, int[] copies, bool[] issued, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Записів поки немає.");
                return;
            }

            string h1 = "№";
            string h2 = "Назва";
            string h3 = "Автор";
            string h4 = "Рік";
            string h5 = "Жанр";
            string h6 = "Прим.";
            string h7 = "Видана";

            Console.WriteLine($"{h1,4} {h2,-32}{h3,-22}{h4,6}{h5,6}{h6,7}{h7,9}");
            Console.WriteLine(new string('-', 87));

            for (int i = 0; i < count; i++)
            {
                string mark = issued[i] ? "так" : "ні";
                Console.WriteLine($"{ids[i],4} {titles[i],-32}{authors[i],-22}{years[i],6}{genreCodes[i],6}{copies[i],7}{mark,9}");
            }

            Console.WriteLine($"\nЗаписів: {count}.");
        }
        // Додає в кінець сховища кілька готових книг для перевірки програми.
        static void FillDemoBooks(int[] ids, string[] titles, string[] authors, int[] years,
                                 int[] genreCodes, int[] copies, bool[] issued,
                                 ref int count, ref int nextId, int capacity)
        {
            string[] demoTitles = { "Тигролови", "Місто", "Лісова пісня", "Тіні забутих предків", "Захар Беркут", "Кобзар" };
            string[] demoAuthors = { "Багряний І.", "Підмогильний В.", "Українка Л.", "Коцюбинський М.", "Франко І.", "Шевченко Т." };
            int[] demoYears = { 1944, 1928, 1911, 1911, 1883, 1840 };
            int[] demoGenres = { 1, 2, 3, 2, 1, 4 };
            int[] demoCopies = { 3, 2, 5, 4, 2, 6 };
            bool[] demoIssued = { true, false, true, false, true, false };

            if (count + demoTitles.Length > capacity)
            {
                Console.WriteLine("Не вистачає місця для готових записів.");
                return;
            }

            for (int i = 0; i < demoTitles.Length; i++)
            {
                ids[count] = nextId;
                titles[count] = demoTitles[i];
                authors[count] = demoAuthors[i];
                years[count] = demoYears[i];
                genreCodes[count] = demoGenres[i];
                copies[count] = demoCopies[i];
                issued[count] = demoIssued[i];

                count++;
                nextId++;
            }

            Console.WriteLine($"Додано готових записів: {demoTitles.Length}. Записів у списку: {count}.");
        }
    }
}