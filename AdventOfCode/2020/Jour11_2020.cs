using System;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Jour11_2020 : IJour
    {
        char[,] map;

        char[,] current;
        char[,] next;

        int x;
        int y;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            y = lines[0].Length;
            x = lines.Length;

            map = new char[x,y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    map[i,j] = lines[i][j];
                }
            }
        }

        private int IsOccupied(int i, int j)
        {
            if (i<0 || i>=x || j<0 || j>=y)
            {
                return 0;
            }
            return current[i, j] == '#' ? 1 : 0;
        }

        private int NearOccupiedSeats(int i, int j)
        {
            int n = 0;
            n += IsOccupied(i - 1, j - 1);
            n += IsOccupied(i - 1, j);
            n += IsOccupied(i - 1, j + 1);
            n += IsOccupied(i, j - 1);
            n += IsOccupied(i, j + 1);
            n += IsOccupied(i + 1, j - 1);
            n += IsOccupied(i + 1, j);
            n += IsOccupied(i + 1, j + 1);
            return n;
        }

        private void DoStep()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (current[i, j] == '.')
                    {
                        next[i, j] = '.';
                    }
                    if (current[i, j] == 'L')
                    {
                        if (NearOccupiedSeats(i, j) == 0)
                        {
                            next[i, j] = '#';
                        }
                        else
                        {
                            next[i, j] = 'L';
                        }
                        continue;
                    }
                    if (current[i, j] == '#')
                    {
                        if (NearOccupiedSeats(i, j) >= 4)
                        {
                            next[i, j] = 'L';
                        }
                        else
                        {
                            next[i, j] = '#';
                        }
                    }
                }
            }
        }

        private void Switch()
        {
            var tmp = current;
            current = next;
            next = tmp;
        }

        private bool AreSame()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (current[i,j] != next[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int CountSeats()
        {
            int result = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (current[i, j] == '#')
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private void Copy()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    current[i, j] = map[i, j];
                }
            }
        }

        public void ResolveFirstPart()
        {
            current = new char[x, y];
            next = new char[x, y];
            Copy();
            do
            {
                DoStep();
                Switch();
            } 
            while (!AreSame());

            Console.WriteLine($"Result : {CountSeats()}");
        }

        private int IsOccupiedInDirection(int i, int j, int dx, int dy)
        {
            i += dx;
            j += dy;
            while (i >= 0 && i < x && j >= 0 && j < y)
            {
                if (current[i, j] == '#')
                {
                    return 1;
                }
                if (current[i, j] == 'L')
                {
                    return 0;
                }
                i += dx;
                j += dy;
            }
            return 0;
        }

        private int NearOccupiedSeatsWithDirection(int i, int j)
        {
            int n = 0;
            n += IsOccupiedInDirection(i, j, -1, -1);
            n += IsOccupiedInDirection(i, j, -1, 0);
            n += IsOccupiedInDirection(i, j, -1, 1);
            n += IsOccupiedInDirection(i, j, 0, -1);
            n += IsOccupiedInDirection(i, j, 0, 1);
            n += IsOccupiedInDirection(i, j, 1, -1);
            n += IsOccupiedInDirection(i, j, 1, 0);
            n += IsOccupiedInDirection(i, j, 1, 1);
            return n;
        }


        private void DoStep2()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (current[i, j] == '.')
                    {
                        next[i, j] = '.';
                    }
                    if (current[i, j] == 'L')
                    {  
                        if (NearOccupiedSeatsWithDirection(i, j) == 0)
                        {
                            next[i, j] = '#';
                        }
                        else
                        {
                            next[i, j] = 'L';
                        }
                        continue;
                    }
                    if (current[i, j] == '#')
                    {
                        if (NearOccupiedSeatsWithDirection(i, j) >= 5)
                        {
                            next[i, j] = 'L';
                        }
                        else
                        {
                            next[i, j] = '#';
                        }
                    }

                }
            }
        }

        public void ResolveSecondPart()
        {
            current = new char[x, y];
            next = new char[x, y];
            Copy();
            do
            {
                DoStep2();
                Switch();
            }
            while (!AreSame());

            Console.WriteLine($"Result : {CountSeats()}");
        }
    }
}
