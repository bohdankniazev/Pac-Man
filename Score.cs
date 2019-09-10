// Decompiled with JetBrains decompiler
// Type: Pacman.Score
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
  internal class Score
  {
    public int Value;
    public Timer Timer;
    public Vector2 Position;
    public bool IsAlive;

    public Score(int time)
    {
      this.IsAlive = false;
      this.Timer = new Timer(time);
      this.Position = new Vector2(200f, 16f);
    }

    public void Update()
    {
      if (!this.IsAlive)
        return;
      this.Timer.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (!this.IsAlive)
        return;
      if (!this.Timer.IsFinished)
      {
        spriteBatch.DrawString(PacmanGame.HudFont, this.Value.ToString(), this.Position, Color.Red);
      }
      else
      {
        this.Timer.Reset();
        this.IsAlive = false;
      }
    }
  }
}
