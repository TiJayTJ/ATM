using ATM_machine;
using System.Net.Http.Headers;
using System.Numerics;

class Program
{
    static void Main()
    {
        while(true)
        {
            Console.WriteLine("======================================================================");
            int nominal = GetNominal();
            Console.WriteLine();
            var banknotes = GetBanknotes();
            var atm = new Atm(banknotes);
            Console.WriteLine("Ведётся подсчёт...");
            List<List<int>> result = atm.Change(nominal);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
            if (result.Count > 0)
            {
                Console.WriteLine("Всевозможные комбинации для размена:");
                Atm.PrintResult(result);
            }
            else
            {
                Console.WriteLine("Не удалось найти комбинации для размена");
            }
        }        
    }
    public static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }


    static private int GetNominal()
    {
        while (true)
        {
            Console.Write("Введите номинал банкноты, которую хотите разменять: ");
            string? inputString = Console.ReadLine().Replace('.', ',');

            if (string.IsNullOrWhiteSpace(inputString))
            {
                Console.WriteLine("Ошибка: Пустая строка");
                continue;
            }


            if (double.TryParse(inputString, out double result)) { 
                if (result % 1 == 0)
                {
                    if (result > int.MaxValue)
                    {
                        Console.WriteLine("Ошибка: Слишком большое число (>= 2 147 483 648)");
                    }
                    else if (result < 0)
                    {
                        Console.WriteLine("Ошибка: Номинал не может быть отрицательным");
                    }
                    else if (result == 0)
                    {
                        Console.WriteLine("Ошибка: Номинал не может быть нулевым");
                    }
                    else
                    {
                        return (int)result;
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: Номинал должен быть целым числом");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Некорректный ввод");
            }
        }
    }
    static List<int> GetBanknotes()
    {
        var banknotes = new List<int> {};
        List<string> logs;
        string?[] inputLine;
        var continueCycle = true;

        while (continueCycle)
        {
            banknotes = [];
            logs = [];

            Console.WriteLine("Введите через пробел купюры, которые будут доступны для размена. " +
                "Строка должна заканчиваться числом 0, означающем конец входных данных");
            string? inputString = Console.ReadLine().Replace('.', ',');

            if (string.IsNullOrWhiteSpace(inputString))
            {
                Console.WriteLine("Ошибка: Пустая строка");
                continue;
            }
            inputLine = inputString.Split(' ', '\t').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            if (inputLine[^1] != "0")
            {
                logs.Add("Ошибка: строка должна заканчиваться числом 0");
            }

            foreach (string input in inputLine) 
            {
                if (double.TryParse(input, out double nominal))
                {
                    if ((nominal % 1) == 0)
                    {
                        if (nominal > int.MaxValue)
                        {
                            /*logs.Add(String.Format("Ошибка: Слишком большое число (>= %d)", int.MaxValue + 1));*/
                            logs.Add("Ошибка: Слишком большое число (>= 2 147 483 648)");
                        }
                        else if (nominal < 0)
                        {
                            logs.Add("Ошибка: Номинал не может быть отрицательным");
                        }
                        else
                        {
                            banknotes.Add((int)nominal);
                        }
                    }
                    else
                    {
                        logs.Add("Ошибка: Номинал должен быть целым числом"); 
                    }
                }
                else
                {
                    logs.Add("Ошибка: Некорректный ввод");
                }
            }
            if (banknotes.Count > 0 && banknotes.Count(x => x == 0) > 1){
                logs.Add("Ошибка: Обнаружен 0 в середине строки");
            }

            if(logs.Count > 0)
            {
                foreach (string input in logs.Distinct())
                {
                    Console.WriteLine(input);
                }
            }
            else
            {
                continueCycle = false;
            }
        }
        banknotes.RemoveAt(banknotes.Count - 1);
        return banknotes;
    }
}