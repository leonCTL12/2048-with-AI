namespace _2048.Game;

public class BoardProcessor : IBoardProcessor
{
    public int[,] GetNewBoard(int[,] board, Direction direction)
    {
        if (board.GetLength(0) != ProjectConstants.BoardDimension || board.GetLength(1) != ProjectConstants.BoardDimension)
        {
            throw new ArgumentException("Invalid board dimension");
        }

        // Implement the logic to move tiles based on the direction
        // This is a placeholder for the actual implementation
        // You would typically shift tiles in the specified direction and merge them accordingly

        return board; // Return the modified tiles array
    }
}