

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
