using TMPro;
using UnityEngine;

public class TowerMainStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text armorText;
    [SerializeField] private TMP_Text regenText;
    [SerializeField] private TMP_Text attackSpeedText;

    public void SetData(float hp, float maxHp, int armor, float regen, float attackSpeed)
    {
        float bonus = (attackSpeed - 1f) * 100f;

        healthText.text = $"{hp.ToString()} / {maxHp.ToString()}";
        armorText.text = $"Armor: {armor}";
        regenText.text = $"Regen: {regen}";
        attackSpeedText.text = bonus == 0 
            ? "Attack Speed : 0%"
            : $"Attack Speed: {bonus:+0;-0}%";    }
}