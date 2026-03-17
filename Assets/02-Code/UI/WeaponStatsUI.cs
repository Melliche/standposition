using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WeaponStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text bowText;
    [SerializeField] private TMP_Text axeText;
    [SerializeField] private TMP_Text fireballText;
    [SerializeField] private TMP_Text stoneText;

    public void UpdateStats(TowerStats towerStats, TowerWeaponsController towerWeaponsController)
    {
        List<TowerWeaponInstance> weapons = towerWeaponsController.getOwnedWeapons();
        List<TowerWeaponInstance> bows = weapons.Where(w => w.weaponData.weaponName == "Arrow").ToList();
        List<TowerWeaponInstance> axes = weapons.Where(w => w.weaponData.weaponName == "Axe").ToList();
        List<TowerWeaponInstance> fireballs = weapons.Where(w => w.weaponData.weaponName == "FireBall").ToList();
        List<TowerWeaponInstance> stones = weapons.Where(w => w.weaponData.weaponName == "Rock").ToList();
        
        bowText.text = bows.Count > 0
            ? $"{bows.Count} : Bow | Damage : {towerStats.GetFinalDamage(bows[0].weaponData.baseDamage)}"
            : "0 : Bow | Damage : 0";

        axeText.text = axes.Count > 0
            ? $"{axes.Count} : Axe | Damage : {towerStats.GetFinalDamage(axes[0].weaponData.baseDamage)}"
            : "0 : Axe | Damage : 0";

        fireballText.text = fireballs.Count > 0
            ? $"{fireballs.Count} : Fireball | Damage : {towerStats.GetFinalDamage(fireballs[0].weaponData.baseDamage)}"
            : "0 : Fireball | Damage : 0";

        stoneText.text = stones.Count > 0
            ? $"{stones.Count} : Rock | Damage : {towerStats.GetFinalDamage(stones[0].weaponData.baseDamage)}"
            : "0 : Rock | Damage : 0";
    }
}