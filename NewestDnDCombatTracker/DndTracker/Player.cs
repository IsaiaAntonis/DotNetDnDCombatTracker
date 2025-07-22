using System;

// started off with Enemy/Monster and Player but honestly its all the same thing its just an entity so this class could 
// potentially be deleted and the monster enemy class just renamed to Creature
public class Player : CharacterBase
{

    public Player(string name, int hp, int ac, int initiative,
                        int str, int dex, int con, int intel,
                        int wis, int cha, string notes, int hitDiceAmount, int hitDiceSize, int hitDiceModifier , string imagePath , bool isDead , string equippedWeapons, bool finesse)

    : base(name, hp, ac, initiative, str, dex, con, intel, wis, cha, notes, hitDiceAmount, hitDiceSize, hitDiceModifier , imagePath , isDead , equippedWeapons, finesse)
    {

    }
    public Player() { }
}
