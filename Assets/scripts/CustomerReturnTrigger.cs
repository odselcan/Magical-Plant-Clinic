using UnityEngine;

public class CustomerReturnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (GameManager.Instance != null &&
            GameManager.Instance.CanShop)
        {
            if (continueButton != null)
            {
                continueButton.SetActive(true);
            }

            Debug.Log("NPC'ye geri dönüldü!");
            Debug.Log("Tedaviye Devam Et butonu açıldı.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }
    }
}