using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private TowerStats tower;
    [SerializeField] private Economy economy;
    [SerializeField] private ShopUI shopUI;

    [Header("Shop Items")] [SerializeField]
    public int visibleCount = 8;

    [SerializeField] public int refreshSeconds = 30;

    private List<ShopItem> shopItems;
    private List<ShopItem> visibleShopItems;

    private void Awake()
    {
        shopItems = BuildMarket();

        RollNewItems();
        InvokeRepeating(nameof(RollNewItems), refreshSeconds, refreshSeconds);
    }

    private void RollNewItems()
    {
        visibleShopItems = shopItems.OrderBy(i => Random.value).Take(visibleCount).ToList();
        shopUI.Display(visibleShopItems, economy.Gold, refreshSeconds);
    }

    public void BuyItem(ShopItem item)
    {
        if (item != null && economy.Pay(item.cost))
        {
            ApplyUpgrade(item);
            shopUI.SetStatus($"Vous avez acheté: {item.name}");
            shopUI.Display(visibleShopItems, economy.Gold, refreshSeconds);
        }
        else
        {
            shopUI.SetStatus($"Pas assez d'or");
            shopUI.Display(visibleShopItems, economy.Gold, refreshSeconds);
        }
    }

    private void ApplyUpgrade(ShopItem item)
    {
        switch (item.upgradeType)
        {
            case UpgradeType.MaxHp:
                tower.AddMaxHp(item.intValue);
                break;
            case UpgradeType.Armor:
                tower.AddArmor(item.intValue);
                break;
            case UpgradeType.Regen:
                tower.AddRegen(item.intValue);
                break;
            case UpgradeType.AttackSpeed:
                tower.MultiplyAttackSpeed(item.floatValue);
                break;
        }

        Debug.Log("Armure: " + tower.Armor + " | Max HP: " + tower.MaxHp + " | Regen: " + tower.Regen +
                  " | Attack Speed: " + tower.AttackSpeed);
    }

    private List<ShopItem> BuildMarket()
    {
        return new List<ShopItem>()
        {
            new ShopItem
            {
                name = "Upgrade Max HP", description = "Augmente les points de vie maximum de la tour de 500.",
                cost = 100, upgradeType = UpgradeType.MaxHp, intValue = 500
            },
            new ShopItem
            {
                name = "Upgrade Armor", description = "Augmente l'armure de la tour de 10.", cost = 150,
                upgradeType = UpgradeType.Armor, intValue = 10
            },
            new ShopItem
            {
                name = "Upgrade Regen", description = "Augmente la régénération de la tour de 5 points par seconde.",
                cost = 200, upgradeType = UpgradeType.Regen, intValue = 5
            },
            new ShopItem
            {
                name = "Upgrade Attack Speed", description = "Augmente la vitesse d'attaque de la tour de 0.5.",
                cost = 250, upgradeType = UpgradeType.AttackSpeed, floatValue = 1.05f
            },
            new ShopItem
            {
                name = "Upgrade Max HP", description = "Augmente les points de vie maximum de la tour de 1000.",
                cost = 200, upgradeType = UpgradeType.MaxHp, intValue = 1000
            },
            new ShopItem
            {
                name = "Upgrade Armor", description = "Augmente l'armure de la tour de 20.", cost = 300,
                upgradeType = UpgradeType.Armor, intValue = 20
            },
            new ShopItem
            {
                name = "Upgrade Regen", description = "Augmente la régénération de la tour de 10 points par seconde.",
                cost = 400, upgradeType = UpgradeType.Regen, intValue = 10
            },
            new ShopItem
            {
                name = "Upgrade Attack Speed", description = "Augmente la vitesse d'attaque de la tour de 1.",
                cost = 500, upgradeType = UpgradeType.AttackSpeed, floatValue = 1.10f
            },
            new ShopItem
            {
                name = "Upgrade Max HP", description = "Augmente les points de vie maximum de la tour de 2000.",
                cost = 400, upgradeType = UpgradeType.MaxHp, intValue = 2000
            },
            new ShopItem
            {
                name = "Upgrade Armor", description = "Augmente l'armure de la tour de 50.", cost = 500,
                upgradeType = UpgradeType.Armor, intValue = 50
            },
            new ShopItem
            {
                name = "Upgrade Regen", description = "Augmente la régénération de la tour de 20 points par seconde.",
                cost = 800, upgradeType = UpgradeType.Regen, intValue = 20
            },
            new ShopItem
            {
                name = "Upgrade Attack Speed", description = "Augmente la vitesse d'attaque de la tour de 2.",
                cost = 1000, upgradeType = UpgradeType.AttackSpeed, floatValue = 1.20f
            }
        };
    }
}