using UnityEngine;

public class NewQuest : MonoBehaviour
{
    public int[] plateID = { 666, 777, 888, 999 };  // ������ � ���������
    public int[] foodID = { 11, 21, 31, 41, 51, 61 };  // ������ � ����
    public int[] spiceID = { 111, 222 };  // ������ �� ��������

    public int currentPlateID;
    public int currentFoodID;
    public int currentSpiceID;

    public int playerFoodID;  // ID ���, ������������ �������
    public int playerSpiceID;  // ID ������, ������������ �������

    private bool questCompleted = false;

    // ����� ��������� ���������� ������
    public void GenerateRandomQuest()
    {
        currentPlateID = plateID[Random.Range(0, plateID.Length)];
        currentFoodID = foodID[Random.Range(0, foodID.Length)];
        currentSpiceID = spiceID[Random.Range(0, spiceID.Length)];

        Debug.Log($"��������������� �����: ������� = {currentPlateID}, ��� = {currentFoodID}, ������ = {currentSpiceID}");
    }

    // ����� ��� ���������� ��������� ������ (������� ��� �/��� ������)
    public void PlayerTransfersItems(int plate, int food, int spice)
    {
        // ���������, ��� ��� ��� ������ ��������� �� ������ �������
        if (plate == currentPlateID)
        {
            // ���� ����� ������� ���
            if (food != 0)
            {
                playerFoodID = food;
                Debug.Log($"��� � ID {food} ���� �������� � ������� � ID {plate}");
            }

            // ���� ����� ������� ������
            if (spice != 0)
            {
                playerSpiceID = spice;
                Debug.Log($"������ � ID {spice} ���� �������� � ������� � ID {plate}");
            }

            // ��������� ���������� ������
            CheckQuestCompletion();
        }
        else
        {
            Debug.Log("��� ��� ������ �������� �� �� �� �������.");
        }
    }

    // ����� ��� �������� ���������� ������
    public void CheckQuestCompletion()
    {
        if (playerFoodID == currentFoodID && playerSpiceID == currentSpiceID)
        {
            if (!questCompleted)
            {
                questCompleted = true;
                GiveReward();  // ����� �������
            }
        }
        else
        {
            Debug.Log("����� �� ��������. ��� ��� ������ �� ���������.");
        }
    }

    // ����� ��� ������ �������
    public void GiveReward()
    {
        Debug.Log("����� ��������! ������� ������.");
        // ����� �������� ������ ��� ������ ������� (��������, ���������� ����� ��� ���������)
    }

    private void Start()
    {
        GenerateRandomQuest();  // ��������� ������ ��� ������ ����
    }
}
