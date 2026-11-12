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
			bool running = true;

			while (running)
			{
				Console.WriteLine("=== Бібліотека ===");
				Console.WriteLine("1 — довідка про застосунок");
				Console.WriteLine("2 — табулювання: штраф за прострочення");
				Console.WriteLine("3 — таблиця: видачі за жанрами й днями тижня");
				Console.WriteLine("0 — вихід");
				Console.Write("Ваш вибір: ");
				string choice = Console.ReadLine();
				Console.WriteLine();

				switch (choice)
				{
					case "1":
						ShowAbout();
						Pause();
						break;
					case "2":
						ShowFineTable();
						Pause();
						break;
					case "3":
						ShowGenreTable();
						Pause();
						break;
					case "0":
						running = false;
						Console.WriteLine("До побачення.");
						break;
					default:
						Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
						Pause();
						break;
				}
			}
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

		// Табулювання: штраф залежно від кількості днів прострочення
		// y1 = 2,5 * x                   — звичайний штраф, грн
		// y2 = 2,5 * x * (1 + 0,05 * x)  — штраф із надбавкою за тривале прострочення
		static void ShowFineTable()
		{
			double start = 0.5;
			double end = 5.0;
			double step = 0.5;

			string h1 = "Днів";
			string h2 = "Штраф";
			string h3 = "З надбавкою";

			Console.WriteLine("Штраф за прострочення повернення книги");
			Console.WriteLine($"{h1,8}{h2,12}{h3,16}");
			Console.WriteLine(new string('-', 36));

			// 1e-9 — запас, бо дробові числа зберігаються наближено
			for (double x = start; x <= end + 1e-9; x += step)
			{
				double y1 = 2.5 * x;
				double y2 = 2.5 * x * (1 + 0.05 * x);
				Console.WriteLine($"{x,8:F1}{y1,12:F2}{y2,16:F2}");
			}
		}

		// Таблиця з двома входами: рядки — жанри, стовпці — дні тижня,
		// останній стовпець — підсумок рядка
		static void ShowGenreTable()
		{
			string[] genres = { "пригоди", "роман", "драма", "детектив",
								"фантастика", "поезія", "історія", "казки" };
			string[] days = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Нд" };

			string head = "Жанр";
			string sumHead = "Разом";

			Console.WriteLine("Видачі книг за жанрами й днями тижня");

			// Шапка таблиці
			Console.Write($"{head,-14}");
			for (int c = 0; c < days.Length; c++)
			{
				Console.Write($"{days[c],6}");
			}
			Console.WriteLine($"{sumHead,8}");
			Console.WriteLine(new string('-', 14 + days.Length * 6 + 8));

			// Рядки таблиці
			for (int r = 0; r < genres.Length; r++)
			{
				int rowSum = 0; // обнуляється для кожного нового рядка
				Console.Write($"{genres[r],-14}");
				for (int c = 0; c < days.Length; c++)
				{
					int value = (r + c + 2 * r * c) % 8 + 1; // навчальні дані
					Console.Write($"{value,6}");
					rowSum += value;
				}
				Console.WriteLine($"{rowSum,8}"); // перехід на новий рядок
			}
		}
	}
}