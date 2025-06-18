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
}
