namespace _2048.Game.BoardProcessor;

public class BoardProcessResult
{
    public int[,] Board { get; }
    public int ScoreProduced { get; }
    
    public BoardProcessResult(int[,] board, int scoreProduced)
    {
        Board = board;
        ScoreProduced = scoreProduced;
    }
}