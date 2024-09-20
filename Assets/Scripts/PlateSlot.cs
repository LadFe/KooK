using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateSlot : MonoBehaviour, IDropHandler
{
    public static event Action stopCourutine;

    public Vector3 droppedPosition;
    public int plateID;  // Уникальный ID тарелки
    [SerializeField] NewQuest quest;  // Ссылка на скрипт квеста

    // Этот метод вызывается, когда игрок отпускает предмет в тарелку
    public void OnDrop(PointerEventData eventData)
    {
        stopCourutine?.Invoke();

        RectTransform draggedObject = eventData.pointerDrag.GetComponent<RectTransform>();
        draggedObject.SetParent(transform);
        draggedObject.localPosition = Vector2.zero;
        droppedPosition = draggedObject.localPosition;


        // Получаем объект, который перетаскивали
        GameObject droppedItem = eventData.pointerDrag;
        Debug.Log(droppedItem);
        if (droppedItem != null)
        {
            // Получаем компонент Item у перетаскиваемого предмета (еда или специя)
            Item itemComponent = droppedItem.GetComponent<Item>();
            Debug.Log($"С Предмета {itemComponent}  взят ИТЕМ" );
            if (itemComponent != null)
            {
                // Выводим информацию для отладки
                Debug.Log($"Предмет с ID {itemComponent.itemID} был помещен в тарелку с ID {plateID}");

                // Вызываем проверку: ID тарелки, ID предмета
                // В зависимости от логики, определяем, является ли этот предмет едой или специей
                if (IsFood(itemComponent))
                {
                    quest.PlayerTransfersItems(plateID, itemComponent.itemID, 0);  // Если это еда, специя = 0
                }
                else if (IsSpice(itemComponent))
                {
                    quest.PlayerTransfersItems(plateID, 0, itemComponent.itemID);  // Если это специя, еда = 0
                }
            }
        }
    }

    // Метод для определения, является ли предмет едой
    private bool IsFood(Item item)
    {
        // Пример логики: определяем, что ID 1-100 — это еда
        return item.itemID >= 11 && item.itemID <= 61;  // Здесь ваш диапазон ID для еды
    }

    // Метод для определения, является ли предмет специей
    private bool IsSpice(Item item)
    {
        // Пример логики: определяем, что ID 101-200 — это специи
        return item.itemID == 111 || item.itemID == 222;  // Здесь ваш диапазон ID для специй
    }
}
