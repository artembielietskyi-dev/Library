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

		// Заглушка: буде реалізовано на наступному етапі
		static void ShowFineTable()
		{
			Console.WriteLine("Цей пункт ще не готовий.");
		}

		// Заглушка: буде реалізовано на наступному етапі
		static void ShowGenreTable()
		{
			Console.WriteLine("Цей пункт ще не готовий.");
		}
	}
}