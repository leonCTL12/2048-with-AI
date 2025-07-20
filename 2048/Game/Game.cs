using _2048.Game.BoardProcessor;
using _2048.Game.GameResultEvaluator;

namespace _2048.Game;

public class Game : IGame
{
    private readonly IBoardProcessor _boardProcessor;
    private readonly IGameResultEvaluator _gameResultEvaluator;
    public int Score { get; private set; }
    
    //Count when accessed, it will be cleaner and more efficient
    public int MaxCellValue => _boardProcessor.GetMaxCellValue(_board);
    public int EmptyCellCount => _boardProcessor.CountEmptyCells(_board);
    public IGame Clone()
    {
        return new Game(_boardProcessor, _gameResultEvaluator, (int[,])_board.Clone(), Score);
    }

    private Game(IBoardProcessor boardProcessor, IGameResultEvaluator gameResultEvaluator, int[,] board, int score)
    {
        _boardProcessor = boardProcessor;
        _gameResultEvaluator = gameResultEvaluator;
        _board = board;
        Score = score;
    }

    private int[,] _board;

    public Game(IBoardProcessor boardProcessor, IGameResultEvaluator gameResultEvaluator)
    {
        _boardProcessor = boardProcessor;
        _gameResultEvaluator = gameResultEvaluator;
    }



    public GameResult ProcessInput(Direction direction)
    {
        var boardProcessResult = _boardProcessor.ExecuteMove(_board, direction);
        _board = boardProcessResult.Board;
        var scoreProduced = boardProcessResult.ScoreProduced;
        Score += scoreProduced;
        var result = _gameResultEvaluator.EvaluateGameResult(_board);
        if (result != GameResult.Ongoing)
            return result;
        _board = _boardProcessor.AddRandomCell(_board);
        
        return GameResult.Ongoing;
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


}