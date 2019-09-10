// Decompiled with JetBrains decompiler
// Type: Pacman.Level
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

namespace Pacman
{
  internal class Level
  {
    public int BonusPoints;
    public int FrightTimeSeconds;
    public float GhostSpeed;
    public static int Count;

    public static void Initialize()
    {
      Level.Count = 0;
      Level.AddLevel(0, 6, 0.7f, 50);
      Level.AddLevel(1, 6, 0.75f, 100);
      Level.AddLevel(2, 5, 0.8f, 200);
      Level.AddLevel(3, 5, 0.85f, 300);
      Level.AddLevel(4, 4, 0.85f, 400);
      Level.AddLevel(5, 4, 0.9f, 500);
      Level.AddLevel(6, 3, 0.9f, 600);
      Level.AddLevel(7, 3, 0.95f, 700);
      Level.AddLevel(8, 2, 0.95f, 800);
      Level.AddLevel(9, 2, 1f, 900);
      Level.AddLevel(10, 2, 1.1f, 1000);
      Level.AddLevel(11, 1, 1.1f, 1100);
      Level.AddLevel(12, 1, 1.2f, 1200);
      Level.AddLevel(13, 0, 1.3f, 1500);
    }

    private static void AddLevel(
      int key,
      int frightTimeSeconds,
      float ghostSpeed,
      int bonusPoints)
    {
      Items.Levels.Add(key, new Level()
      {
        FrightTimeSeconds = frightTimeSeconds,
        GhostSpeed = ghostSpeed,
        BonusPoints = bonusPoints
      });
    }
  }
}
