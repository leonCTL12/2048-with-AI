namespace _2048.Game.BoardProcessor;

public interface IBoardProcessor
{
    BoardProcessResult ExecuteMove(int[,] board, Direction direction);
    int[,] AddRandomCell(int[,] board); 
    int CountEmptyCells(int[,] board);
    int GetMaxCellValue(int[,] board);
}