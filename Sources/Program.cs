using Engine;

namespace Penne
{
    static class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var level = new MainLevel();
            game.Run();
        }
    }
}
