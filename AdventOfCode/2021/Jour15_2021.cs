using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Jour15_2021 : IJour
    {
        int[,] maprisk;
        static int x;
        static int y;

        private struct Tile
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Cost { get; set; }
            public int CostDistance => Cost + (x - X) + (y - Y);
            public bool setted { get; set; }
        }

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            y = lines[0].Length;
            x = lines.Length;

            maprisk = new int[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    maprisk[i, j] = lines[i][j] - '0';
                }
            }
        }

        private void Do()
        {
            var start = new Tile { X = 0, Y = 0 };
            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new HashSet<int>();

            // ce bon vieux A*
            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == x - 1 && checkTile.Y == y - 1)
                {
                    Console.WriteLine($"Resultat = {checkTile.Cost}");
                    return;
                }

                visitedTiles.Add(checkTile.X * x + checkTile.Y);
                activeTiles.Remove(checkTile);

                Tile[] possibleTiles = new Tile[4];
                if (checkTile.Y < y - 1)
                {
                    possibleTiles[0].X = checkTile.X;
                    possibleTiles[0].Y = checkTile.Y + 1;
                    possibleTiles[0].Cost = checkTile.Cost + maprisk[checkTile.X, checkTile.Y + 1];
                    possibleTiles[0].setted = true;
                }
                if (checkTile.X < x - 1)
                {
                    possibleTiles[1].X = checkTile.X + 1;
                    possibleTiles[1].Y = checkTile.Y;
                    possibleTiles[1].Cost = checkTile.Cost + maprisk[checkTile.X + 1, checkTile.Y];
                    possibleTiles[1].setted = true;
                }
                if (checkTile.Y > 0)
                {
                    possibleTiles[2].X = checkTile.X;
                    possibleTiles[2].Y = checkTile.Y - 1;
                    possibleTiles[2].Cost = checkTile.Cost + maprisk[checkTile.X, checkTile.Y - 1];
                    possibleTiles[2].setted = true;
                }
                if (checkTile.X > 0)
                {
                    possibleTiles[3].X = checkTile.X - 1;
                    possibleTiles[3].Y = checkTile.Y;
                    possibleTiles[3].Cost = checkTile.Cost + maprisk[checkTile.X - 1, checkTile.Y];
                    possibleTiles[3].setted = true;
                }

                foreach (var walkableTile in possibleTiles)
                {
                    if (walkableTile.setted == false)
                    {
                        continue;
                    }
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Contains(walkableTile.X * x + walkableTile.Y))
                    {
                        continue;
                    }

                    //It's already in the active list, but that's OK, maybe this new tile has a better value
                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > walkableTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
            }
        }


        public void ResolveFirstPart()
        {
            Do();
        }

        private void CopyBlock(int[,] map, int offsetX, int offsetY)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    int a = i + (x * offsetX);
                    int b = j + (y * offsetY);
                    map[a, b] = (maprisk[i, j] + offsetX + offsetY) % 9;
                    if (map[a, b] == 0)
                    {
                        map[a, b] = 9;
                    }
                }
            }
        }


        public void ResolveSecondPart()
        {

            int[,] newmap = new int[x * 5, y * 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    CopyBlock(newmap, i, j);
                }
            }
            x *= 5;
            y *= 5;
            maprisk = newmap;
            Do();
        }
    }
}
