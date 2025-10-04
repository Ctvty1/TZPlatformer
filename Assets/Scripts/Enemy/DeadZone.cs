using UnityEngine;
public class DeadZone : MonoBehaviour
{
    LevelChanger levelChanger;
    AudioBase audioBase;
    LevelInfo levelInfo;
    private void Start()
    {
        levelChanger = FindAnyObjectByType<LevelChanger>();
        audioBase = FindAnyObjectByType<AudioBase>();
        levelInfo = FindAnyObjectByType<LevelInfo>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            levelInfo.keyBlock = true;
            levelChanger.GameFall();
            audioBase.audioFall.Play();
        }
    }
}