namespace GEngine
{
    class Program
    {
        public static void Main(string[] args)
        {
            TestGame game = new TestGame(800, 600, "Test!");
            game.Run();
        }
    }
}