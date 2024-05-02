
using System.Text;

namespace ATM_machine
{
    class Atm(List<int> banknotesPile)
    {
        private record StackData(List<int> banknotes, List<int> partial);
        private readonly List<int> banknotes = [.. banknotesPile.Distinct().OrderBy(x => x)];
        private readonly List<List<int>> result = [];

        public List<List<int>> Change(int nominal) 
        {
            SolveIterative(nominal); 
            return result;
        }

        private void SolveRecursive(in List<int> banknotes, in int nominal, List<int> partial)
        {
            int s = 0;
            foreach (int x in partial) { s += x; }
            if (s == nominal) { result.Add(partial); }
            if (s >= nominal) { return; }

            for (int i = 0; i < banknotes.Count; i++) 
            {
                var remaining = new List<int>();
                int n = banknotes[i];
                for (int j = i; j < banknotes.Count; j++) 
                { 
                    remaining.Add(banknotes[j]); 
                }
                var partialRec = new List<int>(partial) { n };

                SolveRecursive(remaining, nominal, partialRec);
            }
        }

        private void SolveIterative(int nominal)
        {
            var stack = new Stack<(List<int> remaining, List<int> partial)>();

            stack.Push((banknotes, new List<int>()));

            while (stack.Count > 0)
            {
                var (remaining, partial) = stack.Pop();

                int s = 0;
                foreach (int x in partial) { s += x; } 
                if (s == nominal) { result.Add(new List<int>(partial)); }
                if (s >= nominal) { continue; }

                for (int i = 0; i < remaining.Count; i++)
                {
                    int n = remaining[i];
                    var newPartial = new List<int>(partial) { n };
                    var newRemaining = new List<int>();
                    for (int j = i; j < remaining.Count; j++) 
                    {
                        newRemaining.Add(remaining[j]);
                    }

                    stack.Push((newRemaining, newPartial));
                }
            }
        }

        public static void PrintResult(in List<List<int>>result)
        {
            var bld = new StringBuilder();

            foreach (List<int> exchange in result) 
            {
                foreach (int banknote in exchange)
                {
                    bld.Append(banknote + " ");
                }
                bld.Append('\n');
            }
            Console.WriteLine(bld.ToString());
        }
    }
}
