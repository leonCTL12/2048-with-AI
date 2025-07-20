namespace _2048.Game.BoardProcessor;

public class BoardProcessor : IBoardProcessor
{
    public BoardProcessResult ExecuteMove(int[,] board, Direction direction)
    {
        if (board.GetLength(0) != ProjectConstants.BoardDimension ||
            board.GetLength(1) != ProjectConstants.BoardDimension)
        {
            throw new ArgumentException("Invalid board dimension");
        }

        switch (direction)
        {
            case Direction.Left:
                return MergeLeft(board);
            case Direction.Right:
                return MergeRight(board);
            case Direction.Up:
                return MergeUp(board);
            case Direction.Down:
                return MergeDown(board);
            default:
                throw new ArgumentException("Invalid direction");
        }
    }
    
    private BoardProcessResult MergeLeft(int[,] board)
    {
        var rows = HorizontalGather(board);
        var score = Merge(rows, false);
        var newBoard =  HorizontalRowsToBoard(rows, false);
        return new BoardProcessResult(newBoard, score);
    }
    
    private BoardProcessResult MergeRight(int[,] board)
    {
        var rows = HorizontalGather(board);
        var score = Merge(rows, true);
        var newBoard =  HorizontalRowsToBoard(rows, true);
        return new BoardProcessResult(newBoard, score);
        
    }
    

    private BoardProcessResult MergeUp(int[,] board)
    {
        var columns = VerticalGather(board);
        var score = Merge(columns, false);
        var newBoard = VerticalColumnsToBoard(columns, false);
        return new BoardProcessResult(newBoard, score);
        
    }
    
    private BoardProcessResult MergeDown(int[,] board)
    {
        var columns = VerticalGather(board);
        var score = Merge(columns, true);
        var newBoard =  VerticalColumnsToBoard(columns, true);
        return new BoardProcessResult(newBoard, score);
    }

    private List<List<int>> HorizontalGather(int[,] board)
    {
        List<List<int>> rows = new List<List<int>>();
        for (int i = 0; i < board.GetLength(0); i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != 0)
                {
                    row.Add(board[i, j]);
                }
            }
            rows.Add(row);
        }
        
        return rows;
    }
    private List<List<int>> VerticalGather(int[,] board)
    {
        List<List<int>> columns = new List<List<int>>();
        for (int i = 0; i < board.GetLength(1); i++)
        {
            List<int> column = new List<int>();
            for (int j = 0; j < board.GetLength(0); j++)
            {
                if (board[j, i] != 0)
                {
                    column.Add(board[j, i]);
                }
            }
            columns.Add(column);
        }
        
        return columns;
    }

    //Use List<List<int>> is easier than using int[,] for merging operations, because of shifting elements
    //returning the score produced by the merge operation
    private int Merge(List<List<int>> gatheredList, bool reverse)
    {
        int scoreProduced = 0;
        for (int i = 0; i < gatheredList.Count; i++)
        {
            List<int> row = gatheredList[i];
            if (reverse)
            {
                row.Reverse();
            }

            for (int j = 0; j < row.Count - 1; j++)
            {
                if (row[j] == row[j + 1])
                {
                    row[j] *= 2;
                    scoreProduced += row[j];
                    row.RemoveAt(j + 1);
                }
            }
        }

        return scoreProduced;
    }
    
    private int[,] VerticalColumnsToBoard(List<List<int>> columns, bool reverse)
    {
        int[,] board = new int[ProjectConstants.BoardDimension, ProjectConstants.BoardDimension];
        for (int i = 0; i < columns.Count; i++)
        {
            for (int j = 0; j < columns[i].Count; j++)
            {
                if (reverse)
                {
                    board[board.GetLength(0)-1-j, i] = columns[i][j];
                }
                else
                {
                    board[j, i] = columns[i][j];
                }
            }
        }
        return board;
    }
    
    private int[,] HorizontalRowsToBoard(List<List<int>> rows, bool reverse)
    {
        int[,] board = new int[ProjectConstants.BoardDimension, ProjectConstants.BoardDimension];
        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rows[i].Count; j++)
            {
                if (reverse)
                {
                    board[i, board.GetLength(1)-1-j] = rows[i][j];
                }
                else
                {
                    board[i, j] = rows[i][j];
                }
            }
        }
        return board;
    }
    

    public int[,] AddRandomCell(int[,] board)
    {
        //Keep this side effect free
        board = (int[,])board.Clone();
        List<(int, int)> emptyCells = new List<(int, int)>();
        
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == 0)
                {
                    emptyCells.Add((i, j));
                }
            }
        }
        
        if (emptyCells.Count == 0)
        {
            //when the board is full, and user move to a direction that does not merge any cells, return the board as is
            return board;
        }
        
        int numberToAdd = Random.Shared.Next(0, 2) == 0 ? 2 : 4;
        var randomIndex = Random.Shared.Next(emptyCells.Count);
        var (row, col) = emptyCells[randomIndex];
        board[row, col] = numberToAdd;
        
        return board;
    }
}