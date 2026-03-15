using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int hp;
    [SerializeField] private int armor;
    [SerializeField] private int regen;
    [SerializeField] private float attackSpeed;
    [SerializeField] private TowerMainStatsUI towerMainStatsUI;

    protected void Start()
    {
        InvokeRepeating(nameof(RegenHp), 0, 1);
    }

    /// <summary>
    /// Régnère les points de vie de la tour avec les points de régénération définis, jusqu'à atteindre le maximum de points de vie.
    /// </summary>
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
        UpdateMainStatsUI();
    }

    private void UpdateMainStatsUI()
    {
        if (towerMainStatsUI)
        {
            towerMainStatsUI.SetData(hp, maxHp, armor, regen, attackSpeed);
        }
    }

    
}