namespace _2048.Game.GameResultEvaluator;

public interface IGameResultEvaluator
{
    public GameResult EvaluateGameResult(int[,] board);
}