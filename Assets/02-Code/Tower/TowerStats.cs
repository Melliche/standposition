using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [Header("Primary Stats")]
    public int maxHp;
    public int hp;
    public int armor;
    public int regen;
    public float attackSpeed;
    [SerializeField] private TowerMainStatsUI towerMainStatsUI;

    protected void Start()
    {
        InvokeRepeating(nameof(RegenHp), 0, 1);
    }

    /**
     * Régnère les points de vie de la tour avec les points de régénération définis, jusqu'à atteindre le maximum de points de vie.
     */
    private void RegenHp()
    {
        if (regen > 0 && hp < maxHp)
        {
            hp += regen;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }

        UpdateMainStatsUI();
    }

    public void AddMaxHp(int amount)
    {
        maxHp += amount;
        UpdateMainStatsUI();
    }

    public void AddRegen(int amount)
    {
        regen += amount;
        UpdateMainStatsUI();
    }

    public void AddArmor(int amount)
    {
        armor += amount;
        UpdateMainStatsUI();
    }

    public void MultiplyAttackSpeed(float multiplier)
    {
        attackSpeed *= multiplier;
    }

    private void UpdateMainStatsUI()
    {
        if (towerMainStatsUI != null)
        {
            towerMainStatsUI.SetData(hp, maxHp, armor, regen);
        }
    }
}