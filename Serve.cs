

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
