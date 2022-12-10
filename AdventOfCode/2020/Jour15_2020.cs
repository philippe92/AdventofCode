using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Jour15_2020 : IJour
    {
        List<int> serie;

        public void Init(string inputfile)
        {
        }

        private bool ReverseFind(int to_search, out int last)
        {
            last = 0;
            bool result = false;
            int i = serie.Count - 2;
            while (i>=0)
            {
                if (serie[i] == to_search)
                {
                    last = i;
                    result = true;
                    break;
                }
                i--;
            }
            return result;
        }

        public void ResolveFirstPart()
        {
            serie = new List<int> { 0,3,6 };
            int i = serie.Count;
            Dictionary<int, int> last_spoken = new Dictionary<int, int>();
            while ( i < 2020)
            {
                int current = 0;
                if (ReverseFind(serie[i-1], out var last))
                {
                    current = i - (last+1);
                }
                else
                {
                    current = 0;
                }

                serie.Add(current);
                i++;
            }
            Console.WriteLine(serie[i-1]);
        }

        public void ResolveSecondPart()
        {
            Dictionary<long, long> last_spoken = new Dictionary<long, long>();
            last_spoken[2] = 1;
            last_spoken[0] = 2;
            last_spoken[1] = 3;
            last_spoken[7] = 4;
            last_spoken[4] = 5;
            long i = 8;
            long previous_turn = 18;
            long previous_previous_turn = 14;
            while (i <= 30000000)
            {
                last_spoken[previous_previous_turn] = i - 2;
                long turn;
                if (last_spoken.ContainsKey(previous_turn))
                {
                    turn = i - 1 - last_spoken[previous_turn];
                }
                else
                {
                    turn = 0;
                }

                previous_previous_turn = previous_turn;
                previous_turn = turn;
                i++;
            }
            Console.WriteLine($"Resultat = {previous_turn}");
        }
    }
}
