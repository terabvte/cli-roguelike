namespace cli_roguelike;

public class RenderEngine(int width, int height)
{
    private readonly char[,] _buffer = new char[width, height]; 
    private readonly Player _lastPlayerState = new(-1, -1); 
    private bool _isFirstDraw = true;

    public void Draw(Map map, Player player)
    {
        if (_isFirstDraw)
        {
            Console.Clear();
            DrawAll(map, player);
            _isFirstDraw = false;
            return;
        }

        var tileAtOldPlayerPos = map.GetTile(_lastPlayerState.X, _lastPlayerState.Y);
        Console.SetCursorPosition(_lastPlayerState.X, _lastPlayerState.Y);
        Console.Write(GetCharForTile(tileAtOldPlayerPos.Type));

        Console.SetCursorPosition(player.X, player.Y);
        Console.Write(player.Marker);

        _lastPlayerState.X = player.X;
        _lastPlayerState.Y = player.Y;

        Console.SetCursorPosition(0, map.Height);
    }

    private void DrawAll(Map map, Player player)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                var tile = map.GetTile(x, y);
                char tileChar = GetCharForTile(tile.Type);
                _buffer[x, y] = tileChar; // Update the buffer
                Console.Write(tileChar);
            }

            Console.WriteLine();
        }

        Console.SetCursorPosition(player.X, player.Y);
        Console.Write(player.Marker);
        _buffer[player.X, player.Y] = player.Marker;

        _lastPlayerState.X = player.X;
        _lastPlayerState.Y = player.Y;
    }

    private char GetCharForTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Wall: return '#';
            case TileType.Floor: return '.';
            default: return ' ';
        }
    }
}
