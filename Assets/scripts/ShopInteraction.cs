using UnityEngine;
using UnityEngine.UIElements;

public class ShopInteraction : MonoBehaviour
{
    [SerializeField] private GameObject shopInteractButton;
    [SerializeField] private UIDocument ShopUI;

    private void Start()
    {
        HideInteractionButton();

        if (ShopUI != null)
        {
            ShopUI.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Oyuncu İksir & Malzeme Dükkanına girdi!");

        if (GameManager.Instance != null &&
            GameManager.Instance.CanShop)
        {
            ShowInteractionButton();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Oyuncu İksir & Malzeme Dükkanından çıktı!");

        HideInteractionButton();
    }

    private void ShowInteractionButton()
    {
        if (shopInteractButton != null)
        {
            shopInteractButton.SetActive(true);
        }
    }

    private void HideInteractionButton()
    {
        if (shopInteractButton != null)
        {
            shopInteractButton.SetActive(false);
        }
    }

    public void OpenShopUI()
    {
        Debug.Log("DÜKKAN BUTONUNA BASILDI!");

        if (ShopUI != null)
        {
            ShopUI.gameObject.SetActive(true);
        }

        HideInteractionButton();
    }
}