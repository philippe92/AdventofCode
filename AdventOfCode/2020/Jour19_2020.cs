using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Jour19_2020 : IJour
    {
        private class Rule
        {
            private Rule()
            { }

            public Rule(int id, int rule1, int rule2, int rule3, int rule4)
            {
                Id = id;
                subrules1 = new List<int> { rule1, rule2 };
                subrules2 = new List<int> { rule3, rule4 };
            }

            public Rule(int id, int rule1)
            {
                Id = id;
                subrules1 = new List<int> { rule1 };
            }

            public Rule(int id, int rule1, int rule2, int rule3)
            {
                Id = id;
                subrules1 = new List<int> { rule1, rule2, rule3 };
            }

            public Rule(int id, char c)
            {
                Id = id;
                Value = c;
            }

            public static Rule CreateOrDoubleRule(int id, int rule1, int rule2)
            {
                var rule = new Rule
                {
                    Id = id,
                    subrules1 = new List<int> { rule1 },
                    subrules2 = new List<int> { rule2 }
                };
                return rule;
            }

            public static Rule CreateAndDoubleRule(int id, int rule1, int rule2)
            {
                var rule = new Rule
                {
                    Id = id,
                    subrules1 = new List<int> { rule1, rule2 }
                };
                return rule;
            }

            public int Id { get; private set; }
            public char Value { get; private set; }
            List<int> subrules1;
            List<int> subrules2;

            public bool ValidateString(string value, ref int offset, bool withloop)
            {
                if (withloop && offset >= value.Length)
                {
                    return false;
                }

                if (Value != 0)
                {
                    bool test = value[offset] == Value;
                    offset++;
                    return test;
                }

                bool result = true;
                int temp_offset = offset;

                if (Id == 0 && withloop)
                {
                    for (int i = 3; i < 200; i++)
                    { 
                        for (int j = 1; j < i - 1; j++)
                        {
                            result = true;
                            offset = temp_offset;
                            for (int k = 0; k < i-j; k++)
                            {
                                result &= FindRule(42).ValidateString(value, ref offset, withloop);
                                if(offset >= value.Length)
                                {
                                    break;
                                }
                            }

                            if (offset >= value.Length)
                            {
                                continue;
                            }

                            for (int k = 0; k < j; k++)
                            {
                                result &= FindRule(31).ValidateString(value, ref offset, withloop);
                                if (offset >= value.Length)
                                {
                                    break;
                                }
                            }
                            if (result && offset == value.Length)
                            {
                                return true;
                            }
                        }
                    } 
                    return false;
                }

                foreach (var ruleid in subrules1)
                {
                    result &= FindRule(ruleid).ValidateString(value, ref offset, withloop);
                    if (!result)
                    {
                        break;
                    }
                }

                if (result)
                {
                    return true;
                }

                if (subrules2 != null)
                {
                    offset = temp_offset;
                    result = true;
                    foreach (var ruleid in subrules2)
                    {
                        result &= FindRule(ruleid).ValidateString(value, ref offset, withloop);
                        if (!result)
                        {
                            break;
                        }
                    }
                }

                return result;
            }
        }

        private static List<Rule> rules = new List<Rule>();

        private static Rule FindRule(int id)
        {
            return rules.Find(p => p.Id == id);
        }

        private List<string> values = new List<string>();

        public void Init(string inputfile)
        {
            var rx1 = new Regex(@"^(\d+): (\d+) (\d+) \| (\d+) (\d+)$", RegexOptions.Compiled);
            var rx2 = new Regex(@"^(\d+): (\d+) \| (\d+)$", RegexOptions.Compiled);
            var rx3 = new Regex(@"^(\d+): (\d+) (\d+)$", RegexOptions.Compiled);
            var rx4 = new Regex(@"^(\d+): (\d+) (\d+) (\d+)$", RegexOptions.Compiled);
            var rx5 = new Regex(@"^(\d+): (\d+)$", RegexOptions.Compiled);
            var rx6 = new Regex(@"^(\d+): ""(\w)""$", RegexOptions.Compiled);
            var lines = File.ReadAllLines(inputfile);

            int i = 0;
            while (!string.IsNullOrEmpty(lines[i]))
            {
                var r = rx1.Match(lines[i]);
                if (r.Success)
                {
                    rules.Add(new Rule(int.Parse(r.Groups[1].Value), int.Parse(r.Groups[2].Value), int.Parse(r.Groups[3].Value), int.Parse(r.Groups[4].Value), int.Parse(r.Groups[5].Value)));
                    i++;
                    continue;
                }

                r = rx2.Match(lines[i]);
                if (r.Success)
                {
                    rules.Add(Rule.CreateOrDoubleRule(int.Parse(r.Groups[1].Value), int.Parse(r.Groups[2].Value), int.Parse(r.Groups[3].Value)));
                    i++;
                    continue;
                }

                r = rx3.Match(lines[i]);
                if (r.Success)
                {
                    rules.Add(Rule.CreateAndDoubleRule(int.Parse(r.Groups[1].Value), int.Parse(r.Groups[2].Value), int.Parse(r.Groups[3].Value)));
                    i++;
                    continue;
                }

                r = rx4.Match(lines[i]);
                if (r.Success)
                {
                    rules.Add(new Rule(int.Parse(r.Groups[1].Value), int.Parse(r.Groups[2].Value), int.Parse(r.Groups[3].Value), int.Parse(r.Groups[4].Value)));
                    i++;
                    continue;
                }

                r = rx5.Match(lines[i]);
                if (r.Success)
                {
                    rules.Add(new Rule(int.Parse(r.Groups[1].Value), int.Parse(r.Groups[2].Value)));
                    i++;
                    continue;
                }

                r = rx6.Match(lines[i]);
                if (r.Success)
                {
                    rules.Add(new Rule(int.Parse(r.Groups[1].Value), r.Groups[2].Value[0]));
                    i++;
                    continue;
                }

                Console.WriteLine($"Not recognized line : {lines[i]}");
                i++;
            }

            i++;
            while (i < lines.Length && !string.IsNullOrEmpty(lines[i]))
            {
                values.Add(lines[i]);
                i++;
            }
        }

        public void ResolveFirstPart()
        {
            int result = 0;
            foreach(var value in values)
            {
                int offset = 0;
                if (FindRule(0).ValidateString(value, ref offset, false) && offset == value.Length)
                {
                    result++;
                }
            }
            Console.WriteLine($"Result = {result}");
        }

        public void ResolveSecondPart()
        {
            int result = 0;
            foreach (var value in values)
            {
                int offset = 0;
                if (FindRule(0).ValidateString(value, ref offset, true))
                {
                    Console.WriteLine(value);
                    result++;
                }
            }
            Console.WriteLine($"Result = {result}");
        }
    }
}


