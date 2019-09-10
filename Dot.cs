// Decompiled with JetBrains decompiler
// Type: Pacman.Dot
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
  public class Dot
  {
    private Rectangle forDisplay;
    public Vector2 Position;
    public int Size;
    public bool IsAlive;

    public Dot(Vector2 position, int size)
    {
      this.Position = position;
      this.Size = size;
      this.IsAlive = true;
      this.forDisplay = new Rectangle((int) this.Position.X, (int) this.Position.Y, 16, 16);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (!this.IsAlive)
        return;
      if (this.Size == 0)
        spriteBatch.Draw(PacmanGame.DotTexture, this.forDisplay, Color.White);
      else
        spriteBatch.Draw(PacmanGame.BigDotTexture, this.forDisplay, Color.White);
    }
  }
}
