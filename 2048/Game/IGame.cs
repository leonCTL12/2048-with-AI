using _2048.Input;

namespace _2048.Game;

public interface IGame
{
    public GameResult ProcessInput(InputCommand input);
    public void InitialiseGame();
    public void VisualiseGame(); 
    public int Score { get; }
    public void InitialiseGame(int[,] board, int score);
}