using UnityEngine;

public class ContinueTreatmentButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("TEDAVİYE DEVAM ET TIKLANDI!");

        if (UIHandler.Instance != null)
        {
            UIHandler.Instance.OpenTreatmentAgain();
        }

        gameObject.SetActive(false);
    }
}