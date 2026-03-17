using System;
using UnityEngine;

public class Economy : MonoBehaviour
{
    [SerializeField] private int startingGold = 5000;
    [SerializeField] private ShopUI shopUI;

    public int Gold { get; private set; }
    public int PassiveIncome { get; set; } = 10;
    public int KillIncome { get; private set; } = 5;

    void Awake()
    {
        Gold = startingGold;
    }

    private void Start()
    {
        InvokeRepeating(nameof(GeneratePassiveIncome), 1f, 1f);
    }


    /// <summary>
    /// Tente de payer le montant spécifié. Déduit le montant de l'or du joueur si le paiement est réussi.
    /// </summary>
    /// <param name="amout">Le montant à payer</param>
    /// <returns>True si le paiement s'est bien exécuté, false sinon</returns>
    public bool Pay(int amout)
    {
        if (Gold >= amout)
        {
            Gold -= amout;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Ajoute le montant spécifié à l'or du joueur. Utilisé pour les revenus passifs et les récompenses de kill.
    /// </summary>
    /// <param name="amout">Le montant d'or à ajouter</param>
    public void AddGold(int amout)
    {
        Gold += amout;
    }

    /// <summary>
    /// Génère le revenu passif du joueur.
    /// </summary>
    private void GeneratePassiveIncome()
    {
        if (PassiveIncome > 0)
        {
            AddGold(PassiveIncome);
            shopUI.UpdateHeader(Gold);
        }
    }
}