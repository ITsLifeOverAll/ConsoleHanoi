using System.Text;

namespace ConsoleHanoi;

internal class Pillars
{
    public const int Count = 3;
    public int DiskCount { get; }
    public int? Selected { get; private set; }

    private List<int>[] _array = [new(), new(), new()];

    public List<int> this[int i] => _array[i];

    public Pillars(int disks)
    {
        DiskCount = disks;
        for (int i = disks; i >0; i--) 
        {
            this[0].Add(i);
        }
    }

    public void SelectSource(int? column) => Selected = column;

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < Count; i++)
        {
            var str = string.Join(", ", _array[i]);
            sb.AppendLine($"Pillar {i}: {str}");
        }
        return sb.ToString();
    }

    public bool Moveable(int pillar)
    {
        if (Selected is null) return false;
        return this[pillar].Count == 0 || this[Selected.Value][^1] < this[pillar][^1];
    }

    public void Move(int pillar)
    {
        if (!Moveable(pillar))
            throw new Exception($"{nameof(Move)}: {Selected} to {pillar} is not moveable");

        var disk = this[Selected!.Value][^1];
        var index = this[Selected.Value].Count-1;
        
        this[Selected.Value].RemoveAt(index);
        this[pillar].Add(disk);
    }
}
