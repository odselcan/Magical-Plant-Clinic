using UnityEngine;
using UnityEngine.EventSystems;

public class OrderButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Tıklandığında veya mobilde dokunulduğunda çalışır
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Balona tıklandı!");

        if (UIHandler.Instance != null)
        {
            UIHandler.Instance.DisplayDialogue();
        }

        // Tıklanınca imleci normale döndür ve balonu kapat
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameObject.SetActive(false);
    }

    // Fare balonun üzerine geldiğinde (PC için buton hissi verir)
    public void OnPointerEnter(PointerEventData eventData)
    {
        // İstersen buraya buton büyütme efekti de koyabilirsin:
        transform.localScale *= 1.1f; 
    }

    // Fare balonun üzerinden çekildiğinde
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale /= 1.1f;
    }

    // Eski tarz OnMouseDown da yedek olarak dursun (EventSystem takılırsa çalışır)
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown ile tıklandı!");
        if (UIHandler.Instance != null)
        {
            UIHandler.Instance.DisplayDialogue();
        }
        gameObject.SetActive(false);
    }
}