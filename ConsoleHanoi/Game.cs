
using ConsoleHanoi;

internal class Game
{
    public int DiskCount { get; private set; }
    public Pillars Pillars { get; private set; }
    public GameUi GameUi { get; private set; }
    public State State { get; private set; }
    public int Moves { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Game()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }


    internal void Run()
    {
        while(true)
        {
            (bool isStart, int disks) = IsNewGame(); 
            if (!isStart) break; 

            DiskCount = disks;
            Pillars = new Pillars(disks);
            GameUi = new GameUi(Pillars);
            StartGame(); 
            if (State == State.Abort) break; 
        }
    }

    private void StartGame()
    {
        State = State.ChooseSource;
        Moves = 0; 
        Pillars.SelectSource(null);

        while(State != State.Win)
        {
            State = UserInputAndAct(); 
            if (State is State.Abort or State.Restart) return; 
        }

        GameUi.Render(State, Moves);
        while(true)
        {
            var key = Console.ReadKey(true).Key;
            if (key is ConsoleKey.Escape) State = State.Abort;
            if (key is ConsoleKey.Enter or ConsoleKey.Escape) return; 
        }
    }

    private State UserInputAndAct()
    {
        var statesToReturn = new State[] {State.Win, State.Abort, State.Restart};
        while (true)
        {
            GameUi.Render(State, Moves);
            var key = UserInput(); 
            State = React(key);
            if (statesToReturn.Contains(State)) return State; 
        }
    }

    private State React(ConsoleKey key)
    {
        if (key == ConsoleKey.Escape) return State.Abort;
        if (key == ConsoleKey.Home) return State.Restart;
        var state = key switch
        {
            ConsoleKey.D1 => HandlePillar(0),
            ConsoleKey.D2 => HandlePillar(1),
            ConsoleKey.D3 => HandlePillar(2),
            _ => throw new Exception($"Unknown key: {key}")
        };
        return state;
    }

    private State HandlePillar(int pillar)
    {
        if (State is State.ChooseSource)
        {
            if (Pillars[pillar].Count == 0) return State;

            Pillars.SelectSource(pillar);
            State = State.ChooseTarget;
            return State; 
        }

        // State.ChooseTarget 
        if (Pillars.Selected == pillar)
        {
            Pillars.SelectSource(null);
            State = State.ChooseSource; 
            return State; 
        }

        return SetTarget(pillar);
    }

    private State SetTarget(int pillar)
    {
        if (!Pillars.Moveable(pillar))
        {
            State = State.InvalidTarget; return State;
        }

        Pillars.Move(pillar);
        Moves++;
        Pillars.SelectSource(null);
        State = UserWin() ? State.Win : State.ChooseSource; 
        return State;
    }

    private bool UserWin() => Pillars[2].Count == DiskCount;

    private ConsoleKey UserInput()
    {
        while(true)
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Escape:
                case ConsoleKey.Home:
                    return key; 

                case ConsoleKey.D1 or ConsoleKey.NumPad1: return ConsoleKey.D1;
                case ConsoleKey.D2 or ConsoleKey.NumPad2: return ConsoleKey.D2;
                case ConsoleKey.D3 or ConsoleKey.NumPad3: return ConsoleKey.D3;
            }
        }
    }

    private (bool isStart, int disks) IsNewGame()
    {
        var disks = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("歡迎來到 Hanoi 塔遊戲 (漢諾威塔, 河內塔)");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("  請選擇圓盤數量 (數量越多，越困難):");
            Console.WriteLine("  [3] 3 disks");
            Console.WriteLine("  [4] 4 disks");
            Console.WriteLine("  [5] 5 disks");
            Console.WriteLine("  [6] 6 disks");
            Console.WriteLine("  [7] 7 disks");
            Console.WriteLine("  [8] 8 disks");
            Console.WriteLine("  [escape] 放棄遊戲");

            Console.CursorVisible = false;
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D3 or ConsoleKey.NumPad3: disks = 3; break;
                case ConsoleKey.D4 or ConsoleKey.NumPad4: disks = 4; break;
                case ConsoleKey.D5 or ConsoleKey.NumPad5: disks = 5; break;
                case ConsoleKey.D6 or ConsoleKey.NumPad6: disks = 6; break;
                case ConsoleKey.D7 or ConsoleKey.NumPad3: disks = 7; break;
                case ConsoleKey.D8 or ConsoleKey.NumPad8: disks = 8; break;
                case ConsoleKey.Escape: return (false, 0);
                default: continue;
            }
            return (true, disks);
        }
    }
}