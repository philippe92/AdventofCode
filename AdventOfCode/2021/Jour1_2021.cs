using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Jour1_2021 : IJour
    {
        List<int> scan;
        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            scan = new List<int>();
            foreach (var line in lines)
            {
                scan.Add(int.Parse(line));
            }
        }

        public void ResolveFirstPart()
        {
            var current = 0;
            var result = 0;
            bool first = true;
            foreach(var i in scan)
            {
                
                if (first)
                {
                    first = false;
                    current = i;
                    continue;
                }
                if (i>current)
                {
                    result++;
                }
                current = i;
            }
            Console.WriteLine($"Resultat = {result}");
        }

        public void ResolveSecondPart()
        {
            List<int> calculated = new List<int>();
            for (int i = 0; i < scan.Count - 2; i++)
            {
                calculated.Add(scan[i] + scan[i + 1] + scan[i + 2]);
            }

            var current = 0;
            var result = 0;
            bool first = true;
            foreach (var i in calculated)
            {
                if (first)
                {
                    first = false;
                    current = i;
                    continue;
                }
                if (i > current)
                {
                    result++;
                }
                current = i;
            }
            Console.WriteLine($"Resultat = {result}");
        }
    }
}
