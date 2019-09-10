
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pacman
{
  internal class Bonus : Obj
  {
    private Rectangle forDisplay;
    private Random random;
    public Score Score;
    public Timer Timer;

    public Bonus()
    {
      this.IsAlive = false;
      this.Position = new Vector2(232f, 328f);
      this.Score = new Score(60);
      this.random = new Random();
      this.Timer = new Timer(this.GetNewTimeValue());
    }

    public override void LoadContent(ContentManager content)
    {
    }

    public override void Update(GameTime gameTime)
    {
      if (this.IsAlive)
      {
        this.Timer.Update();
        if (this.Timer.IsFinished)
        {
          this.Timer.Reset();
          this.Timer.Value = this.GetNewTimeValue();
          this.IsAlive = false;
        }
      }
      if (!this.Score.IsAlive)
        return;
      this.Score.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (this.IsAlive)
      {
        this.forDisplay = new Rectangle((int) this.Position.X - 8, (int) this.Position.Y - 8, 16, 16);
        spriteBatch.Draw(PacmanGame.BonusTexture, this.forDisplay, Color.White);
      }
      if (!this.Score.IsAlive)
        return;
      this.Score.Draw(spriteBatch);
    }

    public int GetNewTimeValue()
    {
      return this.random.Next(540, 600);
    }
  }
}
