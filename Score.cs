

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
