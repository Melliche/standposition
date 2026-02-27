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

    protected void Start()
    {
        if (itemDetail != null)
        {
            itemDetail.gameObject.SetActive(false);
        }
    }

    public void Bind(ShopItem item, System.Action onBuy, ItemDetailUI itemDetailGO)
    {
        shopItem = item;
        image.color = new Color(Random.value, Random.value, Random.value);
        itemDetail = itemDetailGO;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => onBuy?.Invoke());
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemDetail != null && shopItem != null)
        {
            itemDetail.SetData(shopItem);
            itemDetail.gameObject.SetActive(true);
        }
    }

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