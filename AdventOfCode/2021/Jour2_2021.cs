using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Jour2_2021 : IJour
    {
        private string[] lines;

        public void Init(string inputfile)
        {
            lines = File.ReadAllLines(inputfile);      
        }

        public void ResolveFirstPart()
        {
            int depth = 0;
            int pos = 0;
            var rx = new Regex(@"(forward|down|up) ([0-9]+)", RegexOptions.Compiled);
            foreach (var line in lines)
            {
                var r = rx.Match(line);
                if (!r.Success)
                {
                    continue;
                }
                string operation = r.Groups[1].Value;
                int value = int.Parse(r.Groups[2].Value);
                switch(operation)
                {
                    case "forward":
                        pos += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                    case "down":
                        depth += value;
                        break;
                }
            }
            Console.WriteLine($"Resultat = {depth * pos}");
        }

        public void ResolveSecondPart()
        {
            int depth = 0;
            int pos = 0;
            int aim = 0;
            var rx = new Regex(@"(forward|down|up) ([0-9]+)", RegexOptions.Compiled);
            foreach (var line in lines)
            {
                var r = rx.Match(line);
                if (!r.Success)
                {
                    continue;
                }
                string operation = r.Groups[1].Value;
                int value = int.Parse(r.Groups[2].Value);
                switch (operation)
                {
                    case "forward":
                        pos += value;
                        depth += aim * value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    case "down":
                        aim += value;
                        break;
                }
            }
            Console.WriteLine($"Resultat = {depth * pos}");
        }
    }
}
