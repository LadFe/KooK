using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] private ItemSlot slot;
    [SerializeField] private PlateSlot plateSlot;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition ; // ����� ������� ������ �� �������� ������� ��� ��������� Drop
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); // ��� ���������� ������������� ��� ��������������
        originalPosition = rectTransform.position;//rectTransform.anchoredPosition;
    }

    // ������ ��������������
    public void OnBeginDrag(PointerEventData eventData)
    {
       
        canvasGroup.blocksRaycasts = false; // ��������� ����������� ���������� �����, ����� Drop ����������
    }

    // ��������������
    public void OnDrag(PointerEventData eventData)
    {
        
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        touchPosition.z = 0;
        transform.position = touchPosition; 

    }

    // ���������� ��������������
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // �������� ������� ���������� �����

        // ���� �� ���� ��������� Drop � ����, ���������� ������ �� �������� �������
        if (slot.Dropped == false )
        {

            rectTransform.position = originalPosition;
        }

        
    }
    void Update()
    {

    }
    
}
