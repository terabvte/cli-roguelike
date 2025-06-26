namespace cli_roguelike;

public class Player(int startX, int startY)
    : Actor(startX, startY, marker: '@', attack: 10, defense: 0, health: 100, maxHealth: 100)
{
}
