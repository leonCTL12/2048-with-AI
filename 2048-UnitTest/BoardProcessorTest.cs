using _2048.Game;
using _2048.Game.BoardProcessor;
using NUnit.Framework;

namespace _2048_UnitTest;

[TestFixture]
public class BoardProcessorTests
{
    private BoardProcessor _boardProcessor;

    [SetUp]
    public void SetUp()
    {
        _boardProcessor = new BoardProcessor();
    }

    // Helper method to execute the move and assert the result
    private void ExecuteAndAssert(int[,] board, Direction direction, int[,] expected, int expectedScore, string testName)
    {
        BoardProcessResult result = _boardProcessor.ExecuteMove(board, direction);
        Assert.That(result.Board, Is.EqualTo(expected).AsCollection, $"{testName} failed: Board mismatch");
        Assert.That(result.ScoreProduced, Is.EqualTo(expectedScore), $"{testName} failed: Score mismatch");
    }

    #region Merge Left Tests

    [Test]
    public void TestMergeLeft_Simple()
    {
        int[,] board = new[,]
        {
            { 2, 2, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 4, "Merge Left Simple");
    }

    [Test]
    public void TestMergeLeft_MultipleMerges()
    {
        int[,] board = new[,]
        {
            { 2, 2, 4, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 8, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 12, "Merge Left Multiple Merges"); // 4 + 8 = 12
    }

    [Test]
    public void TestMergeLeft_NoMerges()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2, 4, 8, 16 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 0, "Merge Left No Merges");
    }

    [Test]
    public void TestMergeLeft_MovementWithoutMerging()
    {
        int[,] board = new[,]
        {
            { 0, 2, 0, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2, 4, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 0, "Merge Left Movement Without Merging");
    }

    [Test]
    public void TestMergeLeft_MixedTiles()
    {
        int[,] board = new[,]
        {
            { 2, 2, 4, 0 },
            { 4, 0, 4, 2 },
            { 8, 8, 0, 0 },
            { 0, 2, 0, 2 }
        };
        int[,] expected = new[,]
        {
            { 4, 4, 0, 0 },
            { 8, 2, 0, 0 },
            { 16, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 32, "Merge Left Mixed Tiles"); // 4 + 8 + 16 + 4 = 32
    }

    [Test]
    public void TestMergeLeft_EmptyBoard()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 0, "Merge Left Empty Board");
    }

    [Test]
    public void TestMergeLeft_FullBoardNoMerges()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        int[,] expected = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 0, "Merge Left Full Board No Merges");
    }

    [Test]
    public void TestMergeLeft_FullBoardAllSame()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 }
        };
        int[,] expected = new[,]
        {
            { 4, 4, 0, 0 },
            { 4, 4, 0, 0 },
            { 4, 4, 0, 0 },
            { 4, 4, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 32, "Merge Left Full Board All Same"); // 4*8=32
    }

    [Test]
    public void TestMergeLeft_MergedTilesNoReMerge()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 4, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 8, "Merge Left Merged Tiles No Re-Merge"); // 4 + 4 = 8
    }

    [Test]
    public void TestMergeLeft_ThreeIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 2, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 4, "Merge Left Three Identical Tiles"); // 4
    }

    [Test]
    public void TestMergeLeft_FourIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 4, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 8, "Merge Left Four Identical Tiles"); // 4 + 4 = 8
    }

    [Test]
    public void TestMergeLeft_InterruptedSequence()
    {
        int[,] board = new[,]
        {
            { 2, 4, 2, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2, 4, 2, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 0, "Merge Left Interrupted Sequence");
    }

    [Test]
    public void TestMergeLeft_SeparatedIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 4, "Merge Left Separated Identical Tiles"); // 4
    }

    [Test]
    public void TestMergeLeft_LargeValues()
    {
        int[,] board = new[,]
        {
            { 1024, 1024, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2048, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 2048, "Merge Left Large Values"); // 2048
    }

    [Test]
    public void TestMergeLeft_MinimalBoard()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Left, expected, 0, "Merge Left Minimal Board");
    }

    #endregion

    #region Merge Right Tests

    [Test]
    public void TestMergeRight_Simple()
    {
        int[,] board = new[,]
        {
            { 0, 0, 2, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 4, "Merge Right Simple"); // 4
    }

    [Test]
    public void TestMergeRight_MultipleMerges()
    {
        int[,] board = new[,]
        {
            { 2, 2, 4, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 4, 8 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 12, "Merge Right Multiple Merges"); // 4 + 8 = 12
    }

    [Test]
    public void TestMergeRight_NoMerges()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 2, 4, 8 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 0, "Merge Right No Merges");
    }

    [Test]
    public void TestMergeRight_MovementWithoutMerging()
    {
        int[,] board = new[,]
        {
            { 2, 0, 4, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 2, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 0, "Merge Right Movement Without Merging");
    }

    [Test]
    public void TestMergeRight_MixedTiles()
    {
        int[,] board = new[,]
        {
            { 0, 2, 2, 4 },
            { 4, 0, 4, 2 },
            { 8, 8, 0, 0 },
            { 2, 0, 2, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 4, 4 },
            { 0, 0, 8, 2 },
            { 0, 0, 0, 16 },
            { 0, 0, 0, 4 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 32, "Merge Right Mixed Tiles"); // 4 + 8 + 16 + 4 = 32
    }

    [Test]
    public void TestMergeRight_EmptyBoard()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 0, "Merge Right Empty Board");
    }

    [Test]
    public void TestMergeRight_FullBoardNoMerges()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        int[,] expected = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 0, "Merge Right Full Board No Merges");
    }

    [Test]
    public void TestMergeRight_FullBoardAllSame()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 4, 4 },
            { 0, 0, 4, 4 },
            { 0, 0, 4, 4 },
            { 0, 0, 4, 4 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 32, "Merge Right Full Board All Same"); // 4 * 8 = 32
    }

    [Test]
    public void TestMergeRight_MergedTilesNoReMerge()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 4, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 8, "Merge Right Merged Tiles No Re-Merge"); // 4 + 4 = 8
    }

    [Test]
    public void TestMergeRight_ThreeIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 0, 2, 2, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 2, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 4, "Merge Right Three Identical Tiles"); // 4
    }

    [Test]
    public void TestMergeRight_FourIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 4, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 8, "Merge Right Four Identical Tiles"); // 4 + 4 = 8
    }

    [Test]
    public void TestMergeRight_InterruptedSequence()
    {
        int[,] board = new[,]
        {
            { 0, 2, 4, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 2, 4, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 0, "Merge Right Interrupted Sequence");
    }

    [Test]
    public void TestMergeRight_SeparatedIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 4, "Merge Right Separated Identical Tiles"); // 4
    }

    [Test]
    public void TestMergeRight_LargeValues()
    {
        int[,] board = new[,]
        {
            { 0, 0, 1024, 1024 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 2048 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 2048, "Merge Right Large Values"); // 2048
    }

    [Test]
    public void TestMergeRight_MinimalBoard()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Right, expected, 0, "Merge Right Minimal Board");
    }

    #endregion

    #region Merge Up Tests

    [Test]
    public void TestMergeUp_Simple()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 4, "Merge Up Simple"); // 4
    }

    [Test]
    public void TestMergeUp_MultipleMerges()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 0, 0, 0 },
            { 8, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 12, "Merge Up Multiple Merges"); // 4 + 8 = 12
    }

    [Test]
    public void TestMergeUp_NoMerges()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 8, 0, 0, 0 },
            { 16, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 8, 0, 0, 0 },
            { 16, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 0, "Merge Up No Merges");
    }

    [Test]
    public void TestMergeUp_MovementWithoutMerging()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 0, "Merge Up Movement Without Merging");
    }

    [Test]
    public void TestMergeUp_MixedTiles() // Corrected as per previous discussion
    {
        int[,] board = new[,]
        {
            { 2, 2, 4, 0 },
            { 2, 0, 4, 2 },
            { 0, 8, 0, 0 },
            { 0, 8, 0, 2 }
        };
        int[,] expected = new[,]
        {
            { 4, 2, 8, 4 },
            { 0, 16, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 32, "Merge Up Mixed Tiles"); // 4 + 8 + 16 + 4 = 32
    }

    [Test]
    public void TestMergeUp_EmptyBoard()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 0, "Merge Up Empty Board");
    }

    [Test]
    public void TestMergeUp_FullBoardNoMerges()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        int[,] expected = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 0, "Merge Up Full Board No Merges");
    }

    [Test]
    public void TestMergeUp_FullBoardAllSame()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 }
        };
        int[,] expected = new[,]
        {
            { 4, 4, 4, 4 },
            { 4, 4, 4, 4 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 32, "Merge Up Full Board All Same"); // 4 + 4 + 4 + 4 + 4 + 4 + 4 + 4 = 32
    }

    [Test]
    public void TestMergeUp_MergedTilesNoReMerge()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 8, "Merge Up Merged Tiles No Re-Merge"); // 4 + 4 = 8
    }

    [Test]
    public void TestMergeUp_ThreeIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 4, "Merge Up Three Identical Tiles"); // 4
    }

    [Test]
    public void TestMergeUp_FourIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 8, "Merge Up Four Identical Tiles"); // 4 + 4 = 8
    }

    [Test]
    public void TestMergeUp_InterruptedSequence()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 0, "Merge Up Interrupted Sequence");
    }

    [Test]
    public void TestMergeUp_SeparatedIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 4, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 4, "Merge Up Separated Identical Tiles"); // 4
    }

    [Test]
    public void TestMergeUp_LargeValues()
    {
        int[,] board = new[,]
        {
            { 1024, 0, 0, 0 },
            { 1024, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2048, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 2048, "Merge Up Large Values"); // 2048
    }

    [Test]
    public void TestMergeUp_MinimalBoard()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, 0, "Merge Up Minimal Board");
    }

    #endregion

    #region Merge Down Tests

    [Test]
    public void TestMergeDown_Simple()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 4, "Merge Down Simple"); // 4
    }

    [Test]
    public void TestMergeDown_MultipleMerges()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 8, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 12, "Merge Down Multiple Merges"); // 4 + 8 = 12
    }

    [Test]
    public void TestMergeDown_NoMerges()
    {
        int[,] board = new[,]
        {
            { 16, 0, 0, 0 },
            { 8, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 16, 0, 0, 0 },
            { 8, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 0, "Merge Down No Merges");
    }

    [Test]
    public void TestMergeDown_MovementWithoutMerging()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 0, "Merge Down Movement Without Merging");
    }

    [Test]
    public void TestMergeDown_MixedTiles()
    {
        int[,] board = new[,]
        {
            { 2, 2, 4, 0 },
            { 2, 0, 4, 2 },
            { 0, 8, 0, 0 },
            { 0, 8, 0, 2 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 2, 0, 0 },
            { 4, 16, 8, 4 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 32, "Merge Down Mixed Tiles"); 
    }

    [Test]
    public void TestMergeDown_EmptyBoard()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 0, "Merge Down Empty Board");
    }

    [Test]
    public void TestMergeDown_FullBoardNoMerges()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        int[,] expected = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 0, "Merge Down Full Board No Merges");
    }

    [Test]
    public void TestMergeDown_FullBoardAllSame()
    {
        int[,] board = new[,]
        {
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 },
            { 2, 2, 2, 2 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 4, 4, 4, 4 },
            { 4, 4, 4, 4 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 32, "Merge Down Full Board All Same"); // 4 * 8 = 32
    }

    [Test]
    public void TestMergeDown_MergedTilesNoReMerge()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 8, "Merge Down Merged Tiles No Re-Merge"); // 4 + 4 = 8
    }

    [Test]
    public void TestMergeDown_ThreeIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 4, "Merge Down Three Identical Tiles"); // 4
    }

    [Test]
    public void TestMergeDown_FourIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 8, "Merge Down Four Identical Tiles"); // 4 + 4 = 8
    }

    [Test]
    public void TestMergeDown_InterruptedSequence()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 },
            { 4, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 0, "Merge Down Interrupted Sequence");
    }

    [Test]
    public void TestMergeDown_SeparatedIdenticalTiles()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 4, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 4, "Merge Down Separated Identical Tiles"); // 4
    }

    [Test]
    public void TestMergeDown_LargeValues()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 1024, 0, 0, 0 },
            { 1024, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2048, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 2048, "Merge Down Large Values"); // 2048
    }

    [Test]
    public void TestMergeDown_MinimalBoard()
    {
        int[,] board = new[,]
        {
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] expected = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Down, expected, 0, "Merge Down Minimal Board");
    }

    #endregion

    #region Invalid Board Dimension Test

    [Test]
    public void TestInvalidBoardDimension()
    {
        int[,] invalidBoard = new[,]
        {
            { 2, 2, 2 },
            { 2, 2, 2 },
            { 2, 2, 2 }
        };
        Assert.Throws<ArgumentException>(() => _boardProcessor.ExecuteMove(invalidBoard, Direction.Left), "Invalid Board Dimension test failed");
    }

    #endregion

    #region AddRandomCell Tests

    [Test]
    public void TestAddRandomCell_SingleEmptyCell()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 0 }
        };
        int[,] result = _boardProcessor.AddRandomCell(board);

        // Count empty cells before and after
        int emptyBefore = CountEmptyCells(board);
        int emptyAfter = CountEmptyCells(result);
        Assert.That(emptyBefore, Is.EqualTo(1), "Expected 1 empty cell before adding");
        Assert.That(emptyAfter, Is.EqualTo(0), "Expected 0 empty cells after adding");

        // Verify the new cell is 2 or 4 at position [3,3]
        Assert.That(result[3, 3], Is.AnyOf(2, 4), "New cell should be 2 or 4");
        
        // Verify all other cells are unchanged
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i != 3 || j != 3)
                {
                    Assert.That(result[i, j], Is.EqualTo(board[i, j]), "Non-empty cells should not change");
                }
            }
        }
    }

    [Test]
    public void TestAddRandomCell_MultipleEmptyCells()
    {
        int[,] board = new[,]
        {
            { 0, 8, 2, 2 },
            { 4, 2, 0, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 2 }
        };
        int[,] result = _boardProcessor.AddRandomCell(board);

        // Count empty cells
        int emptyBefore = CountEmptyCells(board);
        int emptyAfter = CountEmptyCells(result);
        Assert.That(emptyBefore, Is.EqualTo(9), "Expected 9 empty cells before adding");
        Assert.That(emptyAfter, Is.EqualTo(8), "Expected 8 empty cells after adding");

        // Verify exactly one new cell is 2 or 4
        int newCells = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (board[i, j] == 0 && result[i, j] != 0)
                {
                    Assert.That(result[i, j], Is.AnyOf(2, 4), $"New cell at [{i},{j}] should be 2 or 4");
                    newCells++;
                }
                else
                {
                    Assert.That(result[i, j], Is.EqualTo(board[i, j]), $"Cell at [{i},{j}] should not change");
                }
            }
        }
        Assert.That(newCells, Is.EqualTo(1), "Exactly one new cell should be added");
    }

    [Test]
    public void TestAddRandomCell_AllEmptyCells()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int[,] clone = (int[,])board.Clone();
        int[,] result = _boardProcessor.AddRandomCell(board);
        // Count empty cells
        int emptyBefore = CountEmptyCells(clone); // Use clone to avoid mutation issue
        int emptyAfter = CountEmptyCells(result);
        Assert.That(emptyBefore, Is.EqualTo(16), "Expected 16 empty cells before adding");
        Assert.That(emptyAfter, Is.EqualTo(15), "Expected 15 empty cells after adding");

        // Verify exactly one cell is 2 or 4
        int newCells = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (result[i, j] != 0)
                {
                    Assert.That(result[i, j], Is.AnyOf(2, 4), $"New cell at [{i},{j}] should be 2 or 4");
                    newCells++;
                }
            }
        }
        Assert.That(newCells, Is.EqualTo(1), "Exactly one new cell should be added");
    }

    [Test]
    public void TestAddRandomCell_NoEmptyCells()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        Assert.Throws<InvalidOperationException>(() => _boardProcessor.AddRandomCell(board), 
            "Expected InvalidOperationException when no empty cells are available");
    }

    // Helper method to count empty cells
    private int CountEmptyCells(int[,] board)
    {
        int count = 0;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == 0)
                {
                    count++;
                }
            }
        }
        return count;
    }

    #endregion
    
    #region CountEmptyCells Tests

    [Test]
    public void TestCountEmptyCells_EmptyBoard()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int result = _boardProcessor.CountEmptyCells(board);
        Assert.That(result, Is.EqualTo(16), "CountEmptyCells Empty Board failed: Expected 16 empty cells");
    }

    [Test]
    public void TestCountEmptyCells_FullBoard()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        int result = _boardProcessor.CountEmptyCells(board);
        Assert.That(result, Is.EqualTo(0), "CountEmptyCells Full Board failed: Expected 0 empty cells");
    }

    [Test]
    public void TestCountEmptyCells_MixedBoard()
    {
        int[,] board = new[,]
        {
            { 2, 0, 4, 0 },
            { 0, 8, 0, 16 },
            { 32, 0, 64, 0 },
            { 0, 128, 0, 256 }
        };
        int result = _boardProcessor.CountEmptyCells(board);
        Assert.That(result, Is.EqualTo(8), "CountEmptyCells Mixed Board failed: Expected 8 empty cells");
    }

    [Test]
    public void TestCountEmptyCells_SingleEmptyCell()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 0 }
        };
        int result = _boardProcessor.CountEmptyCells(board);
        Assert.That(result, Is.EqualTo(1), "CountEmptyCells Single Empty Cell failed: Expected 1 empty cell");
    }

    [Test]
    public void TestCountEmptyCells_InvalidBoardDimension()
    {
        int[,] invalidBoard = new[,]
        {
            { 2, 2, 2 },
            { 2, 2, 2 },
            { 2, 2, 2 }
        };
        Assert.Throws<ArgumentException>(() => _boardProcessor.CountEmptyCells(invalidBoard), 
            "CountEmptyCells Invalid Board Dimension test failed");
    }
    
    #endregion

    #region GetMaxCellValue Tests

    [Test]
    public void TestGetMaxCellValue_EmptyBoard()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };
        int result = _boardProcessor.GetMaxCellValue(board);
        Assert.That(result, Is.EqualTo(0), "GetMaxCellValue Empty Board failed: Expected max value 0");
    }

    [Test]
    public void TestGetMaxCellValue_FullBoard()
    {
        int[,] board = new[,]
        {
            { 2, 4, 8, 16 },
            { 32, 64, 128, 256 },
            { 512, 1024, 2048, 4096 },
            { 8192, 16384, 32768, 65536 }
        };
        int result = _boardProcessor.GetMaxCellValue(board);
        Assert.That(result, Is.EqualTo(65536), "GetMaxCellValue Full Board failed: Expected max value 65536");
    }

    [Test]
    public void TestGetMaxCellValue_MixedBoard()
    {
        int[,] board = new[,]
        {
            { 2, 0, 4, 0 },
            { 0, 8, 0, 16 },
            { 32, 0, 64, 0 },
            { 0, 128, 0, 256 }
        };
        int result = _boardProcessor.GetMaxCellValue(board);
        Assert.That(result, Is.EqualTo(256), "GetMaxCellValue Mixed Board failed: Expected max value 256");
    }

    [Test]
    public void TestGetMaxCellValue_SingleNonEmptyCell()
    {
        int[,] board = new[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 4 }
        };
        int result = _boardProcessor.GetMaxCellValue(board);
        Assert.That(result, Is.EqualTo(4), "GetMaxCellValue Single Non-Empty Cell failed: Expected max value 4");
    }

    [Test]
    public void TestGetMaxCellValue_InvalidBoardDimension()
    {
        int[,] invalidBoard = new[,]
        {
            { 2, 2, 2 },
            { 2, 2, 2 },
            { 2, 2, 2 }
        };
        Assert.Throws<ArgumentException>(() => _boardProcessor.GetMaxCellValue(invalidBoard), 
            "GetMaxCellValue Invalid Board Dimension test failed");
    }

#endregion
}