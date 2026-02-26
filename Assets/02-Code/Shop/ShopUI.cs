using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("UI Refs")] [SerializeField] private Transform contentRoot;
    [SerializeField] private ShopItemButton itemButtonPrefab;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private GameObject emptySlotPrefab;
    [SerializeField] private ItemDetailUI itemDetailGO;
    
    public int visibleCount = 8;
    // private readonly List<ShopItemButton> itemsPurchasable = new();
    private readonly List<Transform> slots = new();
    private readonly Dictionary<ShopItem, ShopItemButton> buttonsByItem = new();
    
    /**
     * Affiche les items disponibles à l'achat, le montant d'or du joueur et le timer de rafraîchissement du shop
     * @param items Les items à afficher
     * @param gold Le montant d'or du joueur
     * @param timer Le temps restant avant le prochain rafraîchissement du shop
     */
    public void Display(List<ShopItem> items, int gold, float timer)
    {
        UpdateHeader(gold, timer);
        SetStatus("");

        ClearAllCells();

        buttonsByItem.Clear();

        for (int i = 0; i < visibleCount; i++)
        {
            if (i < items.Count)
            {
                var it = items[i];
                var btn = Instantiate(itemButtonPrefab, contentRoot); // enfant direct du grid
                btn.Bind(it, onBuy: () => shopManager.BuyItem(it), itemDetailGO);
                buttonsByItem[it] = btn;
            }
            else
            {
                Instantiate(emptySlotPrefab, contentRoot);
            }
        }
    }

    private void ClearAllCells()
    {
        for (int i = contentRoot.childCount - 1; i >= 0; i--)
            Destroy(contentRoot.GetChild(i).gameObject);
    }
    
    public void UpdateHeader(int gold, float timer)
    {
        if (goldText != null) goldText.text = $"Or: {gold}";
        if (timerText != null) timerText.text = $"Refresh: {Mathf.CeilToInt(timer)}s";
    }
    
    public void RemoveItem(ShopItem item)
    {
        if (item == null) return;
        if (!buttonsByItem.TryGetValue(item, out var btn) || btn == null) return;

        int index = btn.transform.GetSiblingIndex();
        Destroy(btn.gameObject);
        buttonsByItem.Remove(item);

        var empty = Instantiate(emptySlotPrefab, contentRoot);
        empty.transform.SetSiblingIndex(index);
    }

    public void SetStatus(string msg)
    {
        if (statusText != null) statusText.text = msg;
    }
}