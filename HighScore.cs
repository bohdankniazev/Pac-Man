

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Pacman
{
  internal class HighScore : Obj
  {
    private const string FILE_NAME = "Score.data";
    private bool newHighScore;
    public int highScore;

    public HighScore()
    {
      this.IsAlive = true;
      try
      {
        FileStream fileStream = new FileStream("Score.data", FileMode.Open, FileAccess.Read);
        using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream))
          this.highScore = binaryReader.ReadInt32();
        fileStream.Close();
      }
      catch (IOException ex)
      {
        this.highScore = 0;
      }
    }

    public override void LoadContent(ContentManager content)
    {
    }

    public override void Update(GameTime gameTime)
    {
      if (!this.IsAlive)
        return;
      if (Items.Player.Score > this.highScore)
      {
        this.highScore = Items.Player.Score;
        this.newHighScore = true;
      }
      if (!(this.newHighScore & Items.Pacman.IsGameOver))
        return;
      this.WriteHighScore();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (!this.IsAlive)
        return;
      spriteBatch.DrawString(PacmanGame.HudFont, "HIGH SCORE = " + this.highScore.ToString(), new Vector2((float) (170 - this.highScore.ToString().Length), 546f), Color.Red);
    }

    private void WriteHighScore()
    {
      try
      {
        FileStream fileStream = new FileStream("Score.data", FileMode.OpenOrCreate, FileAccess.Write);
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) fileStream))
          binaryWriter.Write(this.highScore);
        fileStream.Close();
      }
      catch (IOException ex)
      {
      }
      this.newHighScore = false;
    }
  }
}
