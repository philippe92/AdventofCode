using System;
using System.IO;

namespace AdventOfCode
{
    class Jour17_2020 : IJour
    {
        static int size = 20;

        private class Space
        {
            private bool[,,] space;
            private bool[,,] buffer;
            private readonly int size;
            private readonly int offset;

            public Space(int _size)
            {
                size = _size*2;
                space = new bool[size, size, size];
                buffer = new bool[size,size,size ];
                offset = size / 2;
            }

            public void Set(int x, int y)
            {
                space[x + offset, y + offset, offset] = true;
            }

            private int SumNeighbors(int x, int y, int z)
            {
                int result = 0;
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        for (int k = z - 1; k <= z + 1; k++)
                        {
                            if (i==x && j==y && k==z)
                            {
                                continue;
                            }
                            if (space[i,j,k])
                            {
                                result++;
                            }
                        }
                    }
                }
                return result;
            }

            public int Count()
            {
                int result = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        for (int k = 0; k < size; k++)
                        {
                            if (space[i, j, k])
                            {
                                result++;
                            }
                        }
                    }
                }
                return result;
            }

            public void DoStep()
            {
                for (int i = 1; i < size-1; i++)
                {
                    for (int j = 1; j < size-1; j++)
                    {
                        for (int k = 1; k < size-1; k++)
                        {
                            int n = SumNeighbors(i, j, k);
                            if (space[i,j,k])
                            {
                                if (n==2 ||n==3)
                                {
                                    buffer[i, j, k] = true;
                                }
                                else
                                {
                                    buffer[i, j, k] = false;
                                }
                            }
                            else
                            {
                                if (n==3)
                                {
                                    buffer[i, j, k] = true;
                                }
                                else
                                {
                                    buffer[i, j, k] = false;
                                }
                            }
                        }
                    }
                }
                
                var tmp = space;
                space = buffer;
                buffer = tmp;
            }
        }

        private class Hyperspace
        {
            private bool[,,,] space;
            private bool[,,,] buffer;
            private readonly int size;
            private readonly int offset;

            public Hyperspace(int _size)
            {
                size = _size * 2;
                space = new bool[size, size, size, size];
                buffer = new bool[size, size, size, size];
                offset = size / 2;
            }

            public void Set(int x, int y)
            {
                space[x + offset, y + offset, offset, offset] = true;
            }

            private int SumNeighbors(int x, int y, int z, int w)
            {
                int result = 0;
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        for (int k = z - 1; k <= z + 1; k++)
                        {
                            for (int l = w - 1; l <= w + 1; l++)
                            {
                                if (i == x && j == y && k == z && l == w)
                                {
                                    continue;
                                }
                                if (space[i, j, k, l])
                                {
                                    result++;
                                }
                            }
                        }
                    }
                }
                return result;
            }

            public int Count()
            {
                int result = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        for (int k = 0; k < size; k++)
                        {
                            for (int l = 0; l < size; l++)
                            {
                                if (space[i, j, k, l])
                                {
                                    result++;
                                }
                            }
                        }
                    }
                }
                return result;
            }

            public void DoStep()
            {
                for (int i = 1; i < size - 1; i++)
                {
                    for (int j = 1; j < size - 1; j++)
                    {
                        for (int k = 1; k < size - 1; k++)
                        {
                            for (int l = 1; l < size - 1; l++)
                            {
                                int n = SumNeighbors(i, j, k, l);
                                if (space[i, j, k, l])
                                {
                                    if (n == 2 || n == 3)
                                    {
                                        buffer[i, j, k, l] = true;
                                    }
                                    else
                                    {
                                        buffer[i, j, k, l] = false;
                                    }
                                }
                                else
                                {
                                    if (n == 3)
                                    {
                                        buffer[i, j, k, l] = true;
                                    }
                                    else
                                    {
                                        buffer[i, j, k, l] = false;
                                    }
                                }
                            }
                        }
                    }
                }

                var tmp = space;
                space = buffer;
                buffer = tmp;
            }
        }

        private Space space;
        private Hyperspace hyperspace;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            space = new Space(size);
            hyperspace = new Hyperspace(size);
            int i = 0;
            foreach (var line in lines)
            {
                int j = 0;
                foreach(var c in line.ToCharArray())
                {
                    if (c == '#')
                    {
                        space.Set(i, j);
                        hyperspace.Set(i, j);
                    }
                    j++;
                }
                i++;
            }
        }

        public void ResolveFirstPart()
        {
            for (int i = 0; i < 6; i++)
            {
                space.DoStep();
            }
            var result = space.Count();
            Console.WriteLine($"Resultat = {result}");
        }

        public void ResolveSecondPart()
        {
            for (int i = 0; i < 6; i++)
            {
                hyperspace.DoStep();
            }
            var result = hyperspace.Count();
            Console.WriteLine($"Resultat = {result}");
        }
    }
}
