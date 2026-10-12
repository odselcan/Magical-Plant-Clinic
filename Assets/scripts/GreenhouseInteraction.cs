using UnityEngine;
using UnityEngine.UIElements;

public class GreenhouseInteraction : MonoBehaviour
{
    [SerializeField] private GameObject greenhouseInteractButton;
    [SerializeField] private UIDocument greenhouseUI;

    private void Start()
    {
        HideInteractionButton();

        if (greenhouseUI != null)
        {
            greenhouseUI.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Oyuncu Sihirli Sera alanına girdi!");

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

        Debug.Log("Oyuncu Sihirli Sera alanından çıktı!");

        HideInteractionButton();
    }

    private void ShowInteractionButton()
    {
        if (greenhouseInteractButton != null)
        {
            greenhouseInteractButton.SetActive(true);
        }
    }

    private void HideInteractionButton()
    {
        if (greenhouseInteractButton != null)
        {
            greenhouseInteractButton.SetActive(false);
        }
    }

    public void OpenGreenhouse()
    {
        if (greenhouseUI != null)
        {
            greenhouseUI.gameObject.SetActive(true);
        }

        HideInteractionButton();
    }
}