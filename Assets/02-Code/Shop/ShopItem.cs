using System;

public enum UpgradeType
{
    MaxHp,
    Armor,
    Regen,
    AttackSpeed,
    Income,
    Damage
}

public class ShopItem
{
    public string name;
    public string description;
    public int cost;
    public UpgradeType upgradeType;
    public ShopItemType itemType;
    public int intValue;
    public float floatValue;
    public WeaponData weaponData;
}

public enum ShopItemType
{
    StatUpgrade,
    Weapon
}