using BowlingGameApp;

public class Program
{
    public static void Main(string[] args)
    {
        var game = new BowlingGame();

        while (!game.IsGameComplete())
        {
            Thread.Sleep(200);

            game.PlayNext();
            Console.WriteLine($"Frame: {game.CurrentFrame}, score: {game.GetScore()}");
        }

        var framescores = game.GetFrameScores();
        for (int i = 0; i < framescores.Length; i++)
        {
            Console.WriteLine($"frame: {i + 1}, score: {framescores[i]}");
        }
    }
}