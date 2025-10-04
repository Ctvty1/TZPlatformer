using UnityEngine;
public class Traps : MonoBehaviour
{
    public int damage = 5;

    AudioBase audioBase;
    HeroInfo heroInfo;
    private void Start()
    {
        audioBase = FindAnyObjectByType<AudioBase>();
        heroInfo = FindAnyObjectByType<HeroInfo>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioBase.audioAttack.Play();
            heroInfo.DamageTake(damage);
        }
    }
}