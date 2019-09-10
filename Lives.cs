// Decompiled with JetBrains decompiler
// Type: Pacman.Lives
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
  internal class Lives : Obj
  {
    private Rectangle forDisplay;

    public Lives(string textureName)
    {
      this.TextureName = textureName;
    }

    public override void LoadContent(ContentManager content)
    {
      base.LoadContent(content);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      for (int index = 0; index < Items.Player.Lives; ++index)
      {
        if (index != 2)
        {
          this.forDisplay = new Rectangle(index * 32, 544, 32, 32);
          spriteBatch.Draw(this.Texture, this.forDisplay, Color.White);
        }
      }
    }
  }
}
