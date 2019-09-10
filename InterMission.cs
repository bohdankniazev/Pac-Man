

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
  internal class InterMission : Obj
  {
    private Ready ready;
    private Timer timer;

    public InterMission()
    {
      this.ready = new Ready(150);
      this.timer = new Timer(120);
      this.Reset();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (!this.ready.IsAlive)
        return;
      this.ready.Draw(spriteBatch);
    }

    public override void LoadContent(ContentManager content)
    {
    }

    public override void Update(GameTime gameTime)
    {
      if (!this.IsAlive)
        return;
      this.timer.Update();
      if (!this.ready.IsAlive)
      {
        Items.Pacman.ResetLevel();
        Items.Pacman.Reset();
        ++Level.Count;
      }
      if (Level.Count > 20)
        Level.Count = 20;
      this.ready.Timer.IsFinished = false;
      this.ready.IsAlive = true;
      if (!this.ready.IsAlive)
        return;
      this.ready.Update();
      if (!this.ready.Timer.IsFinished)
        return;
      Items.Pacman.IsReady = true;
      this.Reset();
    }

    public void Reset()
    {
      this.IsAlive = false;
      this.timer.Reset();
    }
  }
}
