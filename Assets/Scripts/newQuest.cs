using UnityEngine;

public class NewQuest : MonoBehaviour
{
    public int[] plateID = { 666, 777, 888, 999 };  // Массив с тарелками
    public int[] foodID = { 11, 21, 31, 41, 51, 61 };  // Массив с едой
    public int[] spiceID = { 111, 222 };  // Массив со специями

    public int currentPlateID;
    public int currentFoodID;
    public int currentSpiceID;

    public int playerFoodID;  // ID еды, перенесенной игроком
    public int playerSpiceID;  // ID специи, перенесенной игроком

    private bool questCompleted = false;

    // Метод генерации случайного квеста
    public void GenerateRandomQuest()
    {
        currentPlateID = plateID[Random.Range(0, plateID.Length)];
        currentFoodID = foodID[Random.Range(0, foodID.Length)];
        currentSpiceID = spiceID[Random.Range(0, spiceID.Length)];

        Debug.Log($"Сгенерированный квест: Тарелка = {currentPlateID}, Еда = {currentFoodID}, Специя = {currentSpiceID}");
    }

    // Метод для обновления состояния игрока (перенос еды и/или специй)
    public void PlayerTransfersItems(int plate, int food, int spice)
    {
        // Проверяем, что еду или специю перенесли на нужную тарелку
        if (plate == currentPlateID)
        {
            // Если игрок перенес еду
            if (food != 0)
            {
                playerFoodID = food;
                Debug.Log($"Еда с ID {food} была положена в тарелку с ID {plate}");
            }

            // Если игрок перенес специю
            if (spice != 0)
            {
                playerSpiceID = spice;
                Debug.Log($"Специя с ID {spice} была положена в тарелку с ID {plate}");
            }

            // Проверяем выполнение квеста
            CheckQuestCompletion();
        }
        else
        {
            Debug.Log("Еда или специя положены не на ту тарелку.");
        }
    }

    // Метод для проверки выполнения квеста
    public void CheckQuestCompletion()
    {
        if (playerFoodID == currentFoodID && playerSpiceID == currentSpiceID)
        {
            if (!questCompleted)
            {
                questCompleted = true;
                GiveReward();  // Выдаём награду
            }
        }
        else
        {
            Debug.Log("Квест не выполнен. Еда или специя не совпадают.");
        }
    }

    // Метод для выдачи награды
    public void GiveReward()
    {
        Debug.Log("Квест выполнен! Награда выдана.");
        // Здесь добавьте логику для выдачи награды (например, добавление очков или предметов)
    }

    private void Start()
    {
        GenerateRandomQuest();  // Генерация квеста при старте игры
    }
}
