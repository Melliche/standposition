using System;

public enum UpgradeType
{
    MaxHp,
    Armor,
    Regen,
    AttackSpeed,
    Income
}

public class ShopItem
{
    public string name;
    public string description;
    public int cost;
    public UpgradeType upgradeType;
    public int intValue;
    public float floatValue;
}