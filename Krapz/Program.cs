using System;

public class Program
{
    // Main method, entry point of the program
    static void Main()
    {
        Random random = new Random(); // Random object for dice rolls
        bool gameActive = true; // Control the game loop
        int coins = 100; // Initial number of coins

        // Main game loop
        while (gameActive && coins > 0)
        {
            Console.WriteLine($"You have {coins} coins."); // Display current number of coins
            Console.WriteLine("Welcome to the Craps Game!"); // Display welcome message

            int point = 0; // Initialize point value
            bool shooterWin = false; // Track if the shooter wins

            // Betting
            Console.Write("Enter the amount of coins to bet: ");
            int betAmount;
            // Validate and input the bet amount
            while (!int.TryParse(Console.ReadLine(), out betAmount) || betAmount <= 0 || betAmount > coins)
            {
                Console.Write("Invalid input or insufficient coins. Enter the amount of coins to bet: ");
            }

            // Select betting choice (with or against the shooter)
            Console.Write("Place your bet (1 for with the shooter, 2 for against the shooter): ");
            int betChoice;
            while (!int.TryParse(Console.ReadLine(), out betChoice) || (betChoice != 1 && betChoice != 2))
            {
                Console.Write("Invalid input. Place your bet again (1 for with the shooter, 2 for against the shooter): ");
            }

            Console.WriteLine("Press Enter to roll the dice...");
            Console.ReadLine();

            int roll = RollDice(random); // Roll the dice

            // Determine the outcome based on the roll
            switch (roll)
            {
                case 2:
                case 3:
                case 12:
                    Console.WriteLine($"Shooter rolls {roll}. Craps! Shooter loses.");
                    shooterWin = false;
                    break;
                case 7:
                case 11:
                    Console.WriteLine($"Shooter rolls {roll}. Natural! Shooter wins.");
                    shooterWin = true;
                    break;
                default:
                    Console.WriteLine($"Shooter rolls {roll}. Point is set to {roll}");
                    point = roll;
                    shooterWin = RollForPoint(random, point); // Roll for the point
                    break;
            }

            // Check the bet and update coins
            if ((shooterWin && betChoice == 1) || (!shooterWin && betChoice == 2))
            {
                Console.WriteLine("Congratulations! You win!");
                coins += betAmount;
            }
            else
            {
                Console.WriteLine("Sorry! You lose your bet.");
                coins -= betAmount;
            }

            // Prompt for playing again
            Console.Write("Do you want to play again? (y/n): ");
            string playAgain = Console.ReadLine().ToLower();
            gameActive = (playAgain == "y");
        }

        // Display game over message when player is out of coins
        Console.WriteLine("Game Over. You are out of coins.");
    }

    // Method to roll the dice and return the sum
    static int RollDice(Random random)
    {
        int die1 = random.Next(1, 7);
        int die2 = random.Next(1, 7);

        int sum = die1 + die2;

        Console.WriteLine($"Dice: {die1} + {die2} = {sum}");

        return sum;
    }

    // Method to roll for the point
    static bool RollForPoint(Random random, int point)
    {
        bool shooterWin = false;

        // Loop until the point is rolled again or a 7 is rolled
        while (true)
        {
            Console.WriteLine("Press Enter to roll the dice...");
            Console.ReadLine();

            int roll = RollDice(random);

            // Check if the point is rolled
            if (roll == point)
            {
                Console.WriteLine($"Shooter rolls {roll}. Shooter wins!");
                shooterWin = true;
                break;
            }
            // Check if a 7 is rolled
            else if (roll == 7)
            {
                Console.WriteLine($"Shooter rolls {roll}. Shooter loses!");
                break;
            }
        }

        return shooterWin;
    }
}
