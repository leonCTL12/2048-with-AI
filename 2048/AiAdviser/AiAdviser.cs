using _2048.Game;

namespace _2048.AiAdviser;

public class AiAdviser: IAiAdviser
{
    //Limit the move so that reduce the noise of the subsequent random moves
    private const int MovePerGame = 20;
    private const int GamePerDirection = 1000;
    private const int MaxTileWeight = 100;
    private const int EmptyCellWeight = 500;
    
    public Direction GetBestMove(IGame game)
    {
        var directions = new[] { Direction.Up, Direction.Left, Direction.Right, Direction.Down };
        var scores = new Dictionary<Direction, int>();

        foreach (var direction in directions)
        {
            scores[direction] = GetAverageScoreForDirection(game, direction);
        }

        return scores.OrderByDescending(kvp => kvp.Value).First().Key;
    }
    
    private int GetAverageScoreForDirection(IGame game, Direction direction)
    {
        int totalScore = 0;
        for (int i = 0; i < GamePerDirection; i++)
        {
            // Clone the game to avoid modifying the original game state
            var clonedGame = game.Clone();
            var score = GetScoreForGame(clonedGame, direction);
            var maxCellValue = clonedGame.MaxCellValue;
            var emptyCellCount = clonedGame.EmptyCellCount;
            var finalScoreForGame = maxCellValue * MaxTileWeight + emptyCellCount * EmptyCellWeight + score;
            totalScore+= finalScoreForGame;
        }
        return totalScore / GamePerDirection;
    }
    
    private int GetScoreForGame(IGame game, Direction direction)
    {
        // Simulate the game for a certain number of moves
        // and return the score.
        game.ProcessInput(direction);
        for (int i = 0; i < MovePerGame; i++)
        {
            direction = (Direction)Random.Shared.Next(0, 4);
            var result = game.ProcessInput(direction);
            if (result == GameResult.Win || result == GameResult.Lose)
            {
                break;
            }
        }
        return game.Score;
    }
    
}