using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Jour7_2020 : IJour
    {
        internal class Bag
        {
            public Bag(string color)
            {
                Color = color;
                Children = new List<Tuple<int, string>>();
            }
            public string Color { get; set; }

            public List<Tuple<int, string>> Children;
        }

        List<Bag> bags = new List<Bag>();

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            var rx = new Regex(@"([a-z ]+) bags contain (.*)", RegexOptions.Compiled);
            var rx2 = new Regex(@"([0-9]+) ([a-z ]+) bags{0,1}[,.]", RegexOptions.Compiled);
            foreach (var line in lines)
            {
                var r = rx.Match(line);
                if (!r.Success)
                {
                    continue;
                }
                var bag = new Bag(r.Groups[1].Value);

                var r2 = rx2.Matches(r.Groups[2].Value);
                if (r2.Count == 0)
                {
                    continue;
                }


                foreach (Match m in r2)
                {
                    bag.Children.Add(new Tuple<int, string>(int.Parse(m.Groups[1].Value), m.Groups[2].Value));
                }
                bags.Add(bag);
            }
        }

        private bool CheckBag(Bag bag)
        {
            if (bag.Color == "shiny gold")
            {
                return true;
            }
            foreach (var child in bag.Children)
            {
                var childbag = bags.Find(p => p.Color == child.Item2);
                if (childbag == null)
                {
                    continue;
                }
                if (CheckBag(childbag))
                {
                    return true;
                }
            }
            return false;
        }

        public void ResolveFirstPart()
        {
            var count = 0;
            foreach(var bag in bags)
            {
                if (bag.Color == "shiny gold")
                {
                    //ahah
                    continue;
                }
                if (CheckBag(bag))
                {
                    count++;
                }
            }
            Console.WriteLine($"Resultat = {count}");
        }

        private int CountBags(Bag bag)
        {
            int bags_count = 1;
            foreach (var child in bag.Children)
            {
                var childbag = bags.Find(p => p.Color == child.Item2);
                if (childbag == null)
                {
                    bags_count += child.Item1;
                }
                else
                {
                    bags_count += child.Item1 * CountBags(childbag);
                }
            }
            return bags_count;
        }

        public void ResolveSecondPart()
        {
            var shiny = bags.Find(p => p.Color == "shiny gold");
            var count = CountBags(shiny) - 1;
            Console.WriteLine($"Resultat = {count}");
        }
    }
}
