// Проєкт: Library (тема 1 — Бібліотека)
// Автор: Бєлєцький Артем

using System;
using System.Globalization;
using System.Text;

namespace Library
{
    internal class Program
    {
        // Ставка штрафу за один день і частка надбавки за тривале прострочення
        const double DayRate = 2.5;
        const double Surcharge = 0.05;

        // Найбільші розміри таблиці «жанри × дні тижня»
        const int MaxGenres = 8;
        const int MaxDays = 7;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Список штрафів, нарахованих читачам: масив і лічильник живуть тільки тут
            const int capacity = 100;
            double[] fines = new double[capacity];
            int count = 0;
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAbout(); Pause(); break;
                    case "2": ShowFineTable(); Pause(); break;
                    case "3": ShowGenreTable(); Pause(); break;
                    case "4":
                        {
                            double newFine = ReadDouble("Сума штрафу, грн (від 0 до 10000)", 0, 10000);
                            AddValue(fines, ref count, newFine, capacity);
                            Console.WriteLine($"Записів у списку: {count}");
                            Pause();
                            break;
                        }
                    case "5": PrintValues(fines, count); Pause(); break;
                    case "0": running = false; break;
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
            Console.WriteLine("1 — довідка про застосунок");
            Console.WriteLine("2 — табулювання: штраф за прострочення");
            Console.WriteLine("3 — таблиця: видачі за жанрами й днями тижня");
            Console.WriteLine("4 — додати штраф у список");
            Console.WriteLine("5 — показати список штрафів");
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

        // Довідка: тема, сутність, поля з типами
        static void ShowAbout()
        {
            Console.WriteLine("Тема: Бібліотека");
            Console.WriteLine("Головна сутність: Книга. Полів: 7.");
            Console.WriteLine();
            Console.WriteLine("Поля сутності «Книга»:");
            Console.WriteLine("  int    — інвентарний номер");
            Console.WriteLine("  string — назва");
            Console.WriteLine("  string — автор");
            Console.WriteLine("  int    — рік видання");
            Console.WriteLine("  int    — код жанру (з довідника)");
            Console.WriteLine("  int    — кількість примірників");
            Console.WriteLine("  bool   — чи видана зараз");
            Console.WriteLine();
            Console.WriteLine("Довідник: Жанр (код, назва, чи активний).");
            Console.WriteLine("Сутність: Користувач (з'явиться в роботі 14).");
            Console.WriteLine("Підсумкова величина: кількість книг кожного жанру");
            Console.WriteLine("та книги, видані понад 14 днів.");
        }

        // Рахує штраф за прострочення і повертає його (нічого не виводить).
        // days — днів прострочення, ratePerDay — ставка за день, грн,
        // surcharge — надбавка: частка від суми за кожен день прострочення.
        // Формула: ratePerDay * days * (1 + surcharge * days)
        static double CalculateFine(double days, double ratePerDay, double surcharge)
        {
            return ratePerDay * days * (1 + surcharge * days);
        }

        // Пункт меню «табулювання»: задає межі й крок, потім друкує таблицю
        static void ShowFineTable()
        {
            double start = ReadDouble("Початок, днів (від 0,5 до 30)", 0.5, 30);
            double end = ReadDouble($"Кінець, днів (від {start} до 30)", start, 30);
            double step = ReadDouble("Крок, днів (від 0,1 до 5)", 0.1, 5);

            Console.WriteLine();
            PrintFineTable(start, end, step);
        }

        // Табулює штраф: x іде від start до end з кроком step
        static void PrintFineTable(double start, double end, double step)
        {
            string h1 = "Днів";
            string h2 = "Штраф";
            string h3 = "З надбавкою";

            Console.WriteLine("Штраф за прострочення повернення книги");
            Console.WriteLine($"{h1,8}{h2,12}{h3,16}");
            Console.WriteLine(new string('-', 36));

            // 1e-9 — запас, бо дробові числа зберігаються наближено
            for (double x = start; x <= end + 1e-9; x += step)
            {
                double fine = CalculateFine(x, DayRate, 0);
                double fineExtra = CalculateFine(x, DayRate, Surcharge);
                Console.WriteLine($"{x,8:F1}{fine,12:F2}{fineExtra,16:F2}");
            }
        }

        // Пункт меню «таблиця»: задає розмір і викликає друк
        static void ShowGenreTable()
        {
            int rows = ReadInt($"Скільки жанрів (рядків) показати (1–{MaxGenres})", 1, MaxGenres);
            int cols = ReadInt($"Скільки днів (стовпців) показати (1–{MaxDays})", 1, MaxDays);

            Console.WriteLine();
            PrintGenreTable(rows, cols);
        }

        // Друкує таблицю «жанри × дні тижня» з підсумками по рядках і стовпцях
        static void PrintGenreTable(int rows, int cols)
        {
            string[] genres = { "пригоди", "роман", "драма", "детектив",
                                "фантастика", "поезія", "історія", "казки" };
            string[] days = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Нд" };

            string head = "Жанр";
            string sumHead = "Разом";

            Console.WriteLine("Видачі книг за жанрами й днями тижня");

            // Шапка таблиці
            Console.Write($"{head,-14}");
            for (int c = 0; c < cols; c++)
            {
                Console.Write($"{days[c],6}");
            }
            Console.WriteLine($"{sumHead,8}");
            PrintSeparator(cols);

            int[] colSum = new int[cols]; // підсумки по стовпцях
            int total = 0;                // загальний підсумок

            // Рядки таблиці
            for (int r = 0; r < rows; r++)
            {
                int rowSum = 0; // обнуляється для кожного нового рядка
                Console.Write($"{genres[r],-14}");
                for (int c = 0; c < cols; c++)
                {
                    int value = GetIssueCount(r, c);
                    Console.Write($"{value,6}");
                    rowSum += value;
                    colSum[c] += value;
                }
                Console.WriteLine($"{rowSum,8}");
                total += rowSum;
            }

            // Рядок підсумків під таблицею
            PrintSeparator(cols);
            Console.Write($"{sumHead,-14}");
            for (int c = 0; c < cols; c++)
            {
                Console.Write($"{colSum[c],6}");
            }
            Console.WriteLine($"{total,8}");
        }

        // Навчальні дані: скільки книг жанру genre видано в день day
        static int GetIssueCount(int genre, int day)
        {
            return (genre + day + 2 * genre * day) % 8 + 1;
        }

        // Друкує горизонтальну лінію під ширину таблиці з cols стовпців
        static void PrintSeparator(int cols)
        {
            Console.WriteLine(new string('-', 14 + cols * 6 + 8));
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

        // Питає дробове число в межах [min; max], доки не отримає прийнятне.
        // Приймає і крапку, і кому як десятковий роздільник.
        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string text = (Console.ReadLine() ?? "").Replace(',', '.');

                if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                    || double.IsNaN(value))
                {
                    Console.WriteLine("Помилка: потрібно ввести число.");
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
        // Додає значення в кінець масиву.
        // Масив іде без ref (він і так посилальний), лічильник — з ref,
        // бо метод має змінити саме його. Нічого не читає з клавіатури.
        // values — масив, count — скільки елементів заповнено,
        // newValue — що додати, capacity — місткість масиву.
        static void AddValue(double[] values, ref int count, double newValue, int capacity)
        {
            if (count == capacity)
            {
                Console.WriteLine("Місця більше немає.");
                return;
            }

            values[count] = newValue;
            count++;
        }
        // Виводить перші count елементів масиву таблицею з номерами.
        // Нічого не читає з клавіатури: масив і лічильник приходять параметрами.
        static void PrintValues(double[] values, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            string h1 = "№";
            string h2 = "Штраф, грн";

            Console.WriteLine("Штрафи, нараховані читачам");
            Console.WriteLine($"{h1,4}{h2,14}");
            Console.WriteLine(new string('-', 18));

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1,4}{values[i],14:F2}");
            }
        }
    }
}