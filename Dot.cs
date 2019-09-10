

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
