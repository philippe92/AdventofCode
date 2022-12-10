using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    internal class Jour1_2022 : IJour
    {
        List<List<int>> data;
        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            data = new List<List<int>>();
            int i = 0;
            while (i < lines.Length)
            {
                List<int> tmp = new List<int>();
                while (i < lines.Length && !string.IsNullOrEmpty(lines[i]))
                {
                    tmp.Add(int.Parse(lines[i]));
                    i++;
                }
                data.Add(tmp);
                i++;
            }
        }

        public void ResolveFirstPart()
        {
            int max = 0;
            foreach(var l in data)
            {
                max = Math.Max(l.Sum(), max);
            }
            Console.WriteLine($"Max = {max}");
        }

        public void ResolveSecondPart()
        {
            List<int> calories = new List<int>();
            foreach(var l in data)
            {
                calories.Add(l.Sum());
            }
            calories.Sort((a, b) => b.CompareTo(a));
            Console.WriteLine($"Result = {calories[0]+ calories[1]+ calories[2]}");
        }
    }
}
