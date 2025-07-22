using System;

public abstract class CharacterBase
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int AC { get; set; }
    public int Initiative { get; set; }

    public int STR { get; set; }
    public int DEX { get; set; }
    public int CON { get; set; }
    public int INT { get; set; }
    public int WIS { get; set; }
    public int CHA { get; set; }

    public bool IsDead { get; set; }
    public bool Finesse { get; set; }

    public string EquippedWeapons { get; set; } 
    public string Notes { get; set; }

    public int HitDiceAmount { get; set; }
    public int HitDiceSize { get; set; }
    public int HitDiceFlatModifier { get; set; }

    public string ImagePath { get; set; }

    protected CharacterBase(string name, int hp, int ac, int initiative,
                            int str, int dex, int con, int intel,
                            int wis, int cha, string notes , int hitDiceAmount, int hitDiceSize, int hitDiceModifier , string imagePath , bool isDead , string equippedWeapons, bool finesse)
    {
        Name = name;
        HP = hp;
        AC = ac;
        Initiative = initiative;

        STR = str;
        DEX = dex;
        CON = con;
        INT = intel;
        WIS = wis;
        CHA = cha;

        Notes = notes;

        HitDiceAmount = hitDiceAmount;
        HitDiceSize = hitDiceSize;
        HitDiceFlatModifier = hitDiceModifier;

        ImagePath = imagePath;
        IsDead = isDead;
        EquippedWeapons = equippedWeapons;
        Finesse = finesse;
    }
    public CharacterBase() { }

}
