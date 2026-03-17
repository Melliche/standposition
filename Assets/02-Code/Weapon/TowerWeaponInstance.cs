using UnityEngine;

[System.Serializable]
public class TowerWeaponInstance
{
    public WeaponData weaponData;
    public float cooldownRemaining;
    public GameObject cible;

    public TowerWeaponInstance(WeaponData data)
    {
        weaponData = data;
        cooldownRemaining = 0f;
        cible = null;
    }
}