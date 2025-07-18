using _2048.AiAdviser;
using _2048.Input;

namespace _2048.Game;

public class Game : IGame
{
    private readonly IAiAdviser _aiAdviser;
    private readonly IBoardProcessor _boardProcessor;
    private readonly IGameResultEvaluator _gameResultEvaluator;

    private int[,] _board;

    public Game(IAiAdviser aiAdviser, IBoardProcessor boardProcessor, IGameResultEvaluator gameResultEvaluator)
    {
        _aiAdviser = aiAdviser;
        _boardProcessor = boardProcessor;
        _gameResultEvaluator = gameResultEvaluator;
    }

    public GameResult ProcessInput(InputCommand input)
    {
        if (input == InputCommand.AiSuggestion)
        {
            var suggestion = _aiAdviser.GetBestMove(_board);
            Console.WriteLine($"AI suggests to move: {suggestion}");
            return GameResult.Ongoing;
        }

        var direction = InputCommandToDirection(input);
        _board = _boardProcessor.ExecuteMove(_board, direction);
        _board = _boardProcessor.AddRandomCell(_board);
        VisualiseGame();
        return _gameResultEvaluator.EvaluateGameResult(_board);
    }

    public void InitialiseGame()
    {
        _board = new int[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                bool place2 = Random.Shared.Next(0, 2) == 1;
                _board[i, j] = place2 ? 0 : 2;
            }
        }
    }

    public void VisualiseGame()
    {
        Console.WriteLine("Current Game Board: (* represents empty cell)");

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_board[i, j] == 0)
                {
                    Console.Write("* ");
                }
                else
                {
                    Console.Write(_board[i, j] + " ");
                }
            }

            Console.WriteLine();
        }
    }

    private Direction InputCommandToDirection(InputCommand command)
    {
        switch (command)
        {
            case InputCommand.Up:
                return Direction.Up;
            case InputCommand.Down:
                return Direction.Down;
            case InputCommand.Left:
                return Direction.Left;
            case InputCommand.Right:
                return Direction.Right;
            default:
                throw new Exception("Invalid input command for direction conversion.");
        }
    }
}