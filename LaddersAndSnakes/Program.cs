﻿using System;

namespace LaddersAndSnakes
{
    class Program
    {
        // Build board
        private static void buildBoard()
        {
            
            int [,] board = new int [5,5];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write($"| {board[i,j]} |");
                }
                Console.WriteLine("");
                Console.WriteLine("-------------------------");
            }
        
        }

        // Roll dice (1 - 6)
        private static int dice()
        {
            Random rand = new Random();
            int dice = rand.Next(1, 7);
            return dice;
        }

        static void Main(string[] args)
        {
            buildBoard();
            
        }
    }
}
