
using System.Text;

namespace ATM_machine
{
    class Atm(List<int> banknotesPile)
    {
        private readonly List<int> banknotes = [.. banknotesPile.OrderBy(x => x)];
        private readonly List<List<int>> result = [];

        public List<List<int>> Change(int nominal) 
        { 
            SolveRecursive(banknotes, nominal, []); 
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
