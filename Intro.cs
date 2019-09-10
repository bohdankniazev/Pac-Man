// Decompiled with JetBrains decompiler
// Type: Pacman.Intro
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
  internal class Intro
  {
    private Texture2D introTexture;
    private KeyboardState current;
    private KeyboardState previous;
    private bool IsSpacePressed;
    public bool IsAlive;
    public Ready Ready;

    public Intro()
    {
      this.IsAlive = true;
      this.IsSpacePressed = false;
      this.Ready = new Ready(150);
    }

    public void LoadContent(ContentManager content)
    {
      this.introTexture = content.Load<Texture2D>(nameof (Intro));
    }

    public void Update()
    {
      if (!this.IsAlive)
        return;
      this.current = Keyboard.GetState();
      if (Serve.CheckKeyBoard(this.current, this.previous, Keys.Space) && !this.IsSpacePressed)
      {
        PacmanGame.GameState = GameState.Game;
        this.IsSpacePressed = true;
        this.Ready.IsAlive = true;
      }
      if (this.IsSpacePressed)
      {
        this.Ready.Update();
        if (this.Ready.Timer.IsFinished)
        {
          Items.Pacman.IsReady = true;
          this.Ready.Timer.Reset();
          this.IsAlive = false;
        }
      }
      this.previous = this.current;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (!this.IsAlive)
        return;
      if (!this.IsSpacePressed)
      {
        spriteBatch.Draw(this.introTexture, Vector2.Zero, Color.White);
        spriteBatch.DrawString(PacmanGame.HudFont, "PRESS SPACE TO START", new Vector2(85f, 310f), Color.Magenta);
      }
      else
        this.Ready.Draw(spriteBatch);
    }
  }
}
