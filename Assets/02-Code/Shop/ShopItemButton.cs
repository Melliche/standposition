using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")] [SerializeField] private Image image;

    [SerializeField] private Button buyButton;
    private ItemDetailUI itemDetail;
    private ShopItem shopItem;

    public void Awake()
    {
        itemDetail = GameObject.Find("ItemDetailModale")
            .GetComponent<ItemDetailUI>();
    }

    public void Bind(ShopItem item, System.Action onBuy)
    {
        // Bind image
        shopItem = item;
        image.color = new Color(Random.value, Random.value, Random.value);
        // titleText.text = item.name;
        // descText.text = item.description;
        // costText.text = $"{item.cost} or";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => onBuy?.Invoke());
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        itemDetail.SetData(shopItem);
        itemDetail.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDetail.gameObject.SetActive(false);
    }
}