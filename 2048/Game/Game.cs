using _2048.Game.BoardProcessor;
using _2048.Game.GameResultEvaluator;
using _2048.Input;

namespace _2048.Game;

public class Game : IGame
{
    private readonly IBoardProcessor _boardProcessor;
    private readonly IGameResultEvaluator _gameResultEvaluator;
    public int Score { get; private set; }
    private int[,] _board;

    public Game(IBoardProcessor boardProcessor, IGameResultEvaluator gameResultEvaluator)
    {
        _boardProcessor = boardProcessor;
        _gameResultEvaluator = gameResultEvaluator;
    }

    public GameResult ProcessInput(InputCommand input)
    {
        var direction = InputCommandToDirection(input);
        var boardProcessResult = _boardProcessor.ExecuteMove(_board, direction);
        _board = boardProcessResult.Board;
        Score += boardProcessResult.ScoreProduced;
        _board = _boardProcessor.AddRandomCell(_board);
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
        Score = 0;
    }
    
    public void InitialiseGame(int[,] board, int score)
    {
        _board = board;
        Score = 0;
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