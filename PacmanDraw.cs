

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
  internal class PacmanDraw
  {
    public Rectangle forDisplay = new Rectangle();
    private Color color;
    public Texture2D Texture;
    public bool IsActive;
    public Vector2 Position;
    public Vector2 origin;
    public float Direction;

    public void Initialize(Texture2D texture, Vector2 position, Color color)
    {
      this.color = color;
      this.Position = position;
      this.Texture = texture;
      this.IsActive = true;
      this.origin = new Vector2(313f, 313f);
    }

    public void Update(GameTime gameTime)
    {
      this.forDisplay = new Rectangle((int) this.Position.X, (int) this.Position.Y, 16, 16);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (!this.IsActive)
        return;
      spriteBatch.Draw(this.Texture, this.forDisplay, new Rectangle?(), this.color, MathHelper.ToRadians(this.Direction), this.origin, SpriteEffects.None, 0.0f);
    }
  }
}
