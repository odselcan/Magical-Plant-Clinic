
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        UpdateMoney();
    }

    private void Update()
    {
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        if (moneyText != null)
        {
            moneyText.text =
                GameManager.Instance.Gold.ToString();
        }
    }
}