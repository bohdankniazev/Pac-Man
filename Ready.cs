

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
  internal class Ready
  {
    public bool IsAlive;
    public bool IsShowPlayerText;
    public Timer Timer;

    public Ready(int time)
    {
      this.Timer = new Timer(time);
      this.Reset();
    }

    public void Update()
    {
      if (!this.IsAlive)
        return;
      this.Timer.Update();
      if (!this.Timer.IsFinished)
        return;
      Items.Red.Reset();
      Items.Orange.Reset();
      Items.Blue.Reset();
      Items.Pink.Reset();
      this.Reset();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(PacmanGame.HudFont, "GET READY", new Vector2(165f, 340f), Color.Yellow);
    }

    private void Reset()
    {
      this.IsAlive = false;
      this.IsShowPlayerText = true;
      this.Timer.Count = 0;
    }
  }
}
