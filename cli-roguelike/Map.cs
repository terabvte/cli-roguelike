namespace cli_roguelike;

public class Map
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private Tile[,] _tiles;

    public Map(int width, int height)
    {
        Width = width;
        Height = height;

        _tiles = new Tile[width, height];
    }

    public void Generate()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _tiles[i, j].Type = TileType.Wall;
            }
        }
    }

    private void CreateRoom(int roomWidth, int roomHeight, int startX, int startY)
    {
        for (int i = startX; i < (startX + roomWidth); i++)
        {
            for (int j = startY; j < (startY + roomHeight); j++)
            {
                if ((i >= 0 && i < Width) && (j >= 0 && j < Height))
                {
                    _tiles[i, j].Type = TileType.Floor;
                }
            }
        }
    }
}
