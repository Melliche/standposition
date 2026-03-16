using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private int armor;
    [SerializeField] private int regen;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float damageMultiplier = 1f;
    [SerializeField] private TowerMainStatsUI towerMainStatsUI;

    [SerializeField] private float attackRangeDebug = 15f;

    public GameObject hitBox;
    
    private void Awake()
    {
        hp = maxHp;
    }

    void Start()
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

    public void MultiplyDamage(float multiplier)
    {
        damageMultiplier *= multiplier;
        UpdateMainStatsUI();
    }
    
    private void UpdateMainStatsUI()
    {
        if (towerMainStatsUI)
        {
            towerMainStatsUI.SetData(hp, maxHp, armor, regen, attackSpeed);
        }
    }

    public void TakeDamage(float amount)
    {
        float finalDamage = Mathf.Max(1, amount - armor);
        hp -= finalDamage;
        
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        UpdateMainStatsUI();
    }

    public int GetFinalDamage(int baseDamage)
    {
        return Mathf.RoundToInt(baseDamage * damageMultiplier);
    }

    public float GetFinalAttacksPerSecond(float baseAttacksPerSecond)
    {
        return baseAttacksPerSecond * attackSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRangeDebug);
    }
}