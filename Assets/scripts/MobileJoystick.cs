using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler
{
    public static Vector2 InputDirection { get; private set; }

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        InputDirection = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateInput(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateInput(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero;
    }

    private void UpdateInput(PointerEventData eventData)
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        Vector2 halfSize = rectTransform.rect.size / 2f;

        Vector2 direction = new Vector2(
            localPoint.x / halfSize.x,
            localPoint.y / halfSize.y
        );

        InputDirection = Vector2.ClampMagnitude(
            direction,
            1f
        );
    }
}