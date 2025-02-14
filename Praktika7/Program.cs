using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika7
{
    internal class Program
    {
        public static Dictionary<string, List<double>> finances = new Dictionary<string, List<double>>();
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в систему управления финансами!");

            while (true)
            {
                Console.WriteLine("\n1. Добавить доход/расход");
                Console.WriteLine("2. Показать отчет");
                Console.WriteLine("3. Рассчитать баланс");
                Console.WriteLine("4. Прогноз на следующий месяц");
                Console.WriteLine("5. Статистика");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите действие: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTransaction();
                        break;
                    case "2":
                        Console.WriteLine(PrintFinanceReport());
                        break;
                    case "3":
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        public static void AddTransaction()
        {
            Console.Write("Введите категорию (Доход, Продукты, Транспорт, Развлечения и т.д.): ");
            var category = Console.ReadLine();
            Console.Write("Введите сумму: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                if (!finances.ContainsKey(category))
                {
                    finances[category] = new List<double>();
                }
                finances[category].Add(amount);
                Console.WriteLine("Запись добавлена.");
            }
            else
            {
                Console.WriteLine("Некорректная сумма. Пожалуйста, попробуйте снова.");
            }
        }

        public static string PrintFinanceReport()
        {
            var report = "Финансовый отчет:\n";
            foreach (var category in finances)
            {
                report += $"{category.Key}: {category.Value.Sum()} руб. - {category.Value.Count} операций\n";
            }
            return report;
        }
        
        
    }
}

