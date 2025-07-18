using _2048.Input;

namespace _2048.Game;

public interface IGame
{
    public void ProcessInput(InputCommand input);
    
    public void InitialiseGame();
    public void VisualiseGame();
}