
using UnityEngine;
using UnityEngine.EventSystems;

public class Spice : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition; // Чтобы вернуть объект на исходную позицию при неудачном Drop
    [SerializeField] private ItemSlot slot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); // Для управления прозрачностью при перетаскивании
        originalPosition = rectTransform.position;
    }

    // Начало перетаскивания
    public void OnBeginDrag(PointerEventData eventData)
    {

        canvasGroup.blocksRaycasts = false; // Отключаем возможность блокировки лучей, чтобы Drop срабатывал
    }

    // Перетаскивание
    public void OnDrag(PointerEventData eventData)
    {

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        touchPosition.z = 0;
        transform.position = touchPosition;

    }

    // Завершение перетаскивания
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Включаем обратно блокировку лучей
        Debug.Log("DropSpice");
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        touchPosition.z = 0;
        touchPosition.x = -22;
        touchPosition.y = -15;
        transform.localPosition = touchPosition;

        if (slot.Dropped == false)
        {

            rectTransform.position = originalPosition;
        }

        if (slot.Donned == false)
        {

            rectTransform.localPosition = slot.droppedPosition;
        }
    }
    void Update()
    {

    }

}
