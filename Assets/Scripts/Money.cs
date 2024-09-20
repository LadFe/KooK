
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text textMoney;
    [SerializeField] private ItemSlot slot;
    
    
    public int money = 0;

    // Метод теперь принимает int как параметр
    public void PayReciept(int amount)
    {
        
            money += amount;  // Добавляем переданное количество денег
       
    }

    public void UpdateMoneyUI()
    {
        string formattedScore = money.ToString();
        textMoney.text = formattedScore;
    }

    public void Update()
    {
        UpdateMoneyUI();
    }

    private void OnEnable()
    {
        // Подписываемся на событие с делегатом Action<int>
       // Quest.OnQuestCompleted += PayReciept;
    }

    private void OnDisable()
    {
        // Отписываемся от события
       // Quest.OnQuestCompleted -= PayReciept;
    }
}


