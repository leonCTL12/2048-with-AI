namespace _2048.Game;

public interface IGameResultEvaluator
{
    public GameResult EvaluateGameResult(int[,] board);
}