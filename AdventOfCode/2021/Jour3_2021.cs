using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Jour3_2021 : IJour
    {
        private string[] lines;

        public void Init(string inputfile)
        {
            lines = File.ReadAllLines(inputfile);
        }

        public void ResolveFirstPart()
        {
            int gamma = 0;
            int epsilon = 0;
            int size = lines[0].Length;
            int count = lines.Length;
            for (int i = 0; i < size; i++)
            {
                int nb = 0;
                foreach (var line in lines)
                {
                    if (line[i] == '1')
                    {
                        nb++;
                    }
                }
                if (nb>count/2)
                {
                    gamma += 1 << (size-i-1);
                }
                else if (nb<count/2)
                {
                    epsilon += 1 << (size - i - 1);
                }
            }
            Console.WriteLine($"Gamma = {gamma}");
            Console.WriteLine($"Epsilon = {epsilon}");
            Console.WriteLine($"Result = {epsilon* gamma}");
        }

        private int Calculate(bool inverse)
        {
            int size = lines[0].Length;
            List<string> workinglist = new List<string>(lines);
            for (int i = 0; i < size; i++)
            {
                int nb = 0;
                bool remove_true = false;
                bool remove_false = false;
                int count = workinglist.Count(p => p != null);
                if (count == 1)
                {
                    break;
                }
                foreach (var line in workinglist)
                {
                    if (line == null)
                    {
                        continue;
                    }
                    if (line[i] == '1')
                    {
                        nb++;
                    }
                }
                var test = nb >= (count - nb);
                if ((!inverse && test) || (inverse && !test ))
                {
                    remove_false = true;
                }
                else
                {
                    remove_true = true;
                    
                }
                for (int j = 0; j < workinglist.Count; j++)
                {
                    if (workinglist[j] == null)
                    {
                        continue;
                    }
                    if ((workinglist[j][i] == '1' && remove_true) || (workinglist[j][i] == '0' && remove_false))
                    {
                        workinglist[j] = null;
                    }
                }
            }
            var finalline = workinglist.First(p => p != null);
            int value = 0;
            for (int i = 0; i < finalline.Length; i++)
            {
                if (finalline[i] == '1')
                {
                    value += 1 << (size - i - 1);
                }
            }
            return value;
        }


        public void ResolveSecondPart()
        {

            int oxygen = Calculate(false);
            int co2 = Calculate(true);

            Console.WriteLine($"Oxygen = {oxygen}");
            Console.WriteLine($"CO2 = {co2}");

            Console.WriteLine($"Result = {oxygen*co2}");
        }
    }
}
