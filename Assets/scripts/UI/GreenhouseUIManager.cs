using UnityEngine;
using UnityEngine.UIElements;

public class GreenhouseUIManager : MonoBehaviour
{
    private int moonleafPrice = 10;

    private Label moneyLabel;
    private Button buyButton;
    private Button closeButton;

    private bool initialized = false;

    private void OnEnable()
    {
        Debug.Log("GREENHOUSE UI MANAGER ÇALIŞTI!");

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
            Debug.LogError("GreenhouseUI üzerinde UIDocument bulunamadı!");
            return;
        }

        var root = document.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("GreenhouseUI rootVisualElement bulunamadı!");
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
            buyButton.clicked += BuyMoonleaf;
        }

        if (closeButton != null)
        {
            closeButton.clicked += CloseGreenhouse;
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

    private void BuyMoonleaf()
    {
        Debug.Log("MOONLEAF BUY BUTTON ÇALIŞTI!");

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager bulunamadı!");
            return;
        }

        if (GameManager.Instance.HasMoonleaf)
        {
            Debug.Log("Ay Yaprağı zaten var!");
            return;
        }

        if (GameManager.Instance.SpendGold(moonleafPrice))
        {
            GameManager.Instance.AddMoonleaf();

            UpdateMoney();

            Debug.Log("Ay Yaprağı satın alındı!");
        }
        else
        {
            Debug.Log("Yeterli Gold yok!");
        }
    }

    private void CloseGreenhouse()
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
            buyButton.clicked -= BuyMoonleaf;
        }

        if (closeButton != null)
        {
            closeButton.clicked -= CloseGreenhouse;
        }

        initialized = false;
    }
}