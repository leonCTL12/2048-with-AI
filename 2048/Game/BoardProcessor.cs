namespace _2048.Game;

public class BoardProcessor
{
    public int[,] MoveTiles(int[,] tiles, string direction)
    {
        if (tiles.GetLength(0) != ProjectConstants.BoardDimension || tiles.GetLength(1) != ProjectConstants.BoardDimension)
        {
            throw new ArgumentException("Invalid board dimension");
        }

        // Implement the logic to move tiles based on the direction
        // This is a placeholder for the actual implementation
        // You would typically shift tiles in the specified direction and merge them accordingly

        return tiles; // Return the modified tiles array
    }
}