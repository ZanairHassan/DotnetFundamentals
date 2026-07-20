using System;
using System.Collections.Generic;
using System.Text;

namespace ControlStatements
{
    public class NumberGuessingGame
    {
        public void GameAction()
        {
            Random randomNumber = new Random();
            char tryAgain = 'Y';

            while (tryAgain == 'Y')
            {
                int secretNumber = randomNumber.Next(1, 101);
                int guess = 0;
                int attempts = 0;

                Console.Clear();
                Console.WriteLine("******************* Number Guessing Game ***********************");
                Console.WriteLine("Guess a number between 1 and 100.");

                while (guess != secretNumber)
                {
                    Console.Write("\nEnter your guess:\t");

                    if (!int.TryParse(Console.ReadLine(), out guess))
                    {
                        Console.WriteLine("Please enter a valid number.");
                        continue;
                    }

                    attempts++;

                    if (guess > secretNumber)
                    {
                        Console.WriteLine("Too High!");
                    }
                    else if (guess < secretNumber)
                    {
                        Console.WriteLine("Too Low!");
                    }
                    else
                    {
                        Console.WriteLine("\nCongratulations!");
                        Console.WriteLine($"You guessed the number in {attempts} attempts.");
                    }
                }

                Console.Write("\nDo you want to play again? (Y/N): ");
                tryAgain = char.ToUpper(Convert.ToChar(Console.ReadLine()));
                switch (tryAgain)
                {
                    case 'Y':
                        Console.WriteLine("Starting a new game...");
                        break;

                    case 'N':
                        Console.WriteLine("Thank you for playing!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Exiting the game.");
                        tryAgain = 'N';
                        break;
                }
            }
        }
    }
}
