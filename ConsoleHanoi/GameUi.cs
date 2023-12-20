namespace ConsoleHanoi;

internal class GameUi
{
    public Pillars Pillars { get; }

    public GameUi(Pillars pillars) => Pillars = pillars;

    public void RenderPillars()
    {
        for (int i = Pillars.DiskCount - 1; i >= 0; i--)
        {
            for (int j = 0; j < Pillars.Count; j++)
            {
                Console.Write("  ");
                RenderDisk(Pillars[j].Count > i ? Pillars[j][i] : null);
            }
            Console.WriteLine();
        }

        string towerBase = new string('─', Pillars.DiskCount) + '┴' + new string('─', Pillars.DiskCount);
	    Console.WriteLine($"  {towerBase}  {towerBase}  {towerBase}");
	    Console.WriteLine($"  {RenderBelowBase(0)}  {RenderBelowBase(1)}  {RenderBelowBase(2)}");
	    Console.WriteLine();
    }

    private string RenderBelowBase(int pillar) =>
	        pillar == Pillars.Selected
		        ? new string('^', Pillars.DiskCount - 1) + $"[{(pillar + 1).ToString()}]" + new string('^', Pillars.DiskCount - 1)
		        : new string(' ', Pillars.DiskCount - 1) + $"[{(pillar + 1).ToString()}]" + new string(' ', Pillars.DiskCount - 1);

    void RenderDisk(int? disk)
    {
        if (disk is null)
        {
            Console.Write(new string(' ', Pillars.DiskCount) + '│' + new string(' ', Pillars.DiskCount));
        }
        else
        {
            Console.Write(new string(' ', Pillars.DiskCount - disk.Value));
            Console.BackgroundColor = GetDiskColor(disk.Value);
            Console.Write(new string(' ', disk.Value));
            Console.Write('│');
            Console.Write(new string(' ', disk.Value));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(new string(' ', Pillars.DiskCount - disk.Value));
        }
    }

    public ConsoleColor GetDiskColor(int diskSize)
    {
        return diskSize switch
        {
            1 => ConsoleColor.Red,
            2 => ConsoleColor.Green,
            3 => ConsoleColor.Blue,
            4 => ConsoleColor.Magenta,
            5 => ConsoleColor.Cyan,
            6 => ConsoleColor.DarkYellow,
            7 => ConsoleColor.White,
            8 => ConsoleColor.DarkGray,
            _ => throw new NotImplementedException()
        };
    }

    public void Clear() => Console.Clear();

    public void Render(State state, int moves)
    {
        Console.CursorVisible = false;
	    Console.Clear();
	    Console.WriteLine();
	    Console.WriteLine("  Tower Of Hanoi (河內塔問題)");
	    Console.WriteLine();
	    Console.WriteLine($"  Moves: {moves}");
	    Console.WriteLine();
	    
        RenderPillars();

        RenderStateMessge(state);
    }

    private void RenderStateMessge(State state)
    {
        switch (state)
	    {
            case State.ChooseSource:
                SourceMessage();
                break;
            case State.InvalidTarget:
                InvalidTargetMessage();
                TargetMessage(); 
                break;
            case State.ChooseTarget:
			    TargetMessage();
			    break;
            case State.Win:
                WinMessage();
                break;
        }

        void SourceMessage()
        {
            Console.WriteLine("  [1], [2], or [3] 選擇要移動的柱子");
            Console.WriteLine("  [home] 重新開始遊戲");
            Console.Write("  [escape] 結束遊戲");
        }

        void TargetMessage()
        {
            Console.WriteLine("  [1], [2], or [3] 選擇目標柱子");
            Console.WriteLine("  [home] 重新開始遊戲");
            Console.Write("  [escape] 結束遊戲");
        }

        void WinMessage()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("  您成功解決河內塔問題！！");
            
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("  [enter] 返回功能表");
            Console.Write("  [escape] 結束遊戲");
        }

        void InvalidTargetMessage()
        {
            var backgroundColor = Console.BackgroundColor;
            var foregroundColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  您不可以將大的圓盤壓在小的圓盤");
            Console.WriteLine();
            
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }
    }
}
