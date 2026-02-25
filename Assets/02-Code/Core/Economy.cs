using UnityEngine;

public class Economy : MonoBehaviour
{
    
    [SerializeField] private int startingGold = 5000;
    
    public int Gold { get; private set; }
    
    void Awake()
    {
        Gold = startingGold;
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
}
