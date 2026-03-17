using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TowerStats tower;
    [SerializeField] private Economy economy;
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private int visibleCount = 8;
    [SerializeField] private int refreshSeconds = 30;
    [SerializeField] private TowerWeaponsController towerWeaponsController;
    [SerializeField] private WeaponData arrowWeaponData;
    [SerializeField] private WeaponData axeWeaponData;
    [SerializeField] private WeaponData fireballWeaponData;
    [SerializeField] private WeaponData rockWeaponData;
    
    private List<ShopItem> shopItems;
    private List<ShopItem> visibleShopItems;
    private float refreshTimer;

    private void Start()
    {
        shopUI.UpdateHeader(economy.Gold);
        shopItems = BuildMarket();
        RollNewItems();

        // Initialise le timer de rafraîchissement du shop et met à jour l'UI pour afficher le timer complet
        refreshTimer = refreshSeconds;
        shopUI.UpdateRefreshProgress(1f);
    }

    private void Update()
    {
        refreshTimer -= Time.deltaTime;

        float normalizedProgress = refreshTimer / refreshSeconds;
        shopUI.UpdateRefreshProgress(normalizedProgress);

        if (refreshTimer <= 0f)
        {
            RollNewItems();
            refreshTimer = refreshSeconds;
            shopUI.UpdateRefreshProgress(1f);
        }
    }

    /// <summary>
    /// Génère une nouvelle liste d'upgrades achetables.
    /// Met ensuite à jour l'UI du shop
    /// </summary>
    public void RollNewItems()
    {
        visibleShopItems = shopItems.OrderBy(i => Random.value).Take(visibleCount).ToList();
        shopUI.Display(visibleShopItems, economy.Gold, refreshSeconds);
    }

    /// <summary>
    /// Tente d'acheter l'item spécifié. Si le paiement est réussi, applique l'upgrade à la tour, retire l'item de la liste des items visibles et met à jour l'UI du shop.
    /// </summary>
    /// <param name="item">L'item à acheter</param>
    public void BuyItem(ShopItem item)
    {
        if (item != null && economy.Pay(item.cost))
        {
            ApplyUpgrade(item);

            visibleShopItems.Remove(item);
            shopUI.RemoveItem(item);
        }

        shopUI.UpdateHeader(economy.Gold);
    }

    /// <summary>
    /// Applique l'upgrade de l'item acheté à la tour ou à l'économie du joueur en fonction du type d'upgrade de l'item.
    /// </summary>
    /// <param name="item">L'item qui vient d'être débloqué</param>
    private void ApplyUpgrade(ShopItem item)
    {
        switch (item.itemType)
        {
            case ShopItemType.StatUpgrade:
                ApplyStatUpgrade(item);
                break;

            case ShopItemType.Weapon:
                towerWeaponsController.AddWeapon(item.weaponData);
                break;
        }
    }

    private void ApplyStatUpgrade(ShopItem item)
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
            case UpgradeType.Income:
                economy.PassiveIncome += item.intValue;
                break;
            case UpgradeType.Damage:
                tower.MultiplyDamage(item.floatValue);
                break;
        }
    }
    private List<ShopItem> BuildMarket()
    {
        return new List<ShopItem>()
        {
            new ShopItem
            {
                name = "Upgrade Max HP", description = "Augmente les points de vie maximum de la tour de 50.",
                cost = 100, upgradeType = UpgradeType.MaxHp, intValue = 50, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Armor", description = "Augmente l'armure de la tour de 5.", cost = 150,
                upgradeType = UpgradeType.Armor, intValue = 5, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Regen", description = "Augmente la régénération de la tour de 5 points par seconde.",
                cost = 400, upgradeType = UpgradeType.Regen, intValue = 5, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Attack Speed", description = "Augmente la vitesse d'attaque de la tour de  5 %",
                cost = 500, upgradeType = UpgradeType.AttackSpeed, floatValue = 1.05f, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Max HP", description = "Augmente les points de vie maximum de la tour de 100.",
                cost = 200, upgradeType = UpgradeType.MaxHp, intValue = 100, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Armor", description = "Augmente l'armure de la tour de 10.", cost = 300,
                upgradeType = UpgradeType.Armor, intValue = 10, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Regen", description = "Augmente la régénération de la tour de 10 points par seconde.",
                cost = 800, upgradeType = UpgradeType.Regen, intValue = 10, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Attack Speed", description = "Augmente la vitesse d'attaque de la tour de  10 %",
                cost = 1000, upgradeType = UpgradeType.AttackSpeed, floatValue = 1.10f, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Max HP", description = "Augmente les points de vie maximum de la tour de 500.",
                cost = 1000, upgradeType = UpgradeType.MaxHp, intValue = 500, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Armor", description = "Augmente l'armure de la tour de 25.", cost = 600,
                upgradeType = UpgradeType.Armor, intValue = 25, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Regen", description = "Augmente la régénération de la tour de 20 points par seconde.",
                cost = 1600, upgradeType = UpgradeType.Regen, intValue = 20, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Attack Speed", description = "Augmente la vitesse d'attaque de la tour de 20 %",
                cost = 2000, upgradeType = UpgradeType.AttackSpeed, floatValue = 1.20f, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Income", description = "Augmente les revenus passifs de 50 or par seconde.", cost = 500,
                upgradeType = UpgradeType.Income, intValue = 10, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Income", description = "Augmente les revenus passifs de 100 or par seconde.",
                cost = 1000,
                upgradeType = UpgradeType.Income, intValue = 50, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Income", description = "Augmente les revenus passifs de 300 or par seconde.",
                cost = 3000,
                upgradeType = UpgradeType.Income, intValue = 150, itemType = ShopItemType.StatUpgrade
            },
            new ShopItem
            {
                name = "Upgrade Damage +5%",
                description = "Augmente les dégâts de toutes les armes de 5%.",
                cost = 250,
                itemType = ShopItemType.StatUpgrade,
                upgradeType = UpgradeType.Damage,
                floatValue = 1.05f
            },
            new ShopItem
            {
                name = "Arc",
                description = "Ajoute une arme qui tire des flèches.",
                cost = 200,
                itemType = ShopItemType.Weapon,
                weaponData = arrowWeaponData
            },
            new ShopItem
            {
                name = "Hache lancée",
                description = "Ajoute une arme qui lance des haches.",
                cost = 400,
                itemType = ShopItemType.Weapon,
                weaponData = axeWeaponData
            },
            new ShopItem
            {
                name = "Boule de feu",
                description = "Ajoute une arme magique à distance.",
                cost = 1000,
                itemType = ShopItemType.Weapon,
                weaponData = fireballWeaponData
            },
            new ShopItem
            {
                name = "Caillou",
                description = "Ajoute une arme faible mais bon marché.",
                cost = 800,
                itemType = ShopItemType.Weapon,
                weaponData = rockWeaponData
            }
        };
    }
}