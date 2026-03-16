[System.Serializable]
public class TowerWeaponInstance
{
    public WeaponData weaponData;
    public float cooldownRemaining;

    public TowerWeaponInstance(WeaponData data)
    {
        weaponData = data;
        cooldownRemaining = 0f;
    }
}