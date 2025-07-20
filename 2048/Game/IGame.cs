using _2048.Input;

namespace _2048.Game;

public interface IGame
{
    public int MaxCellValue { get; }
    public int EmptyCellCount { get; }
    public GameResult ProcessInput(Direction direction);
    public void InitialiseGame();
    public void VisualiseGame(); 
    public int Score { get; }
    public IGame Clone();
}