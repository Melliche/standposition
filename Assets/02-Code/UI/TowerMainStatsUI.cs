using TMPro;
using UnityEngine;

public class TowerMainStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text armorText;
    [SerializeField] private TMP_Text regenText;

    public void SetData(int health, int armor, float regen)
    {
        healthText.text = health.ToString();
        armorText.text = $"Armor: {armor}";
        regenText.text = $"Regen: {regen}";
    }
}