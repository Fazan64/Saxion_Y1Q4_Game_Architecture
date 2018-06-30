using Engine;

namespace Penne
{
    static class Program
    {
        static void Main(string[] args)
        {
            var game = new Game("Penne", 640, 480);
            var level = new MainLevel();
            game.Run();
        }
    }
}
