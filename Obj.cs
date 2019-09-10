

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
  internal class Obj
  {
    public string TextureName = string.Empty;
    public Vector2 Position = Vector2.Zero;
    public Texture2D Texture;
    public int Direction;
    public float Speed;
    public bool IsAlive;

    public Obj(Vector2 pos)
    {
      this.Position = pos;
    }

    public Obj()
    {
    }

    public virtual void LoadContent(ContentManager content)
    {
      this.Texture = content.Load<Texture2D>(this.TextureName);
    }

    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }
  }
}
