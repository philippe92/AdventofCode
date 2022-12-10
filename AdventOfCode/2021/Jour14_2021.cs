using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Jour14_2021 : IJour
    {
        string polymer_base;

        Dictionary<string, char> rules = new Dictionary<string, char>();

        Dictionary<char, long> freqs = new Dictionary<char, long>();

        Dictionary<string, Dictionary<char, long>> memoization = new Dictionary<string, Dictionary<char, long>>();

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            polymer_base = lines[0];
            for (int i = 2; i < lines.Length; i++)
            {
                var line = lines[i];
                rules[line.Substring(0, 2)] = line[6];
            }
        }

        public void ResolveFirstPart()
        {
            string polymer = polymer_base;
            for (int i = 0; i < 10; i++)
            {
                StringBuilder work = new StringBuilder(polymer);
                for (int j = 0; j < polymer.Length - 1; j++)
                {
                    var toinsert = rules[polymer.Substring(j, 2)];
                    work.Insert((j * 2) + 1, toinsert);
                }
                polymer = work.ToString();
            }

            HashSet<char> items = new HashSet<char>();
            foreach (var c in polymer)
            {
                items.Add(c);
            }

            List<long> freqs = new List<long>();
            foreach (var c in items)
            {
                int f = polymer.Count(a => a == c);
                freqs.Add(f);
            }
            freqs.Sort();
            var result = freqs.Last() - freqs.First();
            Console.WriteLine($"Result : {result}");

        }

        private char[] ProcessPolymer(string polymer, int iterations)
        {
            long len = polymer.Length;
            var old = new char[len];
            for (int i = 0; i < polymer.Length; i++)
            {
                old[i] = polymer[i];
            }
            for (int i = 0; i < iterations; i++)
            {
                char[] work = new char[len + len - 1];
                work[0] = old[0];
                for (int j = 0; j < len - 1; j++)
                {
                    var refstr = string.Concat(old[j], old[j + 1]);
                    var toinsert = rules[refstr];
                    work[j * 2 + 1] = toinsert;
                    work[j * 2 + 2] = old[j + 1];
                }
                len += len - 1;
                old = work;
            }
            return old;
        }

        private Dictionary<char,long> CalculateFrequencies(char[] polymer, bool remove_first)
        {
            var ret = new Dictionary<char, long>();
            HashSet<char> items = new HashSet<char>();
            foreach (var c in polymer)
            {
                items.Add(c);
            }

            foreach (var c in items)
            {
                long f = polymer.Count(a => a == c);
                ret[c] = f;
            }
            if (remove_first)
            {
                ret[polymer[0]]--;
            }
            return ret;
        }

        private void CalculatePolymerAndFreq(string polymer, int iterations, bool remove_first)
        {
            Dictionary<char, long> ret;
            if (!memoization.ContainsKey(polymer))
            {
                var tmp = ProcessPolymer(polymer, iterations);
                ret = CalculateFrequencies(tmp, remove_first);
                memoization[polymer] = ret;
            }
            else
            {
                ret = memoization[polymer];
            }

            foreach(var item in ret)
            {
                if (freqs.ContainsKey(item.Key))
                {
                    freqs[item.Key] += item.Value;
                }
                else
                {
                    freqs[item.Key] = item.Value;
                }
            }
        }

        private void ReprocessPolymer(string polymer)
        {
            var demi = ProcessPolymer(polymer, 10);
            for (int i = 0; i < demi.Length - 1; i++)
            {
                CalculatePolymerAndFreq(string.Concat(demi[i], demi[i + 1]), 20, i != 0);
            };
        }

        public void ResolveSecondPart()
        {
            var demi = ProcessPolymer(polymer_base, 10);
            for (int i = 0; i < demi.Length - 1; i++)
            {
                ReprocessPolymer(string.Concat(demi[i], demi[i + 1]));
                Console.WriteLine($"iteration {i} sur {demi.Length - 1}, soit {i * 100 / (demi.Length-1) }%");
            };

            Console.WriteLine();
            var values = freqs.Values.ToList();
            values.Sort();
            var result = values.Last() - values.First();
            Console.WriteLine($"Result : {result}");
        }
    }
}
