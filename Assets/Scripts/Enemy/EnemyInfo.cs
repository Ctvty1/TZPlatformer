using System.Collections;
using UnityEngine;
public class EnemyInfo : MonoBehaviour
{
    public int hp = 3;
    public int damage = 5;

    Animator anim;

    LevelInfo levelInfo;
    ParticleSystem particle;
    AudioBase audioBase;
    private void Start()
    {
        levelInfo = FindAnyObjectByType<LevelInfo>();
        audioBase = FindAnyObjectByType<AudioBase>();

        particle = GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Attack"))
        {
            anim.SetTrigger("Hit");

            hp--;
            particle.Play();
            DeadCheak();
        }

        if (collision.GetComponent<DeadZone>())
        {
            hp = 0;
            DeadCheak();
        }
    }
    void DeadCheak()
    {
        if (hp <= 0)
        {
            levelInfo.enemyKill++;
            audioBase.audioKill.Play();
            gameObject.SetActive(false);
        }
    }
}