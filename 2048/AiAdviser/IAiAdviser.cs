using _2048.Game;

namespace _2048.AiAdviser;

public interface IAiAdviser
{
    public Direction GetBestMove(int[,] board);
}