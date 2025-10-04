using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public Text statistics;

    Player player;
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        Time.timeScale = 1f;
    }
    public void TextUpdate()
    {
        statistics.text = "Монет собрано:"+ player.money+ "\r\nМонстров убито:" + player.enemyKill;
    }
    public void Exit()
    {
        Application.Quit();
    }
}