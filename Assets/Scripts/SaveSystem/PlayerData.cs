using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int money;

    public int levelComplete;

    public int enemyKill;

    public PlayerData(Player player)
    {
        money = player.money;

        levelComplete = player.levelComplete;


        enemyKill = player.enemyKill;
    }
}
