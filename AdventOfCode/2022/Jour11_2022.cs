using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Jour11_2022 : IJour
    {
        private static List<Monkey> monkeys;

        private class Monkey
        {
            private Queue<long> _items;
            private Func<long, long> _operation;
            private int _divisor;
            private int _monkey1;
            private int _monkey2;

            public int Inspections { get; set; }

            public Monkey(int[] items, Func<long, long> operation, int divisor, int monkey1, int monkey2)
            {
                _items = new Queue<long>();
                foreach(var i in items)
                {
                    _items.Enqueue(i);
                }
                _operation = operation;
                _divisor = divisor;
                _monkey1 = monkey1;
                _monkey2 = monkey2;
            }

            public void ReceiveBall(long item)
            {
                _items.Enqueue(item);
            }

            public void PlayTurn()
            {
                while (_items.Count > 0)
                {
                    long worry = _items.Dequeue();
                    Inspections++;
                    worry = _operation(worry);
                    worry /= 3; 
                    
                    if (worry % _divisor == 0)
                    {
                        ThrowBall(worry, _monkey1);
                    }
                    else
                    {
                        ThrowBall(worry, _monkey2);
                    }
                }
            }

            public void PlayTurn2()
            {
                while (_items.Count > 0)
                {
                    long worry = _items.Dequeue();
                    Inspections++;
                    worry = _operation(worry);

                    //worry %= 19 * 23 * 13 * 17;
                    worry %= 2 * 7 * 13 * 3 * 19 * 5 * 11 * 17;

                    if (worry % _divisor == 0)
                    {
                        ThrowBall(worry, _monkey1);
                    }
                    else
                    {
                        ThrowBall(worry, _monkey2);
                    }
                }
            }
        }

        public static void ThrowBall(long item, int monkey)
        {
            monkeys[monkey].ReceiveBall(item);
        }

        public void Init(string inputfile)
        {
            monkeys = new List<Monkey>
            {
                /*new Monkey(new[] { 79, 98 }, p => p * 19, 23, 2, 3),
                new Monkey(new[] { 54, 65, 75, 74 }, p => p + 6, 19, 2, 0),
                new Monkey(new[] { 79, 60, 97 }, p => p * p, 13, 1, 3),
                new Monkey(new[] { 74 }, p => p + 3, 17, 0, 1)*/

                new Monkey(new[] { 50, 70, 89, 75, 66, 66 }, p => p * 5, 2, 2, 1),
                new Monkey(new[] { 85 }, p => p * p, 7, 3, 6),
                new Monkey(new[] { 66, 51, 71, 76, 58, 55, 58, 60 }, p => p + 1, 13, 1, 3),
                new Monkey(new[] { 79, 52, 55, 51 }, p => p + 6, 3, 6, 4),
                new Monkey(new[] { 69, 92 }, p => p * 17, 19, 7, 5),
                new Monkey(new[] { 71, 76, 73, 98, 67, 79, 99 }, p => p + 8, 5, 0, 2),
                new Monkey(new[] { 82, 76, 69, 69, 57 }, p => p + 7, 11, 7, 4),
                new Monkey(new[] { 65, 79, 86 }, p => p + 5, 17, 5, 0)
            };

        }

        public void ResolveFirstPart()
        {
            for (int i=0; i<20; i++)
            {
                foreach(var m in monkeys)
                {
                    m.PlayTurn();
                }
            }
            List<int> result = new List<int>();
            monkeys.ForEach(p => result.Add(p.Inspections));
            result.Sort((a, b) => b.CompareTo(a));
            Console.WriteLine($"Resultat = {result[0] * result[1]}");
        }

        public void ResolveSecondPart()
        {
            monkeys.Clear();
            Init("");
            for (int i = 0; i < 10000; i++)
            {
                foreach (var m in monkeys)
                {
                    m.PlayTurn2();
                }
            }

            List<int> result = new List<int>();
            monkeys.ForEach(p => result.Add(p.Inspections));
            result.Sort((a, b) => b.CompareTo(a));
            Console.WriteLine($"Resultat = {(long)result[0] * result[1]}");
        }
    }
}
