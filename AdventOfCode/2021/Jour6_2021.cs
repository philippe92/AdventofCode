using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Jour6_2021 : IJour
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
            List<int> data = new List<int>(init_data);
            int to_add;
            for (int i=0;i<80;i++)
            {
                to_add = 0;
                for (int j = 0; j < data.Count; j++)
                {
                    if (data[j] == 0)
                    {
                        to_add++;
                        data[j] = 6;
                    }
                    else
                    {
                        data[j]--;
                    }
                }
                for(int j=0;j<to_add;j++)
                {
                    data.Add(8);
                }
            }
            Console.WriteLine($"Resultat : {data.Count}");
        }

        public void ResolveSecondPart()
        {
            List<long> states = new List<long> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (var i in init_data)
            {
                states[i]++;
            }

            for (int i = 0; i < 256; i++)
            {
                List<long> buffer = new List<long> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int j = 8; j >=0; j--)
                {
                    if (j == 0)
                    {
                        buffer[6] += states[j];
                        buffer[8] += states[j];
                        states[0] = 0;
                    }
                    else
                    {
                        buffer[j - 1] += states[j];
                        states[j] = 0;
                    }
                }
                for(int j = 0; j < 9 ; j++)
                {
                    states[j] += buffer[j];
                }
            }
            long result = 0;
            foreach(var val in states)
            {
                result += val;
            }
            Console.WriteLine($"Resultat : {result}");
        }
    }
}
