namespace _2048.Game.GameResultEvaluator;

public class GameResultEvaluator: IGameResultEvaluator
{
    public GameResult EvaluateGameResult(int[,] board)
    {
        //check for empty cells and 2048 cell
        bool containEmptyCells = false;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == 2048)
                {
                    return GameResult.Win;
                }
                if (board[i, j] == 0)
                {
                    containEmptyCells = true;
                }
            }
        }
        
        if (containEmptyCells)
        {
            return GameResult.Ongoing;
        }
        
        //Check for adjacent equal cells
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (i < board.GetLength(0) - 1 && board[i, j] == board[i + 1, j]) return GameResult.Ongoing;
                if (j < board.GetLength(1) - 1 && board[i, j] == board[i, j + 1]) return GameResult.Ongoing;
            }
        }

        return GameResult.Lose;
    }
 }