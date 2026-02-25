using UnityEngine;
using UnityEngine.Serialization;

public class TowerStats : MonoBehaviour
{
    [FormerlySerializedAs("damage")] [Header("Primary Stats")] [SerializeField]
    private int maxHp = 1500;

    [SerializeField] private int armor = 0;
    [SerializeField] private int regen = 0;
    [SerializeField] private float attackSpeed = 1f;

    public int MaxHp => maxHp;
    public int Armor => armor;
    public int Regen => regen;
    public float AttackSpeed => attackSpeed;

    public int CurrentHp { get; private set; }

    private void Awake()
    {
        CurrentHp = maxHp - 1000;
    }

    void Start()
    {
        InvokeRepeating(nameof(RegenHp), 0, 1);
    }

    /**
     * Régnère les points de vie de la tour avec les points de régénération définis, jusqu'à atteindre le maximum de points de vie.
     */
    private void RegenHp()
    {
        if (regen > 0 && CurrentHp < maxHp)
        {
            CurrentHp += regen;
            if (CurrentHp > maxHp)
                CurrentHp = maxHp;
        }
    }
    
    public void AddMaxHp(int amount)
    {
        maxHp += amount;
    }
    
    public void AddRegen(int amount) 
    {
        regen += amount;
    }
    
    public void AddArmor(int amount) 
    {
        armor += amount;
    }
    
    public void MultiplyAttackSpeed(float multiplier) 
    {
        attackSpeed = this.attackSpeed * multiplier ;
    }
}