using _2048.Game;
using _2048.Input;

namespace _2048.GameManager;

public class GameManager :IGameManager
{
    private readonly IInputCommandRetriever _inputCommandRetriever;
    private readonly IGame _game;

    public GameManager(IInputCommandRetriever inputCommandRetriever, IGame game)
    {
        _inputCommandRetriever = inputCommandRetriever;
        _game = game;
    }

    public void StartGame()
    {
        Console.WriteLine("Game is running...");
        
        _game.InitialiseGame();
        _game.VisualiseGame();
         
        //Game Loop
        while (true)
        {
            Console.WriteLine("Waiting for input command... (Use arrow keys to move, 'q' to quit, 'a' for AI suggestion)");
            
            var inputCommand = _inputCommandRetriever.GetCommand(); 
            
            if (inputCommand == InputCommand.Invalid)
            {
                Console.WriteLine("Invalid command.");
                continue;
            }
            if (inputCommand == InputCommand.Quit)
            {
                Console.WriteLine("Exiting game...");
                break;
            }

            var result = _game.ProcessInput(inputCommand);
            
            if (result == GameResult.Win)
            {
                Console.WriteLine("Congratulations! You've won the game!");
                break;
            }

            if (result == GameResult.Lose)
            {
                Console.WriteLine("Game Over! You've lost the game.");
                break;
            }
        }
    }
}