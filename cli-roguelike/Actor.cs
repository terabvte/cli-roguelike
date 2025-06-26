namespace cli_roguelike
{
    public abstract class Actor(int startX, int startY, char marker, int health, int attack, int defense, int maxHealth)
    {
        public int X { get; set; } = startX;
        public int Y { get; set; } = startY;
        public char Marker { get; set; } = marker;

        public int Health { get; set; } = health;
        public int Attack { get; set; } = attack;
        public int Defense { get; set; } = defense;

        public int MaxHealth { get; set; } = maxHealth;

        // thanks gippity
        public Actor Copy()
        {
            return (Actor)this.MemberwiseClone();
        }
        
        
    }
}
