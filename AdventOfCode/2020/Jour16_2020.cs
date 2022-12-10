using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Jour16_2020 : IJour
    {
        private class Range
        {
            public Range(string _label, int _start1, int _end1, int _start2, int _end2)
            {
                Label = _label;
                Start1 = _start1;
                End1 = _end1;
                Start2 = _start2;
                End2 = _end2;
            }
            public string Label { get; private set; }
            public int Start1 { get; private set; }
            public int End1 { get; private set; }
            public int Start2 { get; private set; }
            public int End2 { get; private set; }

            public bool Accept(int value)
            {
                return (value >= Start1 && value <= End1) || (value >= Start2 && value <= End2);
            }
        }
    
        List<Range> ranges = new List<Range>();

        List<int> MyTicket = new List<int>();

        List<List<int>> NearbyTickets = new List<List<int>>();

        List<List<int>> CorrectTickets = new List<List<int>>();

        int TicketSize;

        public void Init(string inputfile)
        {
            var rx = new Regex(@"([\w ]+): (\d+)-(\d+) or (\d+)-(\d+)", RegexOptions.Compiled);
            string[] lines = File.ReadAllLines(inputfile);

            int i = 0;
            while (!string.IsNullOrEmpty(lines[i]))
            {
                var r = rx.Match(lines[i]);
                if (r.Success)
                {
                    ranges.Add(new Range(r.Groups[1].Value, int.Parse(r.Groups[2].Value), int.Parse(r.Groups[3].Value), int.Parse(r.Groups[4].Value), int.Parse(r.Groups[5].Value)));
                }
                i++;
            }

            i += 2;
            var ticket_values = lines[i].Split(',');
            foreach(var s in ticket_values)
            {
                MyTicket.Add(int.Parse(s));
            }
            TicketSize = MyTicket.Count;

            i += 3;

            while (!string.IsNullOrEmpty(lines[i]))
            {
                ticket_values = lines[i].Split(',');
                var ticket = new List<int>();
                foreach (var s in ticket_values)
                {
                    ticket.Add(int.Parse(s));
                }
                NearbyTickets.Add(ticket);
                i++;
            }
        }

        public void ResolveFirstPart()
        {
            int result = 0;
            foreach(var ticket in NearbyTickets)
            {
                var correct = true;
                foreach(var value in ticket)
                {
                    var found = false;
                    foreach(var range in ranges)
                    {
                        if (range.Accept(value))
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        result += value;
                        correct = false;
                    }
                }
                if (correct)
                {
                    CorrectTickets.Add(ticket);
                }
            }
            Console.WriteLine($"Resultat = {result}");
        }

        private class Mapping
        {
            public Mapping(int _type, int _entry)
            {
                Type = _type;
                Entry = _entry;
            }
            public int Type { get; set; }
            public int Entry { get; set; }
        }

        private List<Mapping> FindPossibleSolutions(bool[,] matrice, List<Mapping> already_found)
        {
            List<Mapping> result = new List<Mapping>();
            for (int type = 0; type < TicketSize; type++)
            {
                if (already_found.Exists(p => p.Type == type))
                {
                    continue;
                }
                int count = 0;
                int found_entry = 0;
                for (int entry = 0; entry < TicketSize; entry++)
                {
                    if (already_found.Exists(p => p.Entry == entry))
                    {
                        continue;
                    }
                    if (matrice[type, entry])
                    {
                        count++;
                        found_entry = entry;
                    }
                }
                if (count==0)
                {
                    return null;
                }
                if (count==1)
                {
                    result.Add(new Mapping(type, found_entry));
                }
            }
            return result;
        }

        private void Print(bool[,] matrice, List<Mapping> already_found)
        {
            for (int type = 0; type < TicketSize; type++)
            {
                if (already_found.Exists(p => p.Type == type))
                {
                    continue;
                }
                StringBuilder str = new StringBuilder($"{ranges[type].Label, 20} : ");
                for (int entry = 0; entry < TicketSize; entry++)
                {
                    if (already_found.Exists(p => p.Entry == entry))
                    {
                        continue;
                    }
                    str.Append(matrice[type, entry] ? "X" : ".");
                }
                Console.WriteLine(str);
            }
        }

        private List<Mapping> ProceedWithPartialSolution(bool[,] matrice, List<Mapping> partialSolution)
        {
            if (partialSolution.Count == TicketSize)
            {
                // We have found the solution!!!
                return partialSolution;
            }

            var possible_solutions = FindPossibleSolutions(matrice, partialSolution);

            if (possible_solutions == null || possible_solutions.Count == 0)
            {
                // Dead end
                return null;
            }
            foreach (var possible_solution in possible_solutions)
            {
                var newList = new List<Mapping>(partialSolution);
                newList.Add(possible_solution);
                var result = ProceedWithPartialSolution(matrice, newList);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        private bool[,] GetMatrice()
        {
            var matrice = new bool[TicketSize, TicketSize];
            int type = 0;
            foreach (var range in ranges)
            {
                for (int entry = 0; entry < TicketSize; entry++)
                {
                    bool match = true;
                    foreach (var ticket in CorrectTickets)
                    {
                        if (!range.Accept(ticket[entry]))
                        {
                            match = false;
                        }
                    }
                    matrice[type, entry] = match;
                }
                type++;
            }
            return matrice;
        }

        public void ResolveSecondPart()
        {
            var matrice = GetMatrice();
            Print(matrice, new List<Mapping>());
            var solutionlist = ProceedWithPartialSolution(matrice, new List<Mapping>());
            if (solutionlist == null)
            {
                Console.WriteLine("No solution found to the problem :(");
            }
            else
            {
                long result = 1;
                foreach (var e in solutionlist)
                {
                    string label = ranges[e.Type].Label;
                    Console.WriteLine($"{label} ({e.Type}) correspond to entry {e.Entry} : {MyTicket[e.Entry]}");
                    if (label.StartsWith("departure"))
                    {
                        result *= MyTicket[e.Entry];
                    }
                }

                Console.WriteLine($"Result = {result}");
            }
        }     
    }
}
