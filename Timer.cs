

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
