namespace _2048_UnitTest;

using _2048.Game.GameResultEvaluator;
using _2048.Game;
using NUnit.Framework;

[TestFixture]
public class GameResultEvaluatorTest
{
    [Test]
    public void EvaluateGameResult_WhenBoardHas2048_ReturnsWin()
    {
        int[,] board = new[,] {
            {4, 0, 0, 2},
            {2048, 0, 0, 0},
            {4, 2, 0, 0},
            {4, 0, 0, 0}
        };
        var evaluator = new GameResultEvaluator();
        var result = evaluator.EvaluateGameResult(board);
        Assert.That(result, Is.EqualTo(GameResult.Win));
    }

    [Test]
    public void EvaluateGameResult_WhenBoardHasMultiple2048_ReturnsWin()
    {
        int[,] board = new[,] {
            {2048, 2048, 4, 8},
            {16, 32, 64, 128},
            {256, 512, 1024, 2},
            {4, 8, 16, 32}
        };
        var evaluator = new GameResultEvaluator();
        var result = evaluator.EvaluateGameResult(board);
        Assert.That(result, Is.EqualTo(GameResult.Win));
    }

    [Test]
    public void EvaluateGameResult_WhenBoardHasEmptyTilesAndNo2048_ReturnsOngoing()
    {
        int[,] board = new[,] {
            {2, 0, 4, 8},
            {16, 32, 64, 128},
            {256, 512, 1024, 2},
            {4, 8, 16, 32}
        };
        var evaluator = new GameResultEvaluator();
        var result = evaluator.EvaluateGameResult(board);
        Assert.That(result, Is.EqualTo(GameResult.Ongoing));
    }

    [Test]
    public void EvaluateGameResult_WhenBoardIsFullWithAdjacentEqualsAndNo2048_ReturnsOngoing()
    {
        int[,] board = new[,] {
            {2, 2, 4, 8},
            {16, 32, 64, 128},
            {256, 512, 1024, 2},
            {4, 8, 16, 32}
        };
        var evaluator = new GameResultEvaluator();
        var result = evaluator.EvaluateGameResult(board);
        Assert.That(result, Is.EqualTo(GameResult.Ongoing));
    }

    [Test]
    public void EvaluateGameResult_WhenBoardHasAdjacentEqualsInColumnAndNo2048_ReturnsOngoing()
    {
        int[,] board = new[,] {
            {2, 4, 8, 16},
            {2, 64, 128, 256},
            {512, 1024, 2, 4},
            {8, 16, 32, 64}
        };
        var evaluator = new GameResultEvaluator();
        var result = evaluator.EvaluateGameResult(board);
        Assert.That(result, Is.EqualTo(GameResult.Ongoing));
    }

    [Test]
    public void EvaluateGameResult_WhenBoardIsFullWithNoAdjacentEqualsAndNo2048_ReturnsLose()
    {
        int[,] board = new[,] {
            {2, 4, 2, 4},
            {4, 2, 4, 2},
            {2, 4, 2, 4},
            {4, 2, 4, 2}
        };
        var evaluator = new GameResultEvaluator();
        var result = evaluator.EvaluateGameResult(board);
        Assert.That(result, Is.EqualTo(GameResult.Lose));
    }

    [Test]
    public void EvaluateGameResult_WhenBoardIsEmpty_ReturnsOngoing()
    {
        int[,] board = new int[4, 4]; 
        var evaluator = new GameResultEvaluator();
        var result = evaluator.EvaluateGameResult(board);
        Assert.That(result, Is.EqualTo(GameResult.Ongoing));
    }

    [Test]
    public void EvaluateGameResult_WhenBoardIsFullWithNoMovesDifferentPattern_ReturnsLose()
    {
        int[,] board = new[,] {
            {2, 4, 8, 16},
            {32, 64, 128, 256},
            {512, 1024, 2, 4},
            {8, 16, 32, 64}
        };
        var evaluator = new GameResultEvaluator();
        var result = evaluator.EvaluateGameResult(board);
        Assert.That(result, Is.EqualTo(GameResult.Lose));
    }
}