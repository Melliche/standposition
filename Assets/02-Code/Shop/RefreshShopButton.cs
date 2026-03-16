using TMPro;
using UnityEngine;

public class RefreshShopButton : MonoBehaviour
{
    [SerializeField] private Economy economy;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private int refreshCost = 100;
    [SerializeField] private TMP_Text refreshPriceText;

    void Start()
    {
        if (refreshPriceText != null)
        {
            refreshPriceText.text = refreshCost.ToString();
        }
    }

    /// <summary>
    /// Lorsque le bouton de rafraîchissement est cliqué, vérifie si le joueur a assez d'or pour payer le coût de rafraîchissement.
    /// Si oui, le shop génère de nouveaux items, le coût de rafraîchissement augmente de 50 et le texte du prix est mis à jour.
    /// </summary>
    public void OnRefreshButtonClicked()
    {
        if (shopManager && economy.Pay(refreshCost))
        {
            shopManager.RollNewItems();
            refreshCost += 50;
            if (refreshPriceText != null)
            {
                refreshPriceText.text = refreshCost.ToString();
            }
        }
    }
}