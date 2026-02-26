using UnityEngine;
using UnityEngine.Serialization;

public class TowerStats : MonoBehaviour
{
    [FormerlySerializedAs("damage")] [Header("Primary Stats")] 
    
    [SerializeField] private int maxHp;
    [SerializeField] private int armor;
    [SerializeField] private int regen;
    [SerializeField] private float attackSpeed;
    [SerializeField] private TowerMainStatsUI towerMainStatsUI;

    public int CurrentHp { get; private set; }
    public int CurrentMaxHp { get; private set; }
    public int CurrentArmor { get; private set; }
    public int CurrentRegen { get; private set; }
    public float CurrentAttackSpeed { get; private set; }

    
    protected void Start()
    {
        CurrentHp = 1500;
        CurrentMaxHp = maxHp;
        Debug.Log($"[TowerStats] Current MaxHp: {CurrentMaxHp}, maxhp : {maxHp}", this);
        CurrentArmor = armor;
        CurrentRegen = regen;
        CurrentAttackSpeed = attackSpeed;
        InvokeRepeating(nameof(RegenHp), 0, 1);
    }

    /**
     * Régnère les points de vie de la tour avec les points de régénération définis, jusqu'à atteindre le maximum de points de vie.
     */
    private void RegenHp()
    {
        // Debug.Log("CurrentHP  avant regen" + CurrentHp);

        if (CurrentRegen > 0 && CurrentHp < CurrentMaxHp)
        {
            CurrentHp += CurrentRegen;
            if (CurrentHp > CurrentMaxHp)
                CurrentHp = CurrentMaxHp;
        }

        // Debug.Log("CurrentHP  apres regen" + CurrentHp);

        UpdateMainStatsUI();
    }

    public void AddMaxHp(int amount)
    {
        Debug.Log($"[TowerStats] AddMaxHp sur {name} (+{amount}) | avant={CurrentMaxHp}", this);

        // Debug.Log(" CurrentMaxHp avant up : " + CurrentMaxHp + " | amount : " + amount);
        CurrentMaxHp = this.CurrentMaxHp + amount;
        Debug.Log($"[TowerStats] après={CurrentMaxHp}", this);
        UpdateMainStatsUI();
    }

    public void AddRegen(int amount)
    {
        CurrentRegen += amount;
        UpdateMainStatsUI();
    }

    public void AddArmor(int amount)
    {
        CurrentArmor += amount;
        UpdateMainStatsUI();
    }

    public void MultiplyAttackSpeed(float multiplier)
    {
        CurrentAttackSpeed *= multiplier;
    }

    private void UpdateMainStatsUI()
    {
        if (towerMainStatsUI != null)
        {
            // Debug.Log("CurrentMaxHP update ui" + CurrentMaxHp);
            // Debug.Log(CurrentHp + " | " + CurrentMaxHp + " | " + CurrentArmor + " | " + CurrentRegen);
            towerMainStatsUI.SetData(CurrentHp, CurrentArmor, CurrentRegen);
        }
    }
}