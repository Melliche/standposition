using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("UI Refs")] 
    [SerializeField] private Transform contentRoot;
    [SerializeField] private ShopItemButton itemButtonPrefab;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private ShopManager shopManager;

    private readonly List<ShopItemButton> itemsPurchasable = new();

    /**
     * Affiche les items disponibles à l'achat, le montant d'or du joueur et le timer de rafraîchissement du shop
     * @param items Les items à afficher
     * @param gold Le montant d'or du joueur
     * @param timer Le temps restant avant le prochain rafraîchissement du shop
     */
    public void Display(List<ShopItem> items, int gold, float timer)
    {
        goldText.text = $"Or: {gold}";
        timerText.text = $"Refresh: {Mathf.CeilToInt(timer)}s";

        ClearUpgradeButtons();

        // Affiche les nouveaux items
        foreach (var it in items)
        {
            ShopItemButton btn = Instantiate(itemButtonPrefab, contentRoot);
            btn.Bind(it, onBuy: () => shopManager.BuyItem(it));
            itemsPurchasable.Add(btn);
        }
    }

    public void SetStatus(string msg)
    {
        if (statusText != null) statusText.text = msg;
    }

    /**
     * Supprime tous les boutons d'upgrade affichés
     */
    private void ClearUpgradeButtons()
    {
        for (int i = 0; i < itemsPurchasable.Count; i++)
        {
            if (itemsPurchasable[i] != null)
            {
                Destroy(itemsPurchasable[i].gameObject);
                itemsPurchasable.Clear();
            }
        }
    }
}