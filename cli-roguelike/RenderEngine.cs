namespace cli_roguelike
{
    public class RenderEngine(int width, int height)
    {
        private readonly List<Actor> _lastKnownActors = [];
        private bool _isFirstDraw = true;

        public void Draw(Map map, List<Actor> currentActors)
        {
            if (_isFirstDraw)
            {
                Console.Clear();
                DrawAllMap(map); 
                _isFirstDraw = false;
            }

            foreach (var actor in _lastKnownActors)
            {
                var tile = map.GetTile(actor.X, actor.Y);
                Console.SetCursorPosition(actor.X, actor.Y);
                Console.Write(GetCharForTile(tile.Type));
            }

            foreach (var actor in currentActors)
            {
                Console.SetCursorPosition(actor.X, actor.Y);
                Console.Write(actor.Marker);
            }

            _lastKnownActors.Clear();
            foreach (var actor in currentActors)
            {
                _lastKnownActors.Add(actor.Copy());
            }

            Console.SetCursorPosition(0, map.Height);
        }

        private void DrawAllMap(Map map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    var tile = map.GetTile(x, y);
                    Console.Write(GetCharForTile(tile.Type));
                }

                Console.WriteLine();
            }
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
}
