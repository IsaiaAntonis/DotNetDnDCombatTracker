using System;

public class Player : CharacterBase
{

    public Player(string name, int hp, int ac, int initiative,
                        int str, int dex, int con, int intel,
                        int wis, int cha, string notes, int hitDiceAmount, int hitDiceSize, int hitDiceModifier , string imagePath , bool isDead , string equippedWeapons)

    : base(name, hp, ac, initiative, str, dex, con, intel, wis, cha, notes, hitDiceAmount, hitDiceSize, hitDiceModifier , imagePath , isDead , equippedWeapons)
    {

    }
    public Player() { }
}
