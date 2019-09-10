// Decompiled with JetBrains decompiler
// Type: Pacman.Serve
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
  internal class Serve
  {
    public static bool CheckKeyBoard(KeyboardState current, KeyboardState previous, Keys key)
    {
      return current.IsKeyDown(key) && previous.IsKeyUp(key);
    }

    public static Point WorldToMap(Vector2 position)
    {
      return new Point()
      {
        X = (int) position.X / 16,
        Y = (int) position.Y / 16
      };
    }
  }
}
