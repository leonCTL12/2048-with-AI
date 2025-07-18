namespace _2048.Game;

public interface IBoardProcessor
{
    int[,] GetNewBoard(int[,] board, Direction direction);
}