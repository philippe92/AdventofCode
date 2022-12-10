using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Jour7_2021 : IJour
    {
        List<int> init_data;
        public void Init(string inputfile)
        {
            string input = File.ReadLines(inputfile).First();
            init_data = new List<int>();
            foreach (var s in input.Split(','))
            {
                init_data.Add(int.Parse(s));
            }
        }

        public void ResolveFirstPart()
        {
            int min = init_data.Min();
            int max = init_data.Max();
            int fuel;
            int min_fuel = int.MaxValue;
            for (int i = min; i < max; i++)
            {
                fuel = 0;
                for (int j = 0; j < init_data.Count; j++)
                {
                    fuel += Math.Abs(init_data[j] - i);
                }
                min_fuel = Math.Min(min_fuel, fuel);
            }
            

            Console.WriteLine($"min_fuel : {min_fuel}");
        }

        public void ResolveSecondPart()
        {
            int min = init_data.Min();
            int max = init_data.Max();
            int fuel;
            int min_fuel = int.MaxValue;
            for (int i = min; i < max; i++)
            {
                fuel = 0;
                for (int j = 0; j < init_data.Count; j++)
                {
                   int n = Math.Abs(init_data[j] - i);
                   fuel += n * (n + 1) / 2;
                }
                min_fuel = Math.Min(min_fuel, fuel);
            }

            Console.WriteLine($"min_fuel : {min_fuel}");
        }
    }
}
