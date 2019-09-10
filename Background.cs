// Decompiled with JetBrains decompiler
// Type: Pacman.Background
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
  internal class Background
  {
    private Texture2D backgroundTexture;

    public void LoadContent(ContentManager content)
    {
      this.backgroundTexture = content.Load<Texture2D>("Wall");
    }

    public void Draw(SpriteBatch spritebatch)
    {
      for (int index1 = 0; index1 < Map.numberColumns; ++index1)
      {
        for (int index2 = 3; index2 < Map.numberRows - 2; ++index2)
        {
          if (Map.mapTiles[index1, index2] == MapTileType.MapBarrier)
            spritebatch.Draw(this.backgroundTexture, new Rectangle(index1 * 16, index2 * 16, 16, 16), Color.DarkGreen);
        }
      }
    }
  }
}
