using System;

public class Enemy: CharacterBase
{
    public Enemy(string name, int hp, int ac, int initiative,
                            int str, int dex, int con, int intel,
                            int wis, int cha, string notes ,int hitDiceAmount, int hitDiceSize, int hitDiceModifier, string ImagePath)
    
        : base(name, hp, ac, initiative, str, dex, con, intel, wis, cha, notes ,  hitDiceAmount,  hitDiceSize,  hitDiceModifier , ImagePath)
    {
    }

    public Enemy() { }
}
