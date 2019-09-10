// Decompiled with JetBrains decompiler
// Type: Pacman.Ghost
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pacman
{
  internal class Ghost : Obj
  {
    private GhostMode mode;
    private GhostIdent ident;
    private int result;
    private Point maploc;
    public DirectionEnum direction;
    private Timer homeTimer;
    private Vector2 aimPoint;
    private Rectangle forDisplay;
    public Score Score;
    public bool IsMoving;
    public Vector2 ResetPosition;
    public Vector2 StartPosition;

    public Ghost(Vector2 pos, string textureName, GhostIdent ident)
    {
      this.TextureName = textureName;
      this.Position = pos;
      this.ResetPosition = this.Position;
      this.StartPosition = new Vector2(232f, 232f);
      this.IsAlive = false;
      this.IsMoving = false;
      this.ident = ident;
      this.Score = new Score(60);
      this.Speed = Items.Levels[Level.Count].GhostSpeed;
      switch (ident)
      {
        case GhostIdent.Red:
          this.mode = GhostMode.Scatter;
          this.homeTimer = new Timer(10);
          break;
        case GhostIdent.Pink:
          this.mode = GhostMode.Home;
          this.homeTimer = new Timer(540);
          break;
        case GhostIdent.Blue:
          this.mode = GhostMode.Home;
          this.homeTimer = new Timer(360);
          break;
        case GhostIdent.Orange:
          this.mode = GhostMode.Home;
          this.homeTimer = new Timer(180);
          break;
      }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (this.IsAlive)
      {
        this.forDisplay = new Rectangle((int) this.Position.X - 8, (int) this.Position.Y - 8, 16, 16);
        if (Items.Pacman.IsSpeedUp)
          spriteBatch.Draw(PacmanGame.SpeedUpTexture, this.forDisplay, Color.White);
        else
          spriteBatch.Draw(this.Texture, this.forDisplay, Color.White);
      }
      if (!this.Score.IsAlive)
        return;
      this.Score.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
      if (this.IsAlive)
      {
        if (this.homeTimer.IsFinished)
        {
          this.Position = this.StartPosition;
          this.IsMoving = true;
          this.homeTimer.IsFinished = false;
        }
        this.GhostMove();
        switch ((int) this.Position.X)
        {
          case -1:
            this.Position.X = 445f;
            break;
          case 445:
            this.Position.X = -1f;
            break;
        }
        if (this.mode == GhostMode.Reset && !Items.Pacman.IsSpeedUp)
        {
          if (this.ident == GhostIdent.Red)
            this.Reset();
          else
            this.Reset();
        }
        if (this.Score.IsAlive)
          this.Score.Update();
      }
      this.Contact(Serve.WorldToMap(Items.Pacman.Position));
      this.homeTimer.Update();
      base.Update(gameTime);
    }

    public void Contact(Point point)
    {
      if (!this.IsAlive)
        return;
      this.maploc = Serve.WorldToMap(new Vector2(this.Position.X, this.Position.Y));
      if (!(point == this.maploc & this.IsAlive))
        return;
      if (Items.Pacman.IsSpeedUp)
      {
        this.IsMoving = false;
        ++Items.Pacman.GhostDestroyedCounter;
        this.result = 0;
        switch (Items.Pacman.GhostDestroyedCounter)
        {
          case 1:
            this.result = 200;
            break;
          case 2:
            this.result = 400;
            break;
          case 3:
            this.result = 800;
            break;
          case 4:
            this.result = 1600;
            break;
        }
        Items.Player.Score += this.result;
        this.Score.Value = this.result;
        this.Score.Position = new Vector2(this.Position.X - 16f, this.Position.Y - 16f);
        this.Score.IsAlive = true;
        this.Reset();
      }
      else
        Items.Pacman.Expire();
    }

    private void ResetTimers()
    {
      this.homeTimer.Reset();
    }

    public void Reset()
    {
      this.Position = this.ResetPosition;
      this.mode = GhostMode.Home;
      this.Speed = Items.Levels[Level.Count].GhostSpeed;
      this.ResetTimers();
      this.IsAlive = true;
      this.IsMoving = false;
    }

    private void GhostMove()
    {
      if (!this.IsMoving)
        return;
      switch (this.ident)
      {
        case GhostIdent.Red:
          this.aimPoint = Items.Pacman.mode == GhostMode.Scatter ? new Vector2(400f, 0.0f) : Items.Pacman.Position;
          break;
        case GhostIdent.Pink:
          if (Items.Pacman.mode == GhostMode.Chase)
          {
            switch (Items.Pacman.Direction)
            {
              case 0:
                this.aimPoint = new Vector2(Items.Pacman.Position.X + 64f, Items.Pacman.Position.Y);
                break;
              case 90:
                this.aimPoint = new Vector2(Items.Pacman.Position.X, Items.Pacman.Position.Y + 64f);
                break;
              case 180:
                this.aimPoint = new Vector2(Items.Pacman.Position.X - 64f, Items.Pacman.Position.Y);
                break;
              case 270:
                this.aimPoint = new Vector2(Items.Pacman.Position.X, Items.Pacman.Position.Y - 64f);
                break;
            }
          }
            
          else
          {
            this.aimPoint = new Vector2(0.0f, 48f);
            break;
          }
          break;
        case GhostIdent.Blue:
          this.aimPoint = Items.Pacman.mode != GhostMode.Chase ? new Vector2(448f, 576f) : new Vector2(2f * Items.Pacman.Position.X - Items.Red.Position.X, 2f * Items.Pacman.Position.Y - Items.Red.Position.Y);
          break;
        case GhostIdent.Orange:
          this.aimPoint = Items.Pacman.mode != GhostMode.Chase ? new Vector2(0.0f, 576f) : ((double) Vector2.Distance(this.Position, Items.Pacman.Position) <= 128.0 ? new Vector2(0.0f, 576f) : Items.Pacman.Position);
          break;
      }
      Vector2 vector2_1 = new Vector2(this.Position.X - 16f, this.Position.Y);
      Vector2 vector2_2 = new Vector2(this.Position.X + 16f, this.Position.Y);
      Vector2 vector2_3 = new Vector2(this.Position.X, this.Position.Y - 16f);
      Vector2 vector2_4 = new Vector2(this.Position.X, this.Position.Y + 16f);
      this.maploc = Serve.WorldToMap(new Vector2(this.Position.X, this.Position.Y));
      if (this.direction == DirectionEnum.Right)
      {
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1) & !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
        {
          this.direction = DirectionEnum.Right;
          this.Position.X += this.Speed;
          this.Position.Y = this.SnapToY(this.Position);
          return;
        }
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) && !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
        {
          this.direction = DirectionEnum.Up;
          this.Position.Y -= 9f;
          this.Position.X = this.SnapToX(this.Position);
          return;
        }
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) && !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          this.direction = DirectionEnum.Down;
          this.Position.Y += 9f;
          this.Position.X = this.SnapToX(this.Position);
          return;
        }
        if (PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          if (PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y))
          {
            if (Items.Pacman.mode == GhostMode.Frightened)
            {
              switch (new Random().Next(1, 3))
              {
                case 1:
                  this.Position.Y -= 9f;
                  this.Position.X = this.SnapToX(this.Position);
                  this.direction = DirectionEnum.Up;
                  return;
                case 2:
                  this.Position.Y += 9f;
                  this.Position.X = this.SnapToX(this.Position);
                  this.direction = DirectionEnum.Down;
                  return;
                case 3:
                  this.Position.X += 9f;
                  this.Position.Y = this.SnapToY(this.Position);
                  this.direction = DirectionEnum.Right;
                  return;
                default:
                  return;
              }
            }
            else
            {
              if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_4, this.aimPoint) && (double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
              {
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              }
              if ((double) Vector2.Distance(vector2_4, this.aimPoint) <= (double) Vector2.Distance(vector2_3, this.aimPoint) && (double) Vector2.Distance(vector2_4, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
              {
                this.Position.Y += 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Down;
                return;
              }
              this.Position.X += 9f;
              this.Position.Y = this.SnapToY(this.Position);
              this.direction = DirectionEnum.Right;
              return;
            }
          }
          else if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              case 2:
                this.Position.Y += 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Down;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_4, this.aimPoint))
            {
              this.Position.Y -= 9f;
              this.direction = DirectionEnum.Up;
              this.Position.X = this.SnapToX(this.Position);
              return;
            }
            this.Position.Y += 9f;
            this.direction = DirectionEnum.Down;
            this.Position.X = this.SnapToX(this.Position);
            return;
          }
        }
        else if (PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              case 2:
                this.Position.X += 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Right;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
            {
              this.Position.Y -= 9f;
              this.direction = DirectionEnum.Up;
              this.Position.X = this.SnapToX(this.Position);
              return;
            }
            this.Position.X += 9f;
            this.direction = DirectionEnum.Right;
            this.Position.Y = this.SnapToY(this.Position);
            return;
          }
        }
        else if (PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
        {
          if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.Y += 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              case 2:
                this.Position.X += 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Right;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_4, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
            {
              this.Position.Y += 9f;
              this.direction = DirectionEnum.Down;
              this.Position.X = this.SnapToX(this.Position);
              return;
            }
            this.Position.X += 9f;
            this.direction = DirectionEnum.Right;
            this.Position.Y = this.SnapToY(this.Position);
            return;
          }
        }
      }
      if (this.direction == DirectionEnum.Left)
      {
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1) & !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
        {
          this.Position.X -= this.Speed;
          this.Position.Y = this.SnapToY(this.Position);
          this.direction = DirectionEnum.Left;
          return;
        }
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y) & !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
        {
          this.Position.Y -= 9f;
          this.Position.X = this.SnapToX(this.Position);
          this.direction = DirectionEnum.Up;
          return;
        }
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y) && !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          this.Position.Y += 9f;
          this.Position.X = this.SnapToX(this.Position);
          this.direction = DirectionEnum.Down;
          return;
        }
        if (PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          if (PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y))
          {
            if (Items.Pacman.mode == GhostMode.Frightened)
            {
              switch (new Random().Next(1, 3))
              {
                case 1:
                  this.Position.Y -= 9f;
                  this.Position.X = this.SnapToX(this.Position);
                  this.direction = DirectionEnum.Up;
                  return;
                case 2:
                  this.Position.X -= 9f;
                  this.Position.Y = this.SnapToY(this.Position);
                  this.direction = DirectionEnum.Left;
                  return;
                case 3:
                  this.Position.Y += 9f;
                  this.Position.X = this.SnapToX(this.Position);
                  this.direction = DirectionEnum.Down;
                  return;
                default:
                  return;
              }
            }
            else
            {
              if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_4, this.aimPoint) && (double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_1, this.aimPoint))
              {
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              }
              if ((double) Vector2.Distance(vector2_1, this.aimPoint) <= (double) Vector2.Distance(vector2_3, this.aimPoint) && (double) Vector2.Distance(vector2_1, this.aimPoint) <= (double) Vector2.Distance(vector2_4, this.aimPoint))
              {
                this.Position.X -= 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Left;
                return;
              }
              this.Position.Y += 9f;
              this.Position.X = this.SnapToX(this.Position);
              this.direction = DirectionEnum.Down;
              return;
            }
          }
          else if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              case 2:
                this.Position.Y += 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Down;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_4, this.aimPoint))
            {
              this.Position.Y -= 9f;
              this.Position.X = this.SnapToX(this.Position);
              this.direction = DirectionEnum.Up;
              return;
            }
            this.Position.Y += 9f;
            this.Position.X = this.SnapToX(this.Position);
            this.direction = DirectionEnum.Down;
            return;
          }
        }
        else if (PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              case 2:
                this.Position.X -= 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Left;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_1, this.aimPoint))
            {
              this.Position.Y -= 9f;
              this.direction = DirectionEnum.Up;
              this.Position.X = this.SnapToX(this.Position);
              return;
            }
            this.Position.X -= 9f;
            this.direction = DirectionEnum.Left;
            this.Position.Y = this.SnapToY(this.Position);
            return;
          }
        }
        else if (PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
        {
          if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.Y += 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              case 2:
                this.Position.X -= 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Left;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_4, this.aimPoint) <= (double) Vector2.Distance(vector2_1, this.aimPoint))
            {
              this.Position.Y += 9f;
              this.direction = DirectionEnum.Down;
              this.Position.X = this.SnapToX(this.Position);
              return;
            }
            this.Position.X -= 9f;
            this.direction = DirectionEnum.Left;
            this.Position.Y = this.SnapToY(this.Position);
            return;
          }
        }
      }
      if (this.direction == DirectionEnum.Up)
      {
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) & !PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y))
        {
          this.Position.Y -= this.Speed;
          this.Position.X = this.SnapToX(this.Position);
          this.direction = DirectionEnum.Up;
          return;
        }
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y) && !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          this.Position.X += 9f;
          this.Position.Y = this.SnapToY(this.Position);
          this.direction = DirectionEnum.Right;
          return;
        }
        if (!PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) && !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          this.Position.X -= 9f;
          this.Position.Y = this.SnapToY(this.Position);
          this.direction = DirectionEnum.Left;
          return;
        }
        if (PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y))
        {
          if (PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
          {
            if (Items.Pacman.mode == GhostMode.Frightened)
            {
              switch (new Random().Next(1, 3))
              {
                case 1:
                  this.Position.Y -= 9f;
                  this.Position.X = this.SnapToX(this.Position);
                  this.direction = DirectionEnum.Up;
                  return;
                case 2:
                  this.Position.X -= 9f;
                  this.Position.Y = this.SnapToY(this.Position);
                  this.direction = DirectionEnum.Left;
                  return;
                case 3:
                  this.Position.X += 9f;
                  this.Position.Y = this.SnapToY(this.Position);
                  this.direction = DirectionEnum.Right;
                  return;
                default:
                  return;
              }
            }
            else
            {
              if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint) & (double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_1, this.aimPoint))
              {
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              }
              if ((double) Vector2.Distance(vector2_1, this.aimPoint) <= (double) Vector2.Distance(vector2_3, this.aimPoint) && (double) Vector2.Distance(vector2_1, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
              {
                this.Position.X -= 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Left;
                return;
              }
              this.Position.X += 9f;
              this.Position.Y = this.SnapToY(this.Position);
              this.direction = DirectionEnum.Right;
              return;
            }
          }
          else if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.X -= 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Left;
                return;
              case 2:
                this.Position.X += 9f;
                this.Position.Y = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Right;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_1, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
            {
              this.Position.X -= 9f;
              this.direction = DirectionEnum.Left;
              this.Position.Y = this.SnapToY(this.Position);
              return;
            }
            this.Position.X += 9f;
            this.direction = DirectionEnum.Right;
            this.Position.Y = this.SnapToY(this.Position);
            return;
          }
        }
        else if (PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              case 2:
                this.Position.X += 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Right;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
            {
              this.Position.Y -= 9f;
              this.direction = DirectionEnum.Up;
              this.Position.X = this.SnapToX(this.Position);
              return;
            }
            this.Position.X += 9f;
            this.direction = DirectionEnum.Right;
            this.Position.Y = this.SnapToY(this.Position);
            return;
          }
        }
        else if (PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y - 1))
        {
          if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 2))
            {
              case 1:
                this.Position.Y -= 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Up;
                return;
              case 2:
                this.Position.X -= 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Left;
                return;
              default:
                return;
            }
          }
          else
          {
            if ((double) Vector2.Distance(vector2_3, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
            {
              this.Position.Y -= 9f;
              this.direction = DirectionEnum.Up;
              this.Position.X = this.SnapToX(this.Position);
              return;
            }
            this.Position.X -= 9f;
            this.direction = DirectionEnum.Left;
            this.Position.Y = this.SnapToY(this.Position);
            return;
          }
        }
      }
      if (this.direction != DirectionEnum.Down)
        return;
      if (!PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) && !PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y))
      {
        this.direction = DirectionEnum.Down;
        this.Position.Y += this.Speed;
        this.Position.X = this.SnapToX(this.Position);
      }
      else if (!PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y) && !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
      {
        this.Position.X += 9f;
        this.Position.Y = this.SnapToY(this.Position);
        this.direction = DirectionEnum.Right;
      }
      else if (!PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) && !PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
      {
        this.Position.X -= 9f;
        this.Position.Y = this.SnapToY(this.Position);
        this.direction = DirectionEnum.Left;
      }
      else if (PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) && PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y))
      {
        if (PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
        {
          if (Items.Pacman.mode == GhostMode.Frightened)
          {
            switch (new Random().Next(1, 3))
            {
              case 1:
                this.Position.X -= 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Left;
                break;
              case 2:
                this.Position.Y += 9f;
                this.Position.X = this.SnapToX(this.Position);
                this.direction = DirectionEnum.Down;
                break;
              case 3:
                this.Position.X += 9f;
                this.Position.Y = this.SnapToY(this.Position);
                this.direction = DirectionEnum.Right;
                break;
            }
          }
          else if ((double) Vector2.Distance(vector2_1, this.aimPoint) <= (double) Vector2.Distance(vector2_3, this.aimPoint) && (double) Vector2.Distance(vector2_1, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
          {
            this.Position.X -= 9f;
            this.Position.Y = this.SnapToY(this.Position);
            this.direction = DirectionEnum.Left;
          }
          else if ((double) Vector2.Distance(vector2_4, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint) && (double) Vector2.Distance(vector2_4, this.aimPoint) <= (double) Vector2.Distance(vector2_1, this.aimPoint))
          {
            this.Position.Y += 9f;
            this.Position.X = this.SnapToX(this.Position);
            this.direction = DirectionEnum.Down;
          }
          else
          {
            this.Position.X += 9f;
            this.Position.Y = this.SnapToY(this.Position);
            this.direction = DirectionEnum.Right;
          }
        }
        else if (Items.Pacman.mode == GhostMode.Frightened)
        {
          switch (new Random().Next(1, 2))
          {
            case 1:
              this.Position.X -= 9f;
              this.Position.Y = this.SnapToY(this.Position);
              this.direction = DirectionEnum.Left;
              break;
            case 2:
              this.Position.X += 9f;
              this.Position.Y = this.SnapToX(this.Position);
              this.direction = DirectionEnum.Right;
              break;
          }
        }
        else if ((double) Vector2.Distance(vector2_1, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
        {
          this.Position.X -= 9f;
          this.direction = DirectionEnum.Left;
          this.Position.Y = this.SnapToY(this.Position);
        }
        else
        {
          this.Position.X += 9f;
          this.direction = DirectionEnum.Right;
          this.Position.Y = this.SnapToY(this.Position);
        }
      }
      else if (PacmanGame.Map.IsOpenLocation(this.maploc.X + 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1))
      {
        if (Items.Pacman.mode == GhostMode.Frightened)
        {
          switch (new Random().Next(1, 2))
          {
            case 1:
              this.Position.Y += 9f;
              this.Position.X = this.SnapToX(this.Position);
              this.direction = DirectionEnum.Down;
              break;
            case 2:
              this.Position.X += 9f;
              this.Position.Y = this.SnapToY(this.Position);
              this.direction = DirectionEnum.Right;
              break;
          }
        }
        else if ((double) Vector2.Distance(vector2_4, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
        {
          this.Position.Y += 9f;
          this.direction = DirectionEnum.Down;
          this.Position.X = this.SnapToX(this.Position);
        }
        else
        {
          this.Position.X += 9f;
          this.direction = DirectionEnum.Right;
          this.Position.Y = this.SnapToY(this.Position);
        }
      }
      else
      {
        if (!(PacmanGame.Map.IsOpenLocation(this.maploc.X - 1, this.maploc.Y) & PacmanGame.Map.IsOpenLocation(this.maploc.X, this.maploc.Y + 1)))
          return;
        if (Items.Pacman.mode == GhostMode.Frightened)
        {
          switch (new Random().Next(1, 2))
          {
            case 1:
              this.Position.Y += 9f;
              this.Position.X = this.SnapToX(this.Position);
              this.direction = DirectionEnum.Down;
              break;
            case 2:
              this.Position.X -= 9f;
              this.Position.Y = this.SnapToY(this.Position);
              this.direction = DirectionEnum.Left;
              break;
          }
        }
        else if ((double) Vector2.Distance(vector2_4, this.aimPoint) <= (double) Vector2.Distance(vector2_2, this.aimPoint))
        {
          this.Position.Y += 9f;
          this.direction = DirectionEnum.Down;
          this.Position.X = this.SnapToX(this.Position);
        }
        else
        {
          this.Position.X -= 9f;
          this.direction = DirectionEnum.Left;
          this.Position.Y = this.SnapToY(this.Position);
        }
      }
    }

    public void ChangeGhostDirection()
    {
      switch (this.direction)
      {
        case DirectionEnum.Right:
          this.direction = DirectionEnum.Left;
          break;
        case DirectionEnum.Down:
          this.direction = DirectionEnum.Up;
          break;
        case DirectionEnum.Left:
          this.direction = DirectionEnum.Right;
          break;
        case DirectionEnum.Up:
          this.direction = DirectionEnum.Down;
          break;
      }
    }

    public float SnapToX(Vector2 pos)
    {
      pos -= new Vector2(pos.X % 16f, pos.Y % 16f);
      return pos.X + 8f;
    }

    public float SnapToY(Vector2 pos)
    {
      pos -= new Vector2(pos.X % 16f, pos.Y % 16f);
      return pos.Y + 8f;
    }
  }
}
