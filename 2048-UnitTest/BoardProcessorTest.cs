using _2048.Game;
using _2048.Game.BoardProcessor;

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
    private void ExecuteAndAssert(int[,] board, Direction direction, int[,] expected, string testName)
    {
        int[,] result = _boardProcessor.ExecuteMove(board, direction);
        Assert.That(result, Is.EqualTo(expected).AsCollection, $"{testName} failed");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Simple");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Multiple Merges");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left No Merges");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Movement Without Merging");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Mixed Tiles");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Empty Board");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Full Board No Merges");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Full Board All Same");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Merged Tiles No Re-Merge");
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
        ExecuteAndAssert(board, Direction.Left, expected, "Merge Left Three Identical Tiles");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Simple");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Multiple Merges");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right No Merges");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Movement Without Merging");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Mixed Tiles");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Empty Board");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Full Board No Merges");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Full Board All Same");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Merged Tiles No Re-Merge");
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
        ExecuteAndAssert(board, Direction.Right, expected, "Merge Right Three Identical Tiles");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Simple");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Multiple Merges");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up No Merges");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Movement Without Merging");
    }

    [Test]
    public void TestMergeUp_MixedTiles()
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
            { 0, 8, 0, 0 },
            { 0, 8, 0, 0 },
            { 0, 0, 0, 0 }
        };
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Mixed Tiles");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Empty Board");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Full Board No Merges");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Full Board All Same");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Merged Tiles No Re-Merge");
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
        ExecuteAndAssert(board, Direction.Up, expected, "Merge Up Three Identical Tiles");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Simple");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Multiple Merges");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down No Merges");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Movement Without Merging");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Mixed Tiles");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Empty Board");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Full Board No Merges");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Full Board All Same");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Merged Tiles No Re-Merge");
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
        ExecuteAndAssert(board, Direction.Down, expected, "Merge Down Three Identical Tiles");
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
}