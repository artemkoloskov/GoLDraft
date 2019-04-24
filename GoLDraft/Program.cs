using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoLDraft
{
    internal class Program
    {
        private static readonly Random gen = new Random();
        private const int CREATURE_DIMENSION = 50;
        private const string LIVE_CELL = "OO";
        private const string DEAD_CELL = "  ";

        private static void Main(string[] args)
        {
            bool[,] str = new bool[CREATURE_DIMENSION, CREATURE_DIMENSION];

            Console.WriteLine("Push any button to start growth.");
            Console.ReadKey();

            for (int i = 0; i <= str.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= str.GetUpperBound(1); j++)
                {
                    str[i, j] = RandomBool();
                }
            }

            for (int i = 0; i < 10000; i++)
            {
                Console.Clear();
                Console.WriteLine("i: " + i);

                PrintArray(str);

                EvolveArray(str);

                Thread.Sleep(20);
            }

            Console.ReadKey();
        }

        private static bool RandomBool()
        {
            return gen.Next(100) < 50;
        }

        private static void PrintArray(bool[,] array)
        {
            for (int i = 0; i <= array.GetUpperBound(0); i++)
            {
                StringBuilder str = new StringBuilder();

                for (int j = 0; j <= array.GetUpperBound(1); j++)
                {
                    if (array[i, j])
                    {
                        str.Append(LIVE_CELL);
                    }
                    else
                    {
                        str.Append(DEAD_CELL);
                    }
                }

                Console.WriteLine(str);
            }
        }

        private static void EvolveArray(bool[,] array)
        {
            bool[,] oldState = new bool[CREATURE_DIMENSION, CREATURE_DIMENSION];

            CopyArray(array, oldState);

            for (int i = 0; i <= array.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= array.GetUpperBound(1); j++)
                {
                    array[i, j] = CheckNewState(oldState, i, j);
                }
            }
        }

        private static void CopyArray(bool[,] from, bool[,] to)
        {
            if (from.GetUpperBound(0) == to.GetUpperBound(0) && from.GetUpperBound(1) == to.GetUpperBound(1))
            {
                for (int i = 0; i <= from.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= from.GetUpperBound(1); j++)
                    {
                        if (from[i, j])
                        {
                            to[i, j] = true;
                        }
                        else
                        {
                            to[i, j] = false;
                        }
                    }
                }
            }
        }

        private static bool CheckNewState(bool[,] oldState, int i, int j)
        {
            int numOfAliveCells = 0;

            for (int x = i - 1; x <= i + 1; x++)
            {
                for (int y = j - 1; y <= j + 1; y++)
                {
                    int realI;
                    int realJ;

                    if (x < 0)
                    {
                        realI = oldState.GetUpperBound(0);
                    }
                    else if (x > oldState.GetUpperBound(0))
                    {
                        realI = 0;
                    }
                    else
                    {
                        realI = x;
                    }

                    if (y < 0)
                    {
                        realJ = oldState.GetUpperBound(0);
                    }
                    else if (y > oldState.GetUpperBound(0))
                    {
                        realJ = 0;
                    }
                    else
                    {
                        realJ = y;
                    }

                    if (realI != i || realJ != j)
                    {
                        if (oldState[realI, realJ])
                        {
                            numOfAliveCells++;
                        }
                    }

                }
            }

            if (!oldState[i, j])
            {
                if (numOfAliveCells == 3)
                {
                    return true;
                }
                return false;
            }

            if (numOfAliveCells == 2 || numOfAliveCells == 3)
            {
                return true;
            }

            return false;
        }
    }
}
