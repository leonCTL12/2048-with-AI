using _2048.Input;

namespace _2048.Game;

public interface IGame
{
    public GameResult ProcessInput(Direction direction);
    public void InitialiseGame();
    public void VisualiseGame(); 
    public int Score { get; }
    public IGame Clone();
}