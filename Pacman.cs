

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pacman
{
  internal class Pacman : Obj
  {
    private KeyboardState current;
    private KeyboardState previous;
    private PacmanDraw pacmanAnimation;
    private int speedUpTimer;
    private int speedUpTime;
    private Point BonusMapPosition;
    private bool Dots50Eat;
    private bool Dots150Eat;
    private bool IsReset;
    private Ready ready;
    public bool IsGameOver;
    public int DotsCounter;
    public bool IsSpeedUp;
    public bool IsReady;
    public bool IsReload;
    public int GhostDestroyedCounter;
    public GhostMode mode;
    public Timer GhostModeTimer;
    public GhostMode previousMode;

    public Pacman(Vector2 pos, string textureName)
    {
      this.TextureName = textureName;
      this.Position = pos;
      this.IsAlive = true;
      this.Speed = 2f;
      this.pacmanAnimation = new PacmanDraw();
      this.IsGameOver = false;
      this.IsSpeedUp = false;
      this.IsReady = false;
      this.Dots150Eat = false;
      this.Dots50Eat = false;
      this.IsReset = false;
      this.IsReload = false;
      this.GhostDestroyedCounter = 0;
      this.ready = new Ready(150);
      this.GhostModeTimer = new Timer(5040);
    }

    public override void Update(GameTime gameTime)
    {
      this.current = Keyboard.GetState();
      if (PacmanGame.GameState == GameState.Game)
      {
        if (this.IsReload & !this.IsGameOver)
        {
          this.DisableGhosts();
          this.ready.IsAlive = true;
          if (this.ready.IsAlive)
            this.ready.Update();
          if (!this.IsAlive & this.ready.IsAlive)
          {
            this.Reset();
            this.IsAlive = true;
            this.IsReset = true;
            --Items.Player.Lives;
          }
          if (this.ready.Timer.IsFinished)
          {
            this.IsReady = true;
            this.IsReload = false;
            this.ready.Timer.Reset();
            this.ready.IsAlive = false;
          }
        }
        if (!this.IsAlive)
          return;
        if (this.IsReady && !this.GhostModeTimer.IsFinished)
        {
          switch (this.GhostModeTimer.Count)
          {
            case 0:
            case 1620:
            case 3240:
            case 4740:
              this.mode = GhostMode.Scatter;
              break;
            case 420:
            case 2040:
            case 3540:
              this.mode = GhostMode.Chase;
              break;
            case 5040:
              this.mode = GhostMode.Chase;
              this.GhostModeTimer.IsFinished = true;
              break;
          }
          this.GhostModeTimer.Update();
        }
        if (this.current.IsKeyDown(Keys.Up) & this.IsReady)
        {
          Point map = Serve.WorldToMap(new Vector2(this.Position.X, this.Position.Y + 7f));
          if (PacmanGame.Map.IsOpenLocation(map.X, map.Y - 1))
          {
            this.Direction = 270;
            this.Position.Y -= this.Speed;
            this.Position.X = this.SnaptoX(this.Position);
            this.CheckDotContact(map);
            this.CheckBonusContact(map);
            this.CheckGhostContact(map);
          }
        }
        if (this.current.IsKeyDown(Keys.Down) & this.IsReady)
        {
          Point map = Serve.WorldToMap(new Vector2(this.Position.X, this.Position.Y - 7f));
          if (PacmanGame.Map.IsOpenLocation(map.X, map.Y + 1))
          {
            this.Direction = 90;
            this.Position.Y += this.Speed;
            this.Position.X = this.SnaptoX(this.Position);
            this.CheckDotContact(map);
            this.CheckBonusContact(map);
            this.CheckGhostContact(map);
          }
        }
        if (this.current.IsKeyDown(Keys.Left) & this.IsReady)
        {
          Point map = Serve.WorldToMap(new Vector2(this.Position.X + 7f, this.Position.Y));
          if (PacmanGame.Map.IsOpenLocation(map.X - 1, map.Y))
          {
            this.Direction = 180;
            this.Position.X -= this.Speed;
            this.Position.Y = this.SnaptoY(this.Position);
            this.CheckDotContact(map);
            this.CheckBonusContact(map);
            this.CheckGhostContact(map);
            if (PacmanGame.Map.IsInTunnel & map.X == -1)
              this.Position.X = 445f;
          }
        }
        if (this.current.IsKeyDown(Keys.Right) & this.IsReady)
        {
          Point map = Serve.WorldToMap(new Vector2(this.Position.X - 7f, this.Position.Y));
          if (PacmanGame.Map.IsOpenLocation(map.X + 1, map.Y))
          {
            this.Direction = 0;
            this.Position.X += this.Speed;
            this.Position.Y = this.SnaptoY(this.Position);
            this.CheckDotContact(map);
            this.CheckBonusContact(map);
            this.CheckGhostContact(map);
            if (PacmanGame.Map.IsInTunnel & map.X == 29)
              this.Position.X = -1f;
          }
        }
        if (this.IsSpeedUp)
        {
          if (this.speedUpTime > 0)
          {
            --this.speedUpTime;
          }
          else
          {
            this.IsSpeedUp = false;
            this.mode = this.previousMode;
          }
        }
        if (this.DotsCounter == 50 & !this.Dots50Eat)
        {
          Items.Bonus.Timer.Reset();
          Items.Bonus.IsAlive = true;
          this.Dots50Eat = true;
        }
        if (this.DotsCounter == 150 & !this.Dots150Eat)
        {
          Items.Bonus.Timer.Reset();
          Items.Bonus.IsAlive = true;
          this.Dots150Eat = true;
        }
        if (this.DotsCounter == 244)
        {
          this.IsReady = false;
          Items.InterMission.IsAlive = true;
        }
        this.pacmanAnimation.Position = this.Position;
        this.pacmanAnimation.Direction = (float) this.Direction;
        this.pacmanAnimation.Update(gameTime);
      }
      this.previous = this.current;
      base.Update(gameTime);
    }

    public override void LoadContent(ContentManager content)
    {
      base.LoadContent(content);
      this.pacmanAnimation.Initialize(this.Texture, this.Position, Color.White);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (PacmanGame.GameState != GameState.Game)
        return;
      if (this.IsAlive & !this.IsReload || this.IsAlive && this.IsReset)
        this.pacmanAnimation.Draw(spriteBatch);
      else if (this.IsAlive & this.IsReload)
        this.IsAlive = false;
      if (!this.ready.IsAlive)
        return;
      this.ready.Draw(spriteBatch);
    }

    public float SnaptoX(Vector2 pos)
    {
      pos -= new Vector2(pos.X % 16f, pos.Y % 16f);
      return pos.X + 8f;
    }

    public float SnaptoY(Vector2 pos)
    {
      pos -= new Vector2(pos.X % 16f, pos.Y % 16f);
      return pos.Y + 8f;
    }

    private void CheckDotContact(Point point)
    {
      if (!Items.DotList.ContainsKey(point) || !Items.DotList[point].IsAlive)
        return;
      Items.DotList[point].IsAlive = false;
      ++this.DotsCounter;
      switch (Items.DotList[point].Size)
      {
        case 0:
          Items.Player.Score += 10;
          break;
        case 1:
          this.IsSpeedUp = true;
          this.speedUpTime = Items.Levels[Level.Count].FrightTimeSeconds * 60;
          this.speedUpTimer = this.speedUpTime;
          this.GhostDestroyedCounter = 0;
          Items.Player.Score += 50;
          this.previousMode = this.mode;
          this.mode = GhostMode.Frightened;
          this.ChangeGhostsDirections();
          break;
      }
    }

    private void CheckBonusContact(Point point)
    {
      this.BonusMapPosition = Serve.WorldToMap(new Vector2(Items.Bonus.Position.X, Items.Bonus.Position.Y));
      if (!(point == this.BonusMapPosition & Items.Bonus.IsAlive))
        return;
      Items.Bonus.Timer.Reset();
      Items.Bonus.Timer.Value = Items.Bonus.GetNewTimeValue();
      Items.Bonus.IsAlive = false;
      Items.Player.Score += Items.Levels[Level.Count].BonusPoints;
      Items.Bonus.Score.Value = Items.Levels[Level.Count].BonusPoints;
      Items.Bonus.Score.Position = new Vector2(Items.Bonus.Position.X - 8f, Items.Bonus.Position.Y - 8f);
      Items.Bonus.Score.IsAlive = true;
    }

    public void DisableGhosts()
    {
      Items.Red.IsAlive = false;
      Items.Blue.IsAlive = false;
      Items.Orange.IsAlive = false;
      Items.Pink.IsAlive = false;
    }

    public void ChangeGhostsDirections()
    {
      Items.Red.ChangeGhostDirection();
      Items.Orange.ChangeGhostDirection();
      Items.Pink.ChangeGhostDirection();
      Items.Blue.ChangeGhostDirection();
    }

    private void CheckGhostContact(Point point)
    {
      Items.Red.Contact(point);
      Items.Blue.Contact(point);
      Items.Orange.Contact(point);
      Items.Pink.Contact(point);
    }

    public void Expire()
    {
      this.IsReady = false;
      this.IsReload = true;
      this.IsReset = false;
      Items.Bonus.IsAlive = false;
      if (Items.Player.Lives != 0)
        return;
      this.IsGameOver = true;
      Items.GameOver.IsAlive = true;
    }

    public void Reset()
    {
      this.Position = new Vector2(232f, 328f);
      this.Direction = 0;
    }

    private void ResetDots()
    {
      foreach (KeyValuePair<Point, Dot> dot in Items.DotList)
        dot.Value.IsAlive = true;
    }

    public void ResetLevel()
    {
      this.DisableGhosts();
      this.DotsCounter = 0;
      this.ResetDots();
      this.Dots150Eat = false;
      this.Dots50Eat = false;
    }
  }
}
