namespace cli_roguelike;

public class Player(int x, int y)
{
    public char Marker { get; set; }
        = '@';

    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}
