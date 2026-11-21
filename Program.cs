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
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "10":
                        Console.WriteLine("Поки не зроблено.");
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
    }
}