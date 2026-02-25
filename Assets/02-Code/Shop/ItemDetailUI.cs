using TMPro;
using UnityEngine;

public class ItemDetailUI : MonoBehaviour
{
    
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text descText;
    
    public void SetData(ShopItem item)
    {
        titleText.text = item.name;
        goldText.text = item.cost.ToString();
        descText.text = item.description;
    }
}
