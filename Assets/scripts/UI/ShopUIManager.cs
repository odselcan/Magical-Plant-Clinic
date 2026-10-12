using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIManager : MonoBehaviour
{
    private int magicalWaterPrice = 20;

    private Label moneyLabel;
    private Button buyButton;
    private Button closeButton;

    private bool initialized = false;

    private void OnEnable()
    {
        Debug.Log("SHOP UI MANAGER ÇALIŞTI!");

        InitializeUI();
    }

    private void InitializeUI()
    {
        if (initialized)
        {
            UpdateMoney();
            return;
        }

        UIDocument document = GetComponent<UIDocument>();

        if (document == null)
        {
            Debug.LogError("ShopUI üzerinde UIDocument bulunamadı!");
            return;
        }

        var root = document.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("ShopUI rootVisualElement bulunamadı!");
            return;
        }

        moneyLabel = root.Q<Label>("Money");
        buyButton = root.Q<Button>("BuyButton");
        closeButton = root.Q<Button>("CloseButton");

        Debug.Log("Money bulundu: " + (moneyLabel != null));
        Debug.Log("BuyButton bulundu: " + (buyButton != null));
        Debug.Log("CloseButton bulundu: " + (closeButton != null));

        if (buyButton != null)
        {
            buyButton.clicked += BuyMagicalWater;
        }

        if (closeButton != null)
        {
            closeButton.clicked += CloseShop;
        }

        initialized = true;

        UpdateMoney();
    }

    private void UpdateMoney()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager henüz hazır değil!");
            return;
        }

        if (moneyLabel != null)
        {
            moneyLabel.text = "💰 " + GameManager.Instance.Gold;
        }
    }

    private void BuyMagicalWater()
    {
        Debug.Log("BUY BUTTON ÇALIŞTI!");

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager bulunamadı!");
            return;
        }

        if (GameManager.Instance.HasMagicalWater)
        {
            Debug.Log("Büyülü Su zaten var!");
            return;
        }

        if (GameManager.Instance.SpendGold(magicalWaterPrice))
        {
            GameManager.Instance.AddMagicalWater();

            UpdateMoney();

            Debug.Log("Büyülü Su satın alındı!");
        }
        else
        {
            Debug.Log("Yeterli Gold yok!");
        }
    }

    private void CloseShop()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (!initialized)
        {
            return;
        }

        if (buyButton != null)
        {
            buyButton.clicked -= BuyMagicalWater;
        }

        if (closeButton != null)
        {
            closeButton.clicked -= CloseShop;
        }

        initialized = false;
    }
}