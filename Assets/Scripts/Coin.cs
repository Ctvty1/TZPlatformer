using UnityEngine;
public class Coin : MonoBehaviour
{
    public int count = 1;

    Player player;
    LevelInfo levelInfo;
    public void Start()
    {
        player = FindAnyObjectByType<Player>();
        levelInfo = FindAnyObjectByType<LevelInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
    public void Collect()
    {
        player.audioBase.audioCoin.Play();
        levelInfo.moneyCollect += count;
        levelInfo.MoneyTextUpdate();
        gameObject.SetActive(false);
    }
}