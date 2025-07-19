namespace _2048.Game.GameResultEvaluator;

public class GameResultEvaluator: IGameResultEvaluator
{
    public GameResult EvaluateGameResult(int[,] board)
    {
        //TODO: Implement the logic to evaluate the game result based on the board state.
        return GameResult.Ongoing;
    }
}