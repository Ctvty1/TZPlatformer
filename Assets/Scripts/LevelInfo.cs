using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    public int level;

    public int moneyCollect;
    public int enemyKill;

    public GameObject panelWin;
    public GameObject pausePanel;

    public Text moneyText;

    public Text moneyCollectText;
    public Text enemyKillText;

    public bool win;
    bool pause;

    [HideInInspector]
    public bool keyBlock;

    PlayerController playerController;
    Player player;
    public void Start()
    {
        player = FindAnyObjectByType<Player>();
        playerController = FindAnyObjectByType<PlayerController>();
        MoneyTextUpdate();

        Time.timeScale = 1f;
    }

    public void MoneyTextUpdate()
    {
        moneyText.text = (player.money + moneyCollect).ToString();
    }

    public void Update()
    {
        if (keyBlock == false)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (win != true)
                    Pause();
            }
    }

    public void Finish()
    {
        if (!win)
        {
            panelWin.SetActive(true);
            player.audioBase.audioWin.Play();

            playerController.heroPause = true;
            playerController.OnRightButtonDown();

            moneyCollectText.text = "Монет собрано: " + moneyCollect.ToString();
            enemyKillText.text = "Убито монстров: " + enemyKill.ToString();

            player.money += moneyCollect;
            player.enemyKill += enemyKill;

            if (player.levelComplete < level)
                player.levelComplete = level;

            player.Save();
            win = true;
        }
    }
    public void Pause()
    {
        pause = !pause;

        if (pause)
        {
            pausePanel.SetActive(true);
            playerController.heroPause = true;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            playerController.heroPause = false;
        }
    }
}