using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
        static void Main( )
        {
            Random rGen = new Random( );

            // Initialize the game board in the solved puzzle state.
            // The zero value represents a hole.

            int[ , ] board =
            {
                {  0,  1,  2,  0 },
                {  3,  4,  5,  6 },
                {  7,  8,  9, 10 },
                {  0, 11, 12,  0 }
            };

            // Dimensions of the game board are extracted into variables for convenience.

            int rows = board.GetLength( 0 );
            int cols = board.GetLength( 1 );
            int length = board.Length;

            // This is the main game-playing loop.
            // Each iteration is either performing one random move (as part of scrambling)
            // or one move entered by the user.

            bool quit = false;
            int randomMoves = 0;
            while( ! quit )
            {
	
				// Set variables
                int move = 0;
                int r = 0;
                int c = 0;

                // Either generate a random move or display the game board and ask the user for a move.

                if( randomMoves > 0 )
                {
                    move = rGen.Next( 1, 13 );

                    randomMoves --;
                }
                else
                {
                    // Extract the game-board values into an array of displayed game-board strings.
                    // This is done so the strings can be of width 3 which makes the game-board
                    // display code below express very clearly.

                    string[ ] map = new string[ length ];
                    for( int i = 0; i < length; i ++ )
                    {
                        int value = board[ i / cols, i % cols ];
                        if( value == 0 ) map[ i ] = "   ";
                        else map[ i ] = $" {value:x} ";
                    }

                    // Display the game board.

                    Clear( );
                    WriteLine( );
                    WriteLine( " Welcome to the double-play game!" );
                    WriteLine( " Tiles slide in pairs by pushing towards a hole." );
                    WriteLine( " Scramble, then arrange back in order by sliding." );
                    WriteLine( );

                    // Use Unicode 'Box Drawing' range 2500–257f.

                    WriteLine( " ╔═══╦═══╦═══╦═══╗" );
                    WriteLine( " ║{0}║{1}║{2}║{3}║", map[  0 ], map[  1 ], map[  2 ], map[  3 ] );
                    WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                    WriteLine( " ║{0}║{1}║{2}║{3}║", map[  4 ], map[  5 ], map[  6 ], map[  7 ] );
                    WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                    WriteLine( " ║{0}║{1}║{2}║{3}║", map[  8 ], map[  9 ], map[ 10 ], map[ 11 ] );
                    WriteLine( " ╠═══╬═══╬═══╬═══╣" );
                    WriteLine( " ║{0}║{1}║{2}║{3}║", map[ 12 ], map[ 13 ], map[ 14 ], map[ 15 ] );
                    WriteLine( " ╚═══╩═══╩═══╩═══╝" );
                    WriteLine( );

                    // Interpret the user's desired move.

                    Write( " Tile to push (s to scramble, q to quit): " );
                    string response = ReadKey( intercept: true ).KeyChar.ToString( );
                    WriteLine( );

                    switch( response )
                    {
                        case "s": randomMoves = 100000; break;

                        case "1": move =  1; break;
                        case "2": move =  2; break;
                        case "3": move =  3; break;
                        case "4": move =  4; break;
                        case "5": move =  5; break;
                        case "6": move =  6; break;
                        case "7": move =  7; break;
                        case "8": move =  8; break;
                        case "9": move =  9; break;
                        case "a": move = 10; break;
                        case "b": move = 11; break;
                        case "c": move = 12; break;

                        case "q": quit = true; break;

                    }
				}
				
				// Goes through the array to find the entered number/letter
				for (int i = 0; i < 4; i++)
				{
					for (int j = 0; j < 4; j++)
					{
						if ( board[i,j] == move )
						{
							r = i;
							c = j;
							
						}
					}
				}
				
				// Set variables
				bool rightDir = true;
				bool leftDir = true;
				bool upDir = true;
				bool downDir = true;
				int temp = 0;
				
				// Checks all directions to see if a space two spaces from the 
				// entered number/letter is still in the array
				if (c - 2 < 0) leftDir = false;
									
				if (c + 2 > cols - 1) rightDir = false;
				
				if (r - 2 < 0) upDir = false;
				
				if (r + 2 > rows - 1) downDir = false;
				
				// Checks to see if there is not a neighbouring number/letter in the right direction 
				// or if there is not a free space two spaces in the right direction
				if (rightDir == true)
				{
					if (board[r, c + 1] == 0 || board[r, c + 2] != 0)
					{
						rightDir = false;
						
					// If there is, then the board changes to move the entered number/letter, 
					// the neighbouring number/letter, and the 0
					}else{
						temp = board[r, c];
						board[r, c] = board[r, c + 2];
						board[r, c + 2] = board[r, c + 1];
						board[r, c + 1] = temp;
					}
				}
				
				// Repeats the previous steps for each possible direction	
				if (leftDir == true)
				{
					if (board[r, c - 1] == 0 || board[r, c - 2] != 0)
					{
						leftDir = false;
					}else{
						temp = board[r, c];
						board[r, c] = board[r, c - 2];
						board[r, c - 2] = board[r, c - 1];
						board[r, c - 1] = temp;
					}
				}
				
				if (upDir == true)
				{
					if (board[r - 1, c] == 0 || board[r - 2, c] != 0)
					{
						upDir = false;
					}else{
						temp = board[r, c];
						board[r, c] = board[r - 2, c];
						board[r - 2, c] = board[r - 1, c];
						board[r - 1, c] = temp;
					}
				}
				
				if (downDir == true)
				{
					if (board[r + 1, c] == 0 || board[r + 2, c] != 0)
					{
						downDir = false;
					}else{
						temp = board[r, c];
						board[r, c] = board[r + 2, c];
						board[r + 2, c] = board[r + 1, c];
						board[r + 1, c] = temp;
					}
				}             
            }

			// Indicates that the game is over
            WriteLine( " Thanks for playing!" );
            WriteLine( );
        }
    }
}
