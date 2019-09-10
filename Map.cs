// Decompiled with JetBrains decompiler
// Type: Pacman.Map
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

using MapInfo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Pacman
{
  internal class Map
  {
    public static int numberRows;
    public static int numberColumns;
    private int currentMap;
    private List<MapData> maps;
    public static MapTileType[,] mapTiles;
    public bool IsInTunnel;

    public bool InMap(Point point)
    {
      if (point.Y >= 0 && point.Y < Map.numberRows && point.X >= 0)
        return point.X < Map.numberColumns;
      return false;
    }

    public bool InMap(int column, int row)
    {
      if (row >= 0 && row < Map.numberRows && column >= 0)
        return column < Map.numberColumns;
      return false;
    }

    public void LoadContent(ContentManager content)
    {
      this.maps = new List<MapData>();
      this.maps.Add(content.Load<MapData>(nameof (Map)));
      this.SetMapData();
    }

    private void SetMapData()
    {
      Map.numberColumns = this.maps[this.currentMap].NumberColumns;
      Map.numberRows = this.maps[this.currentMap].NumberRows;
      Map.mapTiles = new MapTileType[Map.numberColumns, Map.numberRows];
      for (int index = 0; index < this.maps[this.currentMap].Barriers.Count; ++index)
      {
        int x = this.maps[this.currentMap].Barriers[index].X;
        int y = this.maps[this.currentMap].Barriers[index].Y;
        Map.mapTiles[x, y] = MapTileType.MapBarrier;
      }
      for (int index = 0; index < this.maps[this.currentMap].Dots.Count; ++index)
      {
        int x = (int) this.maps[this.currentMap].Dots[index].X;
        int y = (int) this.maps[this.currentMap].Dots[index].Y;
        int z = (int) this.maps[this.currentMap].Dots[index].Z;
        Dot dot = new Dot(new Vector2((float) (x * 16), (float) (y * 16)), z);
        Point key = new Point(x, y);
        Items.DotList.Add(key, dot);
      }
    }

    public bool IsOpenLocation(int column, int row)
    {
      if (this.InTunnel(column, row))
        return true;
      if (this.InHome(column, row) || !this.InMap(column, row))
        return false;
      return Map.mapTiles[column, row] != MapTileType.MapBarrier;
    }

    private bool InTunnel(int column, int row)
    {
      if (row == 17 && column <= 1 || row == 17 && column >= 28)
      {
        this.IsInTunnel = true;
        return this.IsInTunnel;
      }
      this.IsInTunnel = false;
      return this.IsInTunnel;
    }

    public bool InHome(int column, int row)
    {
      return row == 15 && column == 13 || row == 15 && column == 14;
    }
  }
}
