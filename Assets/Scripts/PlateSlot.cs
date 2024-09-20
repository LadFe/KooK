using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateSlot : MonoBehaviour, IDropHandler
{
    public static event Action stopCourutine;

    public Vector3 droppedPosition;
    public int plateID;  // ���������� ID �������
    [SerializeField] NewQuest quest;  // ������ �� ������ ������

    // ���� ����� ����������, ����� ����� ��������� ������� � �������
    public void OnDrop(PointerEventData eventData)
    {
        stopCourutine?.Invoke();

        RectTransform draggedObject = eventData.pointerDrag.GetComponent<RectTransform>();
        draggedObject.SetParent(transform);
        draggedObject.localPosition = Vector2.zero;
        droppedPosition = draggedObject.localPosition;


        // �������� ������, ������� �������������
        GameObject droppedItem = eventData.pointerDrag;
        Debug.Log(droppedItem);
        if (droppedItem != null)
        {
            // �������� ��������� Item � ���������������� �������� (��� ��� ������)
            Item itemComponent = droppedItem.GetComponent<Item>();
            Debug.Log($"� �������� {itemComponent}  ���� ����" );
            if (itemComponent != null)
            {
                // ������� ���������� ��� �������
                Debug.Log($"������� � ID {itemComponent.itemID} ��� ������� � ������� � ID {plateID}");

                // �������� ��������: ID �������, ID ��������
                // � ����������� �� ������, ����������, �������� �� ���� ������� ���� ��� �������
                if (IsFood(itemComponent))
                {
                    quest.PlayerTransfersItems(plateID, itemComponent.itemID, 0);  // ���� ��� ���, ������ = 0
                }
                else if (IsSpice(itemComponent))
                {
                    quest.PlayerTransfersItems(plateID, 0, itemComponent.itemID);  // ���� ��� ������, ��� = 0
                }
            }
        }
    }

    // ����� ��� �����������, �������� �� ������� ����
    private bool IsFood(Item item)
    {
        // ������ ������: ����������, ��� ID 1-100 � ��� ���
        return item.itemID >= 11 && item.itemID <= 61;  // ����� ��� �������� ID ��� ���
    }

    // ����� ��� �����������, �������� �� ������� �������
    private bool IsSpice(Item item)
    {
        // ������ ������: ����������, ��� ID 101-200 � ��� ������
        return item.itemID == 111 || item.itemID == 222;  // ����� ��� �������� ID ��� ������
    }
}
