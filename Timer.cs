// Decompiled with JetBrains decompiler
// Type: Pacman.Timer
// Assembly: WindowsGame1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 934DD255-E814-463C-81B7-5BC45736D2C2
// Assembly location: C:\Users\bohda\Desktop\Pacman\Pacman.exe

namespace Pacman
{
  internal class Timer
  {
    public int Value { get; set; }

    public int Count { get; set; }

    public bool IsAlive { get; set; }

    public bool IsFinished { get; set; }

    public bool IsPaused { get; set; }

    public Timer(int time)
    {
      this.Value = time;
      this.IsAlive = true;
      this.Reset();
    }

    public void Update()
    {
      if (!(this.IsAlive & !this.IsPaused))
        return;
      ++this.Count;
      if (this.Count != this.Value)
        return;
      this.IsFinished = true;
    }

    public void Reset()
    {
      this.Count = 0;
      this.IsFinished = false;
      this.IsPaused = false;
    }
  }
}
