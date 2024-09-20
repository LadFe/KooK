
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text textMoney;
    [SerializeField] private ItemSlot slot;
    
    
    public int money = 0;

    // ����� ������ ��������� int ��� ��������
    public void PayReciept(int amount)
    {
        
            money += amount;  // ��������� ���������� ���������� �����
       
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
        // ������������� �� ������� � ��������� Action<int>
       // Quest.OnQuestCompleted += PayReciept;
    }

    private void OnDisable()
    {
        // ������������ �� �������
       // Quest.OnQuestCompleted -= PayReciept;
    }
}


