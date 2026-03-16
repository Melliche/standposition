using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/weaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public string description;

    [Header("Combat")] 
    public float attackRange = 15f;
    public int baseDamage = 25;
    public float attacksPerSecond = 1f;

    [Header("Projectile")] 
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
}