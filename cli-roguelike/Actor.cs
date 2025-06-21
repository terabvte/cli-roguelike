namespace cli_roguelike;

public abstract class Actor(int startX, int startY, char marker)
{
    public int X { get; set; } = startX;
    public int Y { get; set; } = startY;
    public char Marker { get; set; } = marker;
}
