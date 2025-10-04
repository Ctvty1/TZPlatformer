using UnityEngine;
public class Player : MonoBehaviour
{
    public int money;
    public int levelComplete;
    public int enemyKill;

    public AudioBase audioBase;
    public void Start()
    {
        //Загрузка сохранения
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            money = data.money;
            levelComplete = data.levelComplete;
            enemyKill = data.enemyKill;
        }
        else
        {
            Save();
        }
    }

    //Сохранение данных
    public void Save()
    {
        SaveSystem.SavePlayer(this);
    }
}