using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Transform contentRoot;
    [SerializeField] private ShopItemButton itemButtonPrefab;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private GameObject emptySlotPrefab;
    [SerializeField] private ItemDetailUI itemDetailModaUi;
    [SerializeField] private RectTransform refreshProgressFill;

    public int visibleCount = 8;
    private readonly Dictionary<ShopItem, ShopItemButton> buttonsByItem = new();

    /// <summary>
    /// Affiche les items disponibles à l'achat, le montant d'or du joueur et le timer de rafraîchissement du shop
    /// </summary>
    /// <param name="items">Les items à afficher</param>
    /// <param name="gold">Le montant d'or du joueur</param>
    /// <param name="timer">Le temps restant avant le prochain rafraîchissement du shop</param>
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
        if (goldText) goldText.text = $"Or: {gold}";
    }

    /// <summary>
    /// Met à jour la barre de progression du rafraîchissement du shop.
    /// </summary>
    /// <param name="normalizedValue">La valeur normalisée (entre 0 et 1) représentant le progrès du rafraîchissement.</param>
    public void UpdateRefreshProgress(float normalizedValue)
    {
        if (!refreshProgressFill) return;

        normalizedValue = Mathf.Clamp01(normalizedValue);
        // Ajuste les ancres et les offsets pour remplir la barre de progression en fonction de la valeur normalisée
        refreshProgressFill.anchorMin = new Vector2(0f, 0f);
        refreshProgressFill.anchorMax = new Vector2(normalizedValue, 1f);
        refreshProgressFill.offsetMin = Vector2.zero;
        refreshProgressFill.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// Supprime un item du shop en détruisant son bouton et en le remplaçant par une cellule vide.
    /// </summary>
    /// <param name="item">L'item à supprimer</param>
    public void RemoveItem(ShopItem item)
    {
        if (item == null) return;
        if (!buttonsByItem.TryGetValue(item, out var btn) || btn == null) return;

        int index = btn.transform.GetSiblingIndex();
        Destroy(btn.gameObject);
        buttonsByItem.Remove(item);

        GameObject empty = Instantiate(emptySlotPrefab, contentRoot);
        empty.transform.SetSiblingIndex(index);
    }
}