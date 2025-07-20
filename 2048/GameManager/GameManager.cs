using _2048.AiAdviser;
using _2048.Game;
using _2048.Input;

namespace _2048.GameManager;

public class GameManager :IGameManager
{
    private readonly IInputCommandRetriever _inputCommandRetriever;
    private readonly IAiAdviser _aiAdviser;
    private readonly IGame _game;

    public GameManager(IInputCommandRetriever inputCommandRetriever, IGame game, IAiAdviser aiAdviser)
    {
        _inputCommandRetriever = inputCommandRetriever;
        _game = game;
        _aiAdviser = aiAdviser;
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
            Console.WriteLine($"Current Score: {_game.Score}");
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

            if (inputCommand == InputCommand.AiSuggestion)
            {
                
                var suggestion = _aiAdviser.GetBestMove(_game);
                Console.WriteLine($"AI suggests to move: {suggestion}");
                continue;
            }

            var result = _game.ProcessInput(inputCommand);
            _game.VisualiseGame();
            
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