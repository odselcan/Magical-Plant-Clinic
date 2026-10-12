using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Gold { get; private set; } = 100;

    public bool HasMagicalWater { get; private set; } = false;
    public bool HasMoonleaf { get; private set; } = false;

    public bool CanShop { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool SpendGold(int amount)
    {
        if (Gold < amount)
        {
            return false;
        }

        Gold -= amount;
        return true;
    }

    public void AddGold(int amount)
    {
        Gold += amount;

        Debug.Log("Gold eklendi: +" + amount);
        Debug.Log("Güncel Gold: " + Gold);
    }

    public void AddMagicalWater()
    {
        HasMagicalWater = true;
    }

    public void AddMoonleaf()
    {
        HasMoonleaf = true;
    }

    public void ConsumeMaterials()
    {
        HasMagicalWater = false;
        HasMoonleaf = false;

        Debug.Log("Tedavi malzemeleri tüketildi.");
    }

    public void UnlockShops()
    {
        CanShop = true;
    }
}