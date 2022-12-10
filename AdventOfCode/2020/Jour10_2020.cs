using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Jour10_2020 : IJour
    {
        List<int> adapters = new List<int>();

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            foreach (var line in lines)
            {
                adapters.Add(int.Parse(line));
            }
        }

        public bool FindSolution(long current_jolt, long final_jolt, List<long> solution)
        {
            if (current_jolt == final_jolt)
            {
                return true;
            }

            if (current_jolt > final_jolt)
            {
                return false;
            }

            var possible = adapters.FindAll(p => p <= current_jolt + 3 && p > current_jolt);
            possible.Sort();
            
            if (possible.Count == 0)
            {
                return false;
            }
            foreach(var p in possible)
            {
                if (FindSolution(p, final_jolt, solution))
                {
                    solution.Add(p-current_jolt);
                    return true;
                }
            }
            return false;
        }

        public void ResolveFirstPart()
        {
            var final_jolt = adapters.Max();
            var solution = new List<long>();
            var found = FindSolution(0, final_jolt, solution);
            if (solution.Count != adapters.Count)
            {
                Console.WriteLine("Found a solution, but without all the adapters");
            }

            var adaptater_1 = solution.Count(p => p == 1);
            var adaptater_3 = solution.Count(p => p == 3) + 1;
            Console.WriteLine($"Found={found} adaptater1={adaptater_1} adaptater3={adaptater_3}");
            Console.WriteLine($"Resultat={adaptater_1* adaptater_3}");
        }

        Dictionary<int, long> memo = new Dictionary<int, long>();

        public long FindAllSolutions(int current_jolt, int final_jolt)
        {
            if (current_jolt == final_jolt)
            {
                return 1;
            }

            var possible = adapters.FindAll(p => p <= current_jolt + 3 && p > current_jolt);

            if (possible.Count == 0)
            {
                return 0;
            }
            long result = 0;
            foreach (var p in possible)
            {
                if (memo.ContainsKey(p))
                {
                    result += memo[p];
                }
                else
                {
                    var r = FindAllSolutions(p, final_jolt);
                    result += r;
                    memo.Add(p, r);
                }
            }
            return result;
        }

        public void ResolveSecondPart()
        {
            int final_jolt = adapters.Max();
            var result = FindAllSolutions(0, final_jolt);
            Console.WriteLine($"solutions={result}");
        }
    }
}
