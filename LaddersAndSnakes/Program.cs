using System;

namespace LaddersAndSnakes
{
    class Program
    {

        private static int[] boostMaker(int amount, int min, int max, Random rand)
        {

            int [] array = new int [amount];


            for(int i = 0; i< amount; i++)
            {
                array[i] = rand.Next(min,max);               
            }

            return array;
        }

        
        // Builds the board in a single array (board)
        /// <summary>
        /// Board creation
        /// </summary>
        /// <returns>single array with 25 int's</returns>
        private static int[] buildBoard(Random rand)                // feito por Bruno e Fabio (para o relatorio)
        {
            int cobraPosition = rand.Next(10,24);
            int amountBoost = rand.Next(0,3);
            int amountUTurn = rand.Next(0,3);
            int [] boostPosition = new int [amountBoost];
            int [] uTurnPosition = new int [amountUTurn];


            if (amountBoost != 0)
            {
                boostPosition = boostMaker(amountBoost,1, 20, rand);
            }
            if (amountUTurn != 0)
            {
                uTurnPosition = boostMaker(amountUTurn,5, 24, rand);
            }

            

            
            

            int [] board = new int[25];
            for(int i = 0; i < board.Length; i++)
            {
                if (cobraPosition == i)
                {
                    board[i] = 4;
                }

                else
                {
                    board[i] = 0;

                }  

                if (amountBoost != 0)
                {
                    for (int j = 0; j<amountBoost ; j++)
                    {
                        if (boostPosition[j]== i)
                        {
                            board[i] = 5;
                        }
                        
                    }
                }
                if (amountUTurn != 0)
                {
                    for (int j = 0; j<amountUTurn ; j++)
                    {
                        if (uTurnPosition[j]== i)
                        {
                            board[i] = 6;
                        }
                        
                    }
                }                     
            }

            return board;

        }

        //Transforms the single array (board) and prints it in the directions wanted
        /// <summary>
        /// print board with player and without player
        /// </summary>
        /// <param name="board"> Receive board created previously</param>
        private static void printBoard(int[] board)                // feito por Bruno (para o relatorio)
        {
            
            

            //val is 25 - 1
            int val = board.Length-1;
            int invert;

            Console.WriteLine("\n-----------------------------------");  
            //while cycle that checks if the array has ended       
            while (val > -1){
                if (val % 2 == 0){   
                    //invert is -4 when the line starting number is pair                 
                    invert = -4;
                }
                else{
                    //otherwise stays at 0
                    invert = 0;
                }
                //for cycle that starts in the last Index and goes through the array 5 by 5 elements
                for (int i = val; i > val-5; i--)
                {   
                     

                    //if it is a normal tile prints empty
                    if(board[i+invert] == 0)
                    {
                        //Console.Write($"|{i+invert,2:d}    |");     //so you can see the numbers
                        Console.Write($"| {"",3:d} |");               //Fazer um if para mostrar Snakes and Ladders
                    }
                    //if it is a "Cobra" tile prints "C"
                    else if (board[i+invert] == 4)
                    {
                        Console.Write($"| {"C",2:d}  |");
                    }
                    else if (board[i+invert] == 5)
                    {
                        Console.Write($"| {"B",2:d}  |");
                    }
                    else if (board[i+invert] == 6)
                    {
                        Console.Write($"| {"U",2:d}  |");
                    }
                    //if it is a player tile prints player
                    else
                    {
                        Console.Write($"| {"P"+board[i+invert],2:d}  |"); 
                    }
                    
                    //changes the value of invert according to the array position    
                    if (val % 2 == 0)
                    {
                        invert += 2;
                    }
                }
                Console.WriteLine("\n-----------------------------------");
                val-=5;
            }
            
        }


        
        /// <summary>
        /// Receive which player is playing and move the number of tiles given by the die
        /// </summary>
        /// <param name="player">Which player</param>
        /// <param name="moveByDie">Number given by the die</param>
        /// <param name="board">Board created by method buildBoard</param>
        private static Boolean movePlayerByDie(int player, int opponent, int moveByDie, int[] board)
        {   
            //gets the position of the player on the board
            int position = Array.IndexOf(board, player);
            int positionOpponent = Array.IndexOf(board, opponent);
            bool normalTile = default;

            int newPos = position+moveByDie;

            while(normalTile != true)
            {
                //if the player is on the board moves normally
                if (position  != -1)       
                {       
                    
                    if (newPos > 24) 
                    {
                        newPos = 24 - (newPos - 24);
                    }                

                    if (newPos == 24)
                    {
                        Console.WriteLine($"Congratulations!! Player {player} WON");
                        return true;
                    }

                    

                                
                    board[position] = 0;               
                    if (board[newPos] == opponent)
                    {
                        positionOpponent -= 1;
                        board[positionOpponent] = opponent;
                        

                        Console.WriteLine($"Player {opponent} was there and was moved back 1 position; ");

                    }
                    if (newPos == 0)
                    {
                        normalTile = true;
                    }

                    else if (board[newPos] == 4)
                    {
                        newPos = 0;
                        Console.WriteLine($"Player{player} was in a Cobra Tile! Moved back to the start of the board; ");

                    }

                    else if (board[newPos] == 5)
                    {
                        newPos += 2;
                        Console.WriteLine($"Player{player} was in a Boost Tile! Advanced 2 positions; ");
                    }

                    else if (board[newPos] == 6)
                    {
                        newPos -= 2;
                        Console.WriteLine($"Player{player} was in a U-Turn Tile! Moved back 2 positions; ");
                    }

                    board[newPos] = player;
                    

                }


                //if it isn't subtract 1 to the move and gets on the board correctly
                else
                {                                          //se o jogador estiver fora do tabuleiro

                    Console.WriteLine(position);
                    if (board[newPos] == opponent)
                    {
                        if (moveByDie==1)
                        {
                            board[positionOpponent]=0;
                        }
                        else
                        {
                            positionOpponent -= 1;
                            board[positionOpponent] = opponent;
                        }

                       Console.WriteLine($"Player {opponent} was there and was moved back 1 position; ");
                    }

                    if (newPos == 0)
                    {
                        normalTile = true;
                    }

                    else if (board[newPos] == 5)
                    {
                        newPos += 2;
                        Console.WriteLine($"Player{player} was in a Boost Tile! Advanced 2 positions; ");
                    }

                    else if (board[newPos] == 6)
                    {
                        newPos += 2;
                        Console.WriteLine($"Player{player} was in a U-Turn Tile! Moved back 2 positions; ");
                    }
               
                    board[newPos] = player;                                                   
                }
                return false;
                               
            }
            return false;       
        }

        // Roll dice (1 - 6)
        /// <summary>
        ///  Number generator (die) 1-6
        /// </summary>
        /// <returns>random number between 1-6</returns>
        private static int dice(Random rand)                   /// feito por Fabio (para o relatorio)
        {           
            int dice = rand.Next(1, 7);
            return dice;
        }


        //makes a player roll the die to make a move
        /// <summary>
        /// Save the generated number by method dice
        /// </summary>
        /// <param name="player">which player</param>
        /// <returns>Saved number by method die</returns>
        private static int playerRoll(int player, Random rand)   /// feito por Bruno (para o relatorio)
        {
            string input;
            bool played = false;
            int roll = dice(rand);
            
            
            do
            {
                //Asks the player to roll the die
            Console.WriteLine($"Player{player}, it's your turn! Press /R to roll the die: ");
            //Reads the input of the player        
            input = Console.ReadLine();  

            //if the input is correct (R or r) rolls the die
            if (input == "r" || input == "R")                                                   
            {
                //prints the number rolled
                Console.WriteLine($"\nPlayer{player} roll: {roll}; ");
                break;
                
            }
            //if the input is incorrect asks to press R again with a Error Message
            else
            {
                Console.WriteLine("Invalid input!!");
                Console.WriteLine("Please use /R to roll the die ");
            }
            
            //changes boolean to true so it can stop the cycle
            played = true;
            

            }while (played == true);

            //returns the number rolled in the die
            return roll;
            
        }


    
   
        static void Main(string[] args)
        {
            Random rand = new Random();
            bool winner = false;                    /// feito por Bruno (para o relatorio)
            int[] board = buildBoard(rand);

            

            //Creates player 1
            int player1 =  1;  
            //Creates player 2   
            int player2 =  2;  

            int moveByDie;
             


            
            printBoard(board);
            //while there isn't a winner runs 
            while(winner != true)                                    /// feito por Bruno (para o relatorio)
            {
                

                
                
                moveByDie = playerRoll(player1, rand);

                winner = movePlayerByDie(player1, player2, moveByDie, board);
                printBoard(board);

                if (winner == true)
                {
                    break;
                }
                
                
                

                



                moveByDie = playerRoll(player2, rand);

                winner = movePlayerByDie(player2, player1, moveByDie, board);
                printBoard(board);
                
                

            }
            
            
        }
    }
}