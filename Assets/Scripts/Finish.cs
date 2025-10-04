using UnityEngine;

public class Finish : MonoBehaviour
{
    LevelInfo levelInfo;
    void Start()
    {
        levelInfo = FindAnyObjectByType<LevelInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelInfo.Finish();
        }
    }
}