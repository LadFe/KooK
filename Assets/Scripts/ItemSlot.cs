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
    
    

    
    // ����� ��� �������� ���
    private bool foodDone = false;
    private bool foodVaped = false;
    private bool dropped = false;
    // ������ ��� �������� �������� ��������� ��������� ���
    public Sprite[] foodSprites; // 0: ������� ���, 1: ��������� ���

    // ������ ��� �������� ������ �� UI-�������� Image (���� � ��� ��������� ���������)
    public Image[] itemImages;

    // ������ �� ��������������� ������
    private Image currentItemImage;
    


    // ������ �� �������� Vaping ��� �� ���������
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
            
            // ���������� ������ � ����
            RectTransform draggedObject = eventData.pointerDrag.GetComponent<RectTransform>();     
            draggedObject.SetParent(transform);
            draggedObject.localPosition = Vector2.zero;
            droppedPosition = draggedObject.localPosition;

            // �������� Image ���������� ���������������� �������
            currentItemImage = draggedObject.GetComponent<Image>();


            // ��������� �������� Cooking
            StartCoroutine(Cooking(droppedItem));

            // ��������� �������� Vaping � ��������� ������ ��� ����������� ���������
            vapingCoroutine = StartCoroutine(Vaping(droppedItem));
        }
    }

    // ������� ��� �������
    private IEnumerator Cooking(GameObject droppedItem)
    {

        yield return new WaitForSeconds(2f);
        

        // ������ ������ �� ������� ��� (������ 1)
        if (currentItemImage != null)
        {
            currentItemImage.sprite = foodSprites[0];
        }

        // ��������� 1 � itemID ��������, ���� �� ����� ��������� Item
        Item itemComponent = droppedItem.GetComponent<Item>();
        if (itemComponent != null)
        {
            itemComponent.itemID += 1;
            Debug.Log($"ID �������� ����� �������������: {itemComponent.itemID}");
        }
        foodDone = true;
    }

    // ������� ��� �������� ���
    private IEnumerator Vaping(GameObject droppedItem)
    {
        yield return new WaitForSeconds(4f);

        if (!foodVaped && foodDone)
        {

            // ������ ������ �� ��������� ��� (������ 2)
            if (currentItemImage != null)
            {
                currentItemImage.sprite = foodSprites[1];
            }
            Item itemComponent = droppedItem.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.itemID += 1;
                Debug.Log($"ID �������� ����� �������������: {itemComponent.itemID}");
            }
            foodVaped = true;
        }
    }

    public void StopCouru()
    {
        if (vapingCoroutine != null)
        {
            StopCoroutine(vapingCoroutine); // ������������� �������� ��������
            vapingCoroutine = null; // ���������� ������ ����� ���������
        }
    }

    private void OnEnable()
    {
        // ������������� �� ������� � ��������� Action<>
        PlateSlot.stopCourutine += StopCouru;
    }

    private void OnDisable()
    {
        // ������������ �� �������
        PlateSlot.stopCourutine -= StopCouru;
        
    }

    void Update()
    {
        
    }
}
