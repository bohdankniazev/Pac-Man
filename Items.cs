// Decompiled with JetBrains decompiler
// Type: Pacman.Items
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pacman
{
  internal class Items
  {
    public static List<Obj> ObjList = new List<Obj>();
    public static Dictionary<Point, Dot> DotList = new Dictionary<Point, Dot>();
    public static Dictionary<int, Level> Levels = new Dictionary<int, Level>();
    public static Pacman Pacman;
    public static Ghost Red;
    public static Ghost Pink;
    public static Ghost Blue;
    public static Ghost Orange;
    public static HighScore HighScore;
    public static Bonus Bonus;
    public static InterMission InterMission;
    public static GameOver GameOver;
    public static Player Player;

    public static void Initialize()
    {
      Items.ObjList.Add((Obj) (Items.HighScore = new HighScore()));
      Items.ObjList.Add((Obj) (Items.Bonus = new Bonus()));
      Items.ObjList.Add((Obj) new Lives("Pacman"));
      Items.ObjList.Add((Obj) (Items.InterMission = new InterMission()));
      Items.ObjList.Add((Obj) (Items.GameOver = new GameOver()));
      Items.ObjList.Add((Obj) (Items.Red = new Ghost(new Vector2(232f, 232f), "RedGhost", GhostIdent.Red)));
      Items.ObjList.Add((Obj) (Items.Pink = new Ghost(new Vector2(264f, 280f), "PinkGhost", GhostIdent.Pink)));
      Items.ObjList.Add((Obj) (Items.Blue = new Ghost(new Vector2(232f, 280f), "BlueGhost", GhostIdent.Blue)));
      Items.ObjList.Add((Obj) (Items.Orange = new Ghost(new Vector2(200f, 280f), "OrangeGhost", GhostIdent.Orange)));
      Items.ObjList.Add((Obj) (Items.Pacman = new Pacman(new Vector2(232f, 328f), "Pacman")));
      Items.Player = new Player();
    }
  }
}
