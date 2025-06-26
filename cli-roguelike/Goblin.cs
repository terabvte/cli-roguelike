namespace cli_roguelike;

public class Goblin(int startX, int startY)
    : Actor(startX, startY, marker: 'Ɏ', attack: 5, defense: 0, health: 25, maxHealth: 25)
{
}
