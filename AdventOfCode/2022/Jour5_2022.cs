using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Jour5_2022 : IJour
    {
        List<(int, int, int)> steps;
        List<Stack<char>> initstacks;

        public void Init(string inputfile)
        {
            initstacks = new List<Stack<char>>();
            /*initstacks.Add(new Stack<char>(new [] { 'Z', 'N' }));
            initstacks.Add(new Stack<char>(new[] { 'M', 'C', 'D' }));
            initstacks.Add(new Stack<char>(new[] { 'P' }));*/
            initstacks.Add(new Stack<char>(new[] { 'R', 'P', 'C', 'D', 'B', 'G' }));
            initstacks.Add(new Stack<char>(new[] { 'H', 'V', 'G' }));
            initstacks.Add(new Stack<char>(new[] { 'N', 'S', 'Q', 'D', 'J', 'P', 'M' }));
            initstacks.Add(new Stack<char>(new[] { 'P', 'S', 'L', 'G', 'D', 'C', 'N', 'M' }));
            initstacks.Add(new Stack<char>(new[] { 'J', 'B', 'N', 'C', 'P', 'F', 'L', 'S' }));
            initstacks.Add(new Stack<char>(new[] { 'Q', 'B', 'D', 'Z', 'V', 'G', 'T', 'S' }));
            initstacks.Add(new Stack<char>(new[] { 'B', 'Z', 'M', 'H', 'F', 'T', 'Q' }));
            initstacks.Add(new Stack<char>(new[] { 'C', 'M', 'D', 'B', 'F' }));
            initstacks.Add(new Stack<char>(new[] { 'F', 'C', 'Q', 'G' }));

            steps = new List<(int, int, int)>();
            string[] lines = File.ReadAllLines(inputfile);
            var rx = new Regex(@"move ([0-9]+) from ([0-9]+) to ([0-9]+)", RegexOptions.Compiled);
            foreach (var line in lines)
            {
                var r = rx.Match(line);
                if (!r.Success)
                {
                    continue;
                }
                steps.Add((int.Parse(r.Groups[1].Value), int.Parse(r.Groups[2].Value), int.Parse(r.Groups[3].Value)));
            }
        }

        private List<Stack<char>> Clone(List<Stack<char>> toclone)
        {
            var cloned = new List<Stack<char>>();
            foreach(var item in toclone)
            { 
                cloned.Add(new Stack<char>(item.Reverse()));
            }
            return cloned;
        }

        public void ResolveFirstPart()
        {
            var stacks = Clone(initstacks);

            foreach (var step in steps)
            {
                for(int i = 0; i < step.Item1; i++)
                {
                    var tmp = stacks[step.Item2 - 1].Pop();
                    stacks[step.Item3 - 1].Push(tmp);
                }
            }

            StringBuilder resultat = new StringBuilder();
            foreach (var stack in stacks)
            {
                resultat.Append(stack.Pop());
            }

            Console.WriteLine($"Resultat = {resultat}");
        }

        public void ResolveSecondPart()
        {
            var stacks = Clone(initstacks);

            foreach (var step in steps)
            {
                var moved = new List<char>();
                for (int i = 0; i < step.Item1; i++)
                {
                    moved.Add(stacks[step.Item2 - 1].Pop());
                }
                moved.Reverse();
                foreach (var c in moved)
                {
                    stacks[step.Item3 - 1].Push(c);
                }
            }

            StringBuilder resultat = new StringBuilder();
            foreach (var stack in stacks)
            {
                resultat.Append(stack.Pop());
            }

            Console.WriteLine($"Resultat = {resultat}");
        }
    }
}
