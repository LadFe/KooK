using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Item meat;
    [SerializeField] private Item meat2;
    [SerializeField] private Item meat3;
    [SerializeField] private Item Fish1;
    [SerializeField] private Item Fish2;
    [SerializeField] private Item Fish3;

    [SerializeField] private PlateSlot plateSlot;
    
    

    
    // Флаги для статусов еды
    private bool foodDone = false;
    private bool foodVaped = false;
    private bool dropped = false;
    // Массив для хранения спрайтов различных состояний еды
    public Sprite[] foodSprites; // 0: готовая еда, 1: сгоревшая еда

    // Массив для хранения ссылок на UI-элементы Image (если у вас несколько предметов)
    public Image[] itemImages;

    // Ссылка на перетаскиваемый объект
    private Image currentItemImage;
    


    // Ссылка на корутину Vaping для ее остановки
    private Coroutine vapingCoroutine;
    public bool Dropped
    {
        get { return dropped; }
        set { dropped = value; }
    }

    public bool Donned
    {
        get { return foodDone; }
        set { foodDone = value; }
    }

    public bool Vapped
    {
        get { return foodVaped; }
        set { foodVaped = value; }
    }

    public Vector3 droppedPosition;

    public void OnDrop(PointerEventData eventData)
    {

        Item draggedObj = eventData.pointerDrag.GetComponent<Item>();

        if (eventData.pointerDrag != null && draggedObj.itemID == 10 || draggedObj.itemID == 20 || draggedObj.itemID == 30 || draggedObj.itemID == 40 || draggedObj.itemID == 50 || draggedObj.itemID == 60) //foodDone == false && foodVaped == false &&
        {
            GameObject droppedItem = eventData.pointerDrag;
            Debug.Log("DROP");
            dropped = true;
            
            // Перемещаем объект в слот
            RectTransform draggedObject = eventData.pointerDrag.GetComponent<RectTransform>();     
            draggedObject.SetParent(transform);
            draggedObject.localPosition = Vector2.zero;
            droppedPosition = draggedObject.localPosition;

            // Получаем Image компонента перетаскиваемого объекта
            currentItemImage = draggedObject.GetComponent<Image>();


            // Запускаем корутину Cooking
            StartCoroutine(Cooking(droppedItem));

            // Запускаем корутину Vaping и сохраняем ссылку для возможности остановки
            vapingCoroutine = StartCoroutine(Vaping(droppedItem));
        }
    }

    // Корутин для готовки
    private IEnumerator Cooking(GameObject droppedItem)
    {

        yield return new WaitForSeconds(2f);
        

        // Меняем спрайт на готовую еду (индекс 1)
        if (currentItemImage != null)
        {
            currentItemImage.sprite = foodSprites[0];
        }

        // Добавляем 1 к itemID предмета, если он имеет компонент Item
        Item itemComponent = droppedItem.GetComponent<Item>();
        if (itemComponent != null)
        {
            itemComponent.itemID += 1;
            Debug.Log($"ID предмета после приготовления: {itemComponent.itemID}");
        }
        foodDone = true;
    }

    // Корутин для сжигания еды
    private IEnumerator Vaping(GameObject droppedItem)
    {
        yield return new WaitForSeconds(4f);

        if (!foodVaped && foodDone)
        {

            // Меняем спрайт на сгоревшую еду (индекс 2)
            if (currentItemImage != null)
            {
                currentItemImage.sprite = foodSprites[1];
            }
            Item itemComponent = droppedItem.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.itemID += 1;
                Debug.Log($"ID предмета после приготовления: {itemComponent.itemID}");
            }
            foodVaped = true;
        }
    }

    public void StopCouru()
    {
        if (vapingCoroutine != null)
        {
            StopCoroutine(vapingCoroutine); // Останавливаем активную корутину
            vapingCoroutine = null; // Сбрасываем ссылку после остановки
        }
    }

    private void OnEnable()
    {
        // Подписываемся на событие с делегатом Action<>
        PlateSlot.stopCourutine += StopCouru;
    }

    private void OnDisable()
    {
        // Отписываемся от события
        PlateSlot.stopCourutine -= StopCouru;
        
    }

    void Update()
    {
        
    }
}
