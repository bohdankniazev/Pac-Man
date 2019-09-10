

namespace Pacman
{
  internal static class Program
  {
    private static void Main(string[] args)
    {
      using (PacmanGame pacmanGame = new PacmanGame())
        pacmanGame.Run();
    }
  }
}
