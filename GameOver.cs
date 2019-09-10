// Decompiled with JetBrains decompiler
// Type: Pacman.GameOver
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
  internal class GameOver : Obj
  {
    private Timer timer;
    private bool draw;
    private KeyboardState current;
    private KeyboardState previous;

    public GameOver()
    {
      this.IsAlive = false;
      this.draw = false;
      this.timer = new Timer(100);
    }

    public override void LoadContent(ContentManager content)
    {
      this.Texture = content.Load<Texture2D>("gameOver");
    }

    public override void Update(GameTime gameTime)
    {
      if (!this.IsAlive)
        return;
      this.timer.Update();
      if (this.timer.Count <= this.timer.Value)
        Items.Pacman.DisableGhosts();
      if (this.timer.Count == 100)
        this.draw = true;
      if (!this.timer.IsFinished)
        return;
      PacmanGame.GameState = GameState.End;
      this.current = Keyboard.GetState();
      if (Serve.CheckKeyBoard(this.current, this.previous, Keys.Space))
      {
        PacmanGame.GameState = GameState.Game;
        Items.Player.Score = 0;
        Items.Player.Lives = 3;
        Items.Pacman.IsGameOver = false;
        Level.Count = 0;
        Items.Pacman.GhostModeTimer.Reset();
        Items.Pacman.ResetLevel();
        this.draw = false;
        this.timer.Reset();
        this.IsAlive = false;
      }
      this.previous = this.current;
    }

    public override void Draw(SpriteBatch spritebatch)
    {
      if (!this.IsAlive)
        return;
      if (this.draw & !this.timer.IsFinished)
        spritebatch.DrawString(PacmanGame.HudFont, "GAME OVER", new Vector2(170f, 270f), Color.Red);
      if (!this.draw)
        return;
      spritebatch.DrawString(PacmanGame.HudFont, "GAME OVER", new Vector2(150f, 100f), Color.Red);
      spritebatch.DrawString(PacmanGame.HudFont, "SCORE     " + Items.Player.Score.ToString(), new Vector2(100f, 200f), Color.White);
      spritebatch.DrawString(PacmanGame.HudFont, "HIGH SCORE   " + Items.HighScore.highScore.ToString(), new Vector2(100f, 250f), Color.White);
      spritebatch.DrawString(PacmanGame.HudFont, "PRESS SPACE TO TRY AGAIN", new Vector2(75f, 350f), Color.Yellow);
    }
  }
}
