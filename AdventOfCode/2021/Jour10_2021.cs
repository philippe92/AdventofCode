using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Jour10_2021 : IJour
    {
        string[] lines;

        List<char[]> remainings;

        public void Init(string inputfile)
        {
            lines = File.ReadAllLines(inputfile);
        }

        public void ResolveFirstPart()
        {
            remainings = new List<char[]>();
            int result = 0;
            foreach(var line in lines)
            {
                Stack<char> q = new Stack<char>();
                bool valid = true;
                foreach(var c in line)
                {
                    if (c == '[' || c == '(' || c == '{' || c == '<')
                    {
                        q.Push(c);
                    }
                    else
                    {
                        if (q.Count == 0)
                        {
                            Console.WriteLine("0!!!");
                            break;
                        }
                        var old = q.Pop();
                        if (old != '(' && c == ')')
                        {
                            valid = false;
                            result += 3;
                            break;
                        }
                        if (old != '[' && c == ']')
                        {
                            valid = false;
                            result += 57;
                            break;
                        }
                        if (old != '{' && c == '}')
                        {
                            valid = false;
                            result += 1197;
                            break;
                        }
                        if (old != '<' && c == '>')
                        {
                            valid = false;
                            result += 25137;
                            break;
                        }
                    }
                }
                if (valid)
                {
                    remainings.Add(q.ToArray());
                }
            }
            Console.WriteLine($"Resultat : {result}");
        }

        public void ResolveSecondPart()
        {
            List<long> scores = new List<long>();
            foreach(var item in remainings)
            {
                long score = 0;
                foreach(var c in item)
                {
                    score *= 5;
                    switch (c)
                    {
                        case '(':
                            score += 1;
                            break;
                        case '[':
                            score += 2;
                            break;
                        case '{':
                            score += 3;
                            break;
                        case '<':
                            score += 4;
                            break;
                        default:
                            Console.WriteLine("error!!!");
                            break;
                    }
                }
                scores.Add(score);
            }
            scores.Sort();
            var result = scores.Skip(scores.Count / 2).First();

            Console.WriteLine($"Resultat : {result}");
        }
    }
}
