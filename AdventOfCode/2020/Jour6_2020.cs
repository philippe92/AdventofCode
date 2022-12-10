using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Jour6_2020 : IJour
    {
        List<HashSet<char>> groups;

        List<HashSet<char>> groups2;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            groups = new List<HashSet<char>>();
            groups2 = new List<HashSet<char>>();
            var answers = new HashSet<char>();
            var answers2 = new HashSet<char>();
            bool create_reference = true;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    groups.Add(answers);
                    groups2.Add(answers2);
                    answers = new HashSet<char>();
                    answers2 = new HashSet<char>();
                    create_reference = true;
                }
                else
                {
                    var chars = new List<char>(line.ToCharArray());

                    foreach (var c in chars)
                    {
                        answers.Add(c);
                    }

                    if (create_reference)
                    {
                        // Create reference
                        foreach (var c in chars)
                        {
                            answers2.Add(c);
                        }
                        create_reference = false;
                    }
                    else
                    {
                        var toremove = new List<char>();
                        foreach (var c in answers2)
                        {
                            if (!chars.Contains(c))
                            {
                                toremove.Add(c);
                            }
                        }

                        foreach (var c in toremove)
                        {
                            answers2.Remove(c);
                        }
                    }
                }
            }
            groups.Add(answers);
            groups2.Add(answers2);
        }

        public void ResolveFirstPart()
        {
            var count = 0;
            groups.ForEach(p => count += p.Count);
            Console.WriteLine($"Resultat = {count}");
        }

        public void ResolveSecondPart()
        {
            var count = 0;
            groups2.ForEach(p => count += p.Count);
            Console.WriteLine($"Resultat = {count}");
        }
    }
}
