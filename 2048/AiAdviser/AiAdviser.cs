using _2048.Game;

namespace _2048.AiAdviser;

public class AiAdviser: IAiAdviser
{
    public Direction GetBestMove(IGame game)
    {
        // This is a placeholder implementation.
        //TODO: Implement a proper AI algorithm to determine the best move.
        return Direction.Down;
    }
}