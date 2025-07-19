namespace _2048.Game.BoardProcessor;

public class BoardProcessor : IBoardProcessor
{
    public int[,] ExecuteMove(int[,] board, Direction direction)
    {
        if (board.GetLength(0) != ProjectConstants.BoardDimension || board.GetLength(1) != ProjectConstants.BoardDimension)
        {
            throw new ArgumentException("Invalid board dimension");
        }

        //TODO: Implement the logic to execute a move in the specified direction
        return board; // Return the modified tiles array
    }

    public int[,] AddRandomCell(int[,] board)
    {
        //TODO: Implement the logic to add a random cell (2 or 4) to an empty position on the board
        return board;
    }
}