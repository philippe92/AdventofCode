using System;
using System.IO;

namespace AdventOfCode
{
    class Jour4_2021 : IJour
    {
        int[] tirage;
        int[,,] cartes;
        int nb_cartes;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            var split_str = lines[0].Split(',');
            tirage = new int[split_str.Length];
            for(int i = 0; i < split_str.Length; i++)
            {
                tirage[i] = int.Parse(split_str[i]);
            }

            nb_cartes = (lines.Length - 1) / 6;
            cartes = new int[nb_cartes, 5, 5];
            int cur_line = 2;
            for (int i=0; i< nb_cartes; i++)
            {
                for (int j=0; j<5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        var str = lines[cur_line].Substring(k * 3, 2);
                        cartes[i,j,k] = int.Parse(str);
                    }
                    cur_line++;
                }
                cur_line++; // pour enlever les lignes blanches
            }
        }

        public void ResolveFirstPart()
        {
            var marquage = new bool[nb_cartes, 5, 5];

            foreach (var nb in tirage)
            {
                // Enleve le numero sur toutes les cartes

                for (int i = 0; i < nb_cartes; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (cartes[i, j, k] == nb)
                            {
                                marquage[i, j, k] = true;
                            }
                        }
                    }
                }

                //Regarde si une carte est gagnante
                for (int i = 0; i < nb_cartes; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (marquage[i, j, 0] && marquage[i, j, 1] && marquage[i, j, 2] && marquage[i, j, 3] && marquage[i, j, 4]
                            || marquage[i, 0, j] && marquage[i, 1, j] && marquage[i, 2, j] && marquage[i, 3, j] && marquage[i, 4, j])
                        {
                            Console.WriteLine($"Carte {i} gagnante sur le tirage {nb}!!!");
                            int somme = 0;
                            for (int a = 0; a < 5; a++)
                            {
                                for (int b = 0; b < 5; b++)
                                {
                                    if (!marquage[i, a, b])
                                    {
                                        somme += cartes[i, a, b];
                                    }
                                }
                            }
                            Console.WriteLine($"Somme : {somme}");
                            Console.WriteLine($"Resultat : { somme * nb}");
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("Aucun gagnant :( :( : (");
        }

        public void ResolveSecondPart()
        {
            var marquage = new bool[nb_cartes, 5, 5];
            bool[] cartes_gagnantes = new bool[nb_cartes];

            foreach (var nb in tirage)
            {
                // Enleve le numero sur toutes les cartes

                for (int i = 0; i < nb_cartes; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (cartes[i, j, k] == nb)
                            {
                                marquage[i, j, k] = true;
                            }
                        }
                    }
                }

                //Regarde si une carte est gagnante
                for (int i = 0; i < nb_cartes; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (marquage[i, j, 0] && marquage[i, j, 1] && marquage[i, j, 2] && marquage[i, j, 3] && marquage[i, j, 4]
                            || marquage[i, 0, j] && marquage[i, 1, j] && marquage[i, 2, j] && marquage[i, 3, j] && marquage[i, 4, j])
                        {
                            cartes_gagnantes[i] = true;

                            // Regarde si on est la derniere carte gagnante
                            int nb_gagnantes = 0;
                            for (int x = 0; x < nb_cartes; x++)
                            {
                                if (cartes_gagnantes[x])
                                {
                                    nb_gagnantes++;
                                }
                            }

                            if (nb_gagnantes == nb_cartes)
                            {
                                Console.WriteLine($"Carte {i} est la derniere gagnante sur le tirage {nb}");
                                int somme = 0;
                                for (int a = 0; a < 5; a++)
                                {
                                    for (int b = 0; b < 5; b++)
                                    {
                                        if (!marquage[i, a, b])
                                        {
                                            somme += cartes[i, a, b];
                                        }
                                    }
                                }
                                Console.WriteLine($"Somme : {somme}");
                                Console.WriteLine($"Resultat : { somme * nb}");
                                return;
                            }
                        }
                    }
                }

            

            }
            Console.WriteLine("Aucun gagnant :( :( : (");
        }
    }
}
