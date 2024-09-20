using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] private ItemSlot slot;
    [SerializeField] private PlateSlot plateSlot;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition ; // Чтобы вернуть объект на исходную позицию при неудачном Drop
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); // Для управления прозрачностью при перетаскивании
        originalPosition = rectTransform.position;//rectTransform.anchoredPosition;
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

        // Если не было успешного Drop в слот, возвращаем объект на исходную позицию
        if (slot.Dropped == false )
        {

            rectTransform.position = originalPosition;
        }

        
    }
    void Update()
    {

    }
    
}
