namespace cli_roguelike;

public class Player(int startX, int startY)
{
    public char Marker { get; set; } = '@';
    public int X { get; set; } = startX;
    public int Y { get; set; } = startY;
}
