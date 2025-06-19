namespace cli_roguelike;

public class Map
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private const int MAX_ROOMS = 10;
    private const int MIN_ROOM_SIZE = 5;
    private const int MAX_ROOM_SIZE = 15;

    private Tile[,] _tiles;

    public Map(int width, int height)
    {
        Width = width;
        Height = height;

        _tiles = new Tile[width, height];
    }
    
    public Tile GetTile(int x, int y)
    {
        return _tiles[x, y];
    }

    public void Generate()
    {
        var rand = new Random();

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _tiles[i, j].Type = TileType.Wall;
            }
        }

        int previousRoomCenterX = 0;
        int previousRoomCenterY = 0;

        for (int k = 0; k < MAX_ROOMS; k++)
        {
            var randomWidth = rand.Next(MIN_ROOM_SIZE, MAX_ROOM_SIZE);
            var randomHeight = rand.Next(MIN_ROOM_SIZE, MAX_ROOM_SIZE);

            var randomX = rand.Next(0, Width);
            var randomY = rand.Next(0, Height);

            CreateRoom(randomWidth, randomHeight, randomX, randomY);

            var newRoomCenterX = randomX + randomWidth / 2;
            var newRoomCenterY = randomY + randomHeight / 2;


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

    private void CreateHorizontalTunnel(int x1, int x2, int y)
    {
        var start = Math.Min(x1, x2);
        var end = Math.Max(x1, x2);

        for (int i = start; i <= end; i++)
        {
            _tiles[i, y].Type = TileType.Floor;
        }
    }

    private void CreateVerticalTunnel(int y1, int y2, int x)
    {
        var start = Math.Min(y1, y2);
        var end = Math.Max(y1, y2);

        for (int i = start; i <= end; i++)
        {
            _tiles[x, i].Type = TileType.Floor;
        }
    }
}
