namespace _2048.Input;


public class InputCommandRetriever: IInputCommandRetriever
{
    public InputCommand GetCommand()
    {
        var input = Console.ReadKey(true);
        switch (input.Key)
        {
            case ConsoleKey.UpArrow:
                return InputCommand.Up;
            case ConsoleKey.DownArrow:
                return InputCommand.Down;
            case ConsoleKey.LeftArrow:
                return InputCommand.Left;
            case ConsoleKey.RightArrow:
                return InputCommand.Right;
            case ConsoleKey.Q:
                return InputCommand.Quit;
            case ConsoleKey.A:
                return InputCommand.AiSuggestion;
            default:
                return InputCommand.Invalid;
        }
    }
}