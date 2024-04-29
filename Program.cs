using ATM_machine;

class Program
{
    static void Main()
    {
        int nominal = GetNominal();
        Console.WriteLine();
        var banknotes = GetBanknotes();
        var atm = new Atm(banknotes);
        Console.WriteLine("Всевозможные варианты размена:");
        Atm.PrintResult(atm.Change(nominal));
    }

    static private int GetNominal()
    {
        int result;
        while (true)
        {
            Console.Write("Введите сумму(целое число), которую хотите разменять: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out result))
            {
                if (result < 0)
                {
                    Console.WriteLine("Ошибка: Выдаваемая сумма не может быть отрицательной");
                }
                else
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Введено не целое число\n"); 
            }
        }
        return result;
    }
    static List<int> GetBanknotes()
    {
        var banknotes = new List<int>();
        string[] inputLine;
        var continueCycle = true;

        while (continueCycle)
        {
            Console.WriteLine("Введите доступные для размена купюры (целые числа) через пробел. " +
                "\nЧтобы закончить ввод, введите 0.");
            inputLine = Console.ReadLine().Split([' ']);
            foreach (string input in inputLine) 
            {
                if (input == "0")
                {
                    continueCycle = false;
                    break;
                }

                if (int.TryParse(input, out int number))
                {
                    if (number < 0)
                    {
                        Console.WriteLine("Ошибка: Купюра не может быть отрицательной.\n");
                    }
                    else
                    {
                        banknotes.Add(number);
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: Некорректный ввод. Пожалуйста, введите целое число.\n");
                }
            }
        }

        return banknotes;
    }
}