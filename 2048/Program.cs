using _2048.AiAdviser;
using Autofac;
using _2048.Game;
using _2048.Input;


namespace _2048;

class Program
{
    private static void Main()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<GameManager.GameManager>().As<IGameManager>().SingleInstance();
        builder.RegisterType<InputCommandRetriever>().As<IInputCommandRetriever>().SingleInstance();
        builder.RegisterType<Game.Game>().As<IGame>().SingleInstance();
        builder.RegisterType<AiAdviser.AiAdviser>().As<IAiAdviser>().SingleInstance();
        builder.RegisterType<BoardProcessor>().As<IBoardProcessor>().SingleInstance();
        builder.RegisterType<GameResultEvaluator>().As<IGameResultEvaluator>().SingleInstance();
        var container = builder.Build();
        var gameManager = container.Resolve<IGameManager>();
        gameManager.StartGame();
    }
}