using System.Collections.Generic;
using UnityEngine;

public class TowerWeaponsController : MonoBehaviour
{
    [SerializeField] private TowerStats towerStats;
    [SerializeField] private Transform firePoint;

    private readonly List<TowerWeaponInstance> ownedWeapons = new List<TowerWeaponInstance>();

    private void Update()
    {
        for (int i = 0; i < ownedWeapons.Count; i++)
        {
            // S'il y a une arme, on met à jour son cooldown, cherche une cible et tire si possible
            UpdateWeapon(ownedWeapons[i]);
        }
    }


    /// <summary>
    /// Ajoute une nouvelle arme à la tour.
    /// </summary>
    /// <param name="weaponData"></param>
    public void AddWeapon(WeaponData weaponData)
    {
        if (weaponData)
        {
            ownedWeapons.Add(new TowerWeaponInstance(weaponData));
        }
    }

    /// <summary>
    /// Met à jour le cooldown de l'arme spécifiée, cherche une cible et tire si possible.
    /// </summary>
    /// <param name="weaponInstance">Instance de l'arme</param>
    private void UpdateWeapon(TowerWeaponInstance weaponInstance)
    {
        if (weaponInstance.weaponData)
        {
            weaponInstance.cooldownRemaining -= Time.deltaTime;
            if (weaponInstance.cooldownRemaining <= 0)
            {
                GameObject target = GetClosestEnemy(weaponInstance.weaponData.attackRange);
                if (target)
                {
                    // Tire sur la cible
                    FireWeapon(weaponInstance, target);

                    float finalAttacksPerSecond = towerStats.GetFinalAttacksPerSecond(
                        weaponInstance.weaponData.attacksPerSecond
                    );

                    float cooldown = 1f / Mathf.Max(0.01f, finalAttacksPerSecond);
                    weaponInstance.cooldownRemaining = cooldown;
                }
            }
        }
    }

    /// <summary>
    /// Instancie un projectile de l'arme spécifiée et lui donne pour cible l'ennemi spécifié.
    /// </summary>
    /// <param name="weaponInstance">Instance de l'arme</param>
    /// <param name="target">Cible</param>
    private void FireWeapon(TowerWeaponInstance weaponInstance, GameObject target)
    {
        WeaponData data = weaponInstance.weaponData;

        if (data && data.projectilePrefab && firePoint)
        {
            // Instancie le projectile et l'initialise avec la cible, les dégâts finaux et la vitesse du projectile
            GameObject projGO = Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);

            Projectile projectile = projGO.GetComponent<Projectile>();
            if (projectile)
            {
                // Calcule les dégâts finaux
                int finalDamage = towerStats.GetFinalDamage(data.baseDamage);
                projectile.Initialize(target.transform, finalDamage, data.projectileSpeed);
            }
        }
    }

    /// <summary>
    /// Trouve et retourne l'ennemi le plus proche de la tour dans une certaine portée. Retourne null s'il n'y a pas d'ennemi dans la portée.
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    private GameObject GetClosestEnemy(float range)
    {

        // Récupère tous les ennemis présents dans la scène
         foreach (GameObject enemy in SkeletonBehaviour.AllEnemies) 
        {
            SkeletonBehaviour skeleton = enemy.GetComponent<SkeletonBehaviour>();
            if (!skeleton.isAimed)
            {
                skeleton.isAimed = true;
                return enemy;
            }            
        }
        return null;
    }
}