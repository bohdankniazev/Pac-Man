// Decompiled with JetBrains decompiler
// Type: Pacman.Obj
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

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
