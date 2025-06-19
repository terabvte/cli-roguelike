namespace cli_roguelike;

public class Map
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private const int MAX_ROOMS = 10;
    private const int MIN_ROOM_SIZE = 6;
    private const int MAX_ROOM_SIZE = 12;

    private readonly Tile[,] _tiles;

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        _tiles = new Tile[width, height];
    }

    public Tile GetTile(int x, int y)
    {
        // Boundary check to prevent crashes
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return _tiles[x, y];
        }

        return new Tile { Type = TileType.Wall };
    }

    public (int, int) Generate()
    {
        var rand = new Random();
        FillWithWalls();

        int previousRoomCenterX = 0;
        int previousRoomCenterY = 0;

        for (int k = 0; k < MAX_ROOMS; k++)
        {
            int randomWidth = rand.Next(MIN_ROOM_SIZE, MAX_ROOM_SIZE);
            int randomHeight = rand.Next(MIN_ROOM_SIZE, MAX_ROOM_SIZE);
            int randomX = rand.Next(0, Width - randomWidth);
            int randomY = rand.Next(0, Height - randomHeight);

            CreateRoom(randomWidth, randomHeight, randomX, randomY);

            int newRoomCenterX = randomX + (randomWidth / 2);
            int newRoomCenterY = randomY + (randomHeight / 2);

            if (k == 0)
            {
                previousRoomCenterX = newRoomCenterX;
                previousRoomCenterY = newRoomCenterY;
            }
            else
            {
                CreateHorizontalTunnel(previousRoomCenterX, newRoomCenterX, previousRoomCenterY);
                CreateVerticalTunnel(previousRoomCenterY, newRoomCenterY, newRoomCenterX);

                previousRoomCenterX = newRoomCenterX;
                previousRoomCenterY = newRoomCenterY;
            }
        }

        return (previousRoomCenterX, previousRoomCenterY);
    }

    private void FillWithWalls()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _tiles[x, y].Type = TileType.Wall;
            }
        }
    }

    private void CreateRoom(int roomWidth, int roomHeight, int startX, int startY)
    {
        for (int x = startX; x < startX + roomWidth; x++)
        {
            for (int y = startY; y < startY + roomHeight; y++)
            {
                if (x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
                {
                    _tiles[x, y].Type = TileType.Floor;
                }
            }
        }
    }

    private void CreateHorizontalTunnel(int x1, int x2, int y)
    {
        for (int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
        {
            if (x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
            {
                _tiles[x, y].Type = TileType.Floor;
            }
        }
    }

    private void CreateVerticalTunnel(int y1, int y2, int x)
    {
        for (int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
        {
            if (x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
            {
                _tiles[x, y].Type = TileType.Floor;
            }
        }
    }
}
