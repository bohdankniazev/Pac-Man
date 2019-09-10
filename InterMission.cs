// Decompiled with JetBrains decompiler
// Type: Pacman.InterMission
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

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
