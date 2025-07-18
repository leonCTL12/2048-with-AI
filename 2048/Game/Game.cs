namespace _2048.Game;

public class Game :IGame
{
    public void StartGame()
    {
        //Game Loop
        while (true)
        {
            Console.WriteLine("Game is running... Press 'q' to quit.");
            var input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Q)
            {
                Console.WriteLine("Exiting game...");
                break;
            }
        }
    }
}