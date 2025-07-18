using _2048.Game;
using Autofac;

namespace _2048;

class Program
{
    private static void Main()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<Game.Game>().As<IGame>().SingleInstance();
        var container = builder.Build();
        var game = container.Resolve<IGame>();
        game.StartGame();
    }
}