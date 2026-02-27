using UnityEngine;

public class Economy : MonoBehaviour
{
    
    [SerializeField] private int startingGold = 5000;
    [SerializeField] private ShopUI shopUI;
    
    public int Gold { get; private set; }
    public int PassiveIncome { get; set; } = 0;
    public int KillIncome { get; private set; } = 0;
    
    void Awake()
    {
        Gold = startingGold;
    }

    private void Start()
    {
        InvokeRepeating(nameof(GeneratePassiveIncome), 1f, 1f);
    }
    

    public bool Pay(int amout)
    {
        if (Gold >= amout)
        {
            Gold -= amout;
            return true;
        }

        return false;
    }

    public void AddGold(int amout)
    {
        Gold += amout;
    }
    
    private void GeneratePassiveIncome()
    {
        if (PassiveIncome > 0)
        {
            AddGold(PassiveIncome);
            shopUI.UpdateHeader(Gold);
        }
    }
}
