using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Button buyButton;
    [SerializeField] private ItemDetailUI itemDetail;
    [SerializeField] private ShopItem shopItem;

    protected void Start()
    {
        if (itemDetail != null)
        {
            itemDetail.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Initialise le bouton d'item du shop avec les données de l'item, la fonction à appeler lors de l'achat et la référence à l'UI de détail de l'item.
    /// </summary>
    /// <param name="item">Item</param>
    /// <param name="onBuy">Méthode à appeler lors de l'achat</param>
    /// <param name="itemDetailGO">Référence vers la modale d'info de l'item</param>
    public void Bind(ShopItem item, System.Action onBuy, ItemDetailUI itemDetailGO)
    {
        shopItem = item;
        image.color = GetColorFromUpgradeType(item);
        itemDetail = itemDetailGO;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => onBuy?.Invoke());
    }

    /// <summary>
    /// Retourne une couleur spécifique pour chaque type d'upgrade, afin de différencier visuellement les items du shop.
    /// </summary>
    /// <param name="item">Item</param>
    /// <returns>Couleur</returns>
    private static Color GetColorFromUpgradeType(ShopItem item)
    {
        if (item.itemType == ShopItemType.StatUpgrade)
        {
            switch (item.upgradeType)
            {
                case UpgradeType.MaxHp: return new Color(0.85f, 0.25f, 0.25f);
                case UpgradeType.Armor: return new Color(0.35f, 0.55f, 0.85f);
                case UpgradeType.Regen: return new Color(0.35f, 0.8f, 0.45f);
                case UpgradeType.AttackSpeed: return new Color(0.65f, 0.35f, 0.9f);
                case UpgradeType.Income: return new Color(1f, 0.75f, 0.2f);
                case UpgradeType.Damage: return new Color(1f, 0.45f, 0.2f);
            }
        }

        if (item.itemType == ShopItemType.Weapon)
        {
            return new Color(0.6f, 0.4f, 0.2f);
        }

        return new Color(0.8f, 0.8f, 0.8f);
    }

    /// <summary>
    /// Affiche les détails de l'item dans la popup dédiée lorsque le curseur survole le bouton d'achat de l'item.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemDetail != null && shopItem != null)
        {
            itemDetail.SetData(shopItem);
            itemDetail.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Cache les détails de l'item lorsque le curseur quitte le bouton d'achat de l'item.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemDetail != null)
        {
            itemDetail.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        itemDetail?.gameObject.SetActive(false);
    }
}