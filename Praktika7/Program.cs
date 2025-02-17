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
                        Console.WriteLine(CalculateBalance());
                        break;
                    case "4":
                        Console.WriteLine(PredictNextMonthExpenses());
                        break;
                    case "5":
                        Console.WriteLine(PrintStatistics());
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
        public static string CalculateBalance()
        {
            double income = finances.ContainsKey("Доход") ? finances["Доход"].Sum() : 0;
            double expenses = finances.Where(f => f.Key != "Доход").Sum(f => f.Value.Sum());
            double balance = income - expenses;
            return $"Текущий баланс: {balance} руб";
        }
        public static string PredictNextMonthExpenses()
        {
            double totalExpenses = 0;
            int totalCategories = 0;

            foreach (var category in finances.Where(f => f.Key != "Доход"))
            {
                totalExpenses += GetAverageExpense(category.Key);
                totalCategories++;
            }

            double prediction = totalCategories > 0 ? totalExpenses / totalCategories : 0;
            return $"Прогнозируемые расходы на следующий месяц: {prediction} руб.";
        }

        public static double GetAverageExpense(string category)
        {
            if (finances.ContainsKey(category) && finances[category].Count > 0)
            {
                return finances[category].Average();
            }
            return 0;
        }

        public static string PrintStatistics()
        {
            double totalExpenses = finances.Where(f => f.Key != "Доход").Sum(f => f.Value.Sum());
            var stats = $"Общая сумма расходов: {totalExpenses} руб.\n";

            var mostExpensesCategory = finances.Where(f => f.Key != "Доход")
                                                .OrderByDescending(f => f.Value.Sum())
                                                .FirstOrDefault();
            if (mostExpensesCategory.Key != null)
            {
                stats += $"Самая затратная категория: {mostExpensesCategory.Key} ({mostExpensesCategory.Value.Sum()} руб.)\n";
            }

            var mostFrequentCategory = finances.Where(f => f.Key != "Доход")
                                                .OrderByDescending(f => f.Value.Count)
                                                .FirstOrDefault();
            if (mostFrequentCategory.Key != null)
            {
                stats += $"Самая частая категория: {mostFrequentCategory.Key} ({mostFrequentCategory.Value.Count} операций)\n";
            }

            stats += "Процентное распределение расходов:\n";
            double total = finances.Where(f => f.Key != "Доход").Sum(f => f.Value.Sum());
            foreach (var category in finances.Where(f => f.Key != "Доход"))
            {
                double categoryTotal = category.Value.Sum();
                double percentage = total > 0 ? (categoryTotal / total) * 100 : 0;
                stats += $"{category.Key}: {categoryTotal} руб. ({percentage:F2}%)\n";
            }

            return stats;


        }
    }
}

