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
        statistics.text = "����� �������:"+ player.money+ "\r\n�������� �����:" + player.enemyKill;
    }
    public void Exit()
    {
        Application.Quit();
    }
}