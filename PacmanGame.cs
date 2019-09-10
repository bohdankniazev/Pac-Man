// Decompiled with JetBrains decompiler
// Type: Pacman.PacmanGame
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pacman
{
  internal class PacmanGame : Game
  {
    public const int TILE_SIZE = 16;
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    public static PacmanGame PacmanGame1;
    public static GameState GameState;
    public static Background Background;
    public static Map Map;
    public static Intro Intro;
    public static Texture2D DotTexture;
    public static Texture2D BigDotTexture;
    public static Texture2D BonusTexture;
    public static Texture2D SpeedUpTexture;
    public static SpriteFont HudFont;

    public PacmanGame()
    {
      this.graphics = new GraphicsDeviceManager((Game) this);
      this.Content.RootDirectory = "Content";
      PacmanGame.PacmanGame1 = this;
      this.graphics.PreferredBackBufferWidth = 448;
      this.graphics.PreferredBackBufferHeight = 576;
      this.graphics.ApplyChanges();
      PacmanGame.Background = new Background();
      PacmanGame.Map = new Map();
      PacmanGame.Intro = new Intro();
    }

    protected override void Initialize()
    {
      Level.Initialize();
      Items.Initialize();
      PacmanGame.GameState = GameState.Intro;
      base.Initialize();
    }

    protected override void LoadContent()
    {
      this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
      PacmanGame.HudFont = this.Content.Load<SpriteFont>("SpriteFont2");
      foreach (Obj obj in Items.ObjList)
        obj.LoadContent(this.Content);
      PacmanGame.Background.LoadContent(this.Content);
      PacmanGame.Map.LoadContent(this.Content);
      PacmanGame.Intro.LoadContent(this.Content);
      PacmanGame.DotTexture = this.Content.Load<Texture2D>("Dot");
      PacmanGame.BigDotTexture = this.Content.Load<Texture2D>("BigDot");
      PacmanGame.BonusTexture = this.Content.Load<Texture2D>("Bonus");
      PacmanGame.SpeedUpTexture = this.Content.Load<Texture2D>("SpeedUp");
    }

    protected override void Update(GameTime gameTime)
    {
      if (PacmanGame.Intro.IsAlive)
      {
        PacmanGame.Intro.Update();
        Items.Pacman.Update(gameTime);
      }
      if (PacmanGame.GameState == GameState.Game)
      {
        foreach (Obj obj in Items.ObjList)
          obj.Update(gameTime);
      }
      if (PacmanGame.GameState == GameState.End)
        Items.GameOver.Update(gameTime);
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      if (PacmanGame.Intro.IsAlive)
      {
        this.GraphicsDevice.Clear(Color.White);
        this.spriteBatch.Begin();
        PacmanGame.Intro.Draw(this.spriteBatch);
        this.spriteBatch.End();
      }
      if (PacmanGame.GameState == GameState.Game)
      {
        this.GraphicsDevice.Clear(Color.White);
        this.spriteBatch.Begin();
        PacmanGame.Background.Draw(this.spriteBatch);
        this.spriteBatch.End();
        this.spriteBatch.Begin();
        if (PacmanGame.Intro.IsAlive)
          PacmanGame.Intro.Draw(this.spriteBatch);
        foreach (KeyValuePair<Point, Dot> dot in Items.DotList)
        {
          if (dot.Value.IsAlive)
            dot.Value.Draw(this.spriteBatch);
        }
        foreach (Obj obj in Items.ObjList)
          obj.Draw(this.spriteBatch);
        this.spriteBatch.DrawString(PacmanGame.HudFont, "SCORE " + Items.Player.Score.ToString(), new Vector2(170f, 16f), Color.Red);
        this.spriteBatch.End();
      }
      if (PacmanGame.GameState == GameState.End)
      {
        this.spriteBatch.Begin();
        Items.GameOver.Draw(this.spriteBatch);
        this.spriteBatch.End();
      }
      base.Draw(gameTime);
    }
  }
}
