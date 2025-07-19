namespace _2048.Game.BoardProcessor;

public interface IBoardProcessor
{
    int[,] ExecuteMove(int[,] board, Direction direction);
    int[,] AddRandomCell(int[,] board);
}