using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("UI Refs")] public Transform contentRoot;
    public ShopItemButton itemButtonPrefab;
    public TMP_Text goldText;
    public ShopManager shopManager;
    public GameObject emptySlotPrefab;
    public ItemDetailUI itemDetailModaUi;

    public int visibleCount = 8;
    private readonly Dictionary<ShopItem, ShopItemButton> buttonsByItem = new();

    /**
     * Affiche les items disponibles à l'achat, le montant d'or du joueur et le timer de rafraîchissement du shop
     * @param items Les items à afficher
     * @param gold Le montant d'or du joueur
     * @param timer Le temps restant avant le prochain rafraîchissement du shop
     */
    public void Display(List<ShopItem> items, int gold, float timer)
    {
        UpdateHeader(gold);

        ClearAllCells();

        buttonsByItem.Clear();

        for (int i = 0; i < visibleCount; i++)
        {
            if (i < items.Count)
            {
                ShopItem item = items[i];
                ShopItemButton btn = Instantiate(itemButtonPrefab, contentRoot);
                btn.Bind(item, onBuy: () => shopManager.BuyItem(item), itemDetailModaUi);
                buttonsByItem[item] = btn;
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

    public void UpdateHeader(int gold)
    {
        if (goldText != null) goldText.text = $"Or: {gold}";
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
}