using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace GoLDraft
{
    internal class Program
    {
        private static readonly Random gen = new Random();
        private const int CREATURE_DIMENSION = 60;
        private const string LIVE_CELL = "OO";
        private const string DEAD_CELL = "  ";

        private static void Main(string[] args)
        {
            bool[,] world = new bool[CREATURE_DIMENSION, CREATURE_DIMENSION];

            Console.WriteLine("Push any button to start growth.");
            Console.ReadKey();

            for (int i = 0; i <= world.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= world.GetUpperBound(1); j++)
                {
                    world[i, j] = RandomBool();
                }
            }

            for (int i = 0; i < 10000; i++)
            {
                Console.Clear();
                Console.WriteLine("i: " + i);

                Print(world);

                Evolve(world);

                WaitBeforeNextEvolution(5);
            }

            Console.ReadKey();
        }

        private static void WaitBeforeNextEvolution(int time)
        {
            Thread.Sleep(time);
        }

        private static bool RandomBool()
        {
            return gen.Next(100) < 50;
        }

        private static void Print(bool[,] world)
        {
            for (int i = 0; i <= world.GetUpperBound(0); i++)
            {
                StringBuilder worldLine = new StringBuilder();

                for (int j = 0; j <= world.GetUpperBound(1); j++)
                {
                    if (world[i, j])
                    {
                        worldLine.Append(LIVE_CELL);
                    }
                    else
                    {
                        worldLine.Append(DEAD_CELL);
                    }
                }

                Console.WriteLine(worldLine);
            }
        }

        private static void Evolve(bool[,] world)
        {
            bool[,] oldWorldState = new bool[CREATURE_DIMENSION, CREATURE_DIMENSION];

            Copy(world, oldWorldState);

            for (int i = 0; i <= world.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= world.GetUpperBound(1); j++)
                {
                    world[i, j] = GetNewWorldState(oldWorldState, i, j);
                }
            }
        }

        private static void Copy(bool[,] fromWorld, bool[,] toWorld)
        {
            if (fromWorld.GetUpperBound(0) == toWorld.GetUpperBound(0) && fromWorld.GetUpperBound(1) == toWorld.GetUpperBound(1))
            {
                for (int i = 0; i <= fromWorld.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= fromWorld.GetUpperBound(1); j++)
                    {
                        if (fromWorld[i, j])
                        {
                            toWorld[i, j] = true;
                        }
                        else
                        {
                            toWorld[i, j] = false;
                        }
                    }
                }
            }
        }

        private static bool GetNewWorldState(bool[,] oldWorldState, int i, int j)
        {
            int numOfAliveCells = 0;

            // We will be checking cells left and right, each by one, and up and down,
            // each by one, of current [i, j] cell
            for (int x = i - 1; x <= i + 1; x++)
            {
                for (int y = j - 1; y <= j + 1; y++)
                {
                    int realI;
                    int realJ;

                    // To "enclose" a world on itself we check if x goes out
                    // of world boundaries and swictch it to the opposite side of the world
                    if (x < 0)
                    {
                        realI = oldWorldState.GetUpperBound(0);
                    }
                    else if (x > oldWorldState.GetUpperBound(0))
                    {
                        realI = 0;
                    }
                    else
                    {
                        realI = x;
                    }

                    // To "enclose" a world on itself we check if y goes out
                    // of world boundaries and swictch it to the opposite side of the world
                    if (y < 0)
                    {
                        realJ = oldWorldState.GetUpperBound(0);
                    }
                    else if (y > oldWorldState.GetUpperBound(0))
                    {
                        realJ = 0;
                    }
                    else
                    {
                        realJ = y;
                    }

                    if (realI != i || realJ != j)
                    {
                        if (oldWorldState[realI, realJ])
                        {
                            numOfAliveCells++;
                        }
                    }
                }
            }

            // Dead cell becomes alive if it has 3 alive neighbores
            if (!oldWorldState[i, j])
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
