using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfo : MonoBehaviour
{
    [Header("Максимальное здоровье")]
    public int maxHP;
    [Header("Текущее здоровье")]
    public int currentHP;

    [Header("Полоска здоровья")]
    public Slider hpSlider;
    [Header("Неуязвимость")]
    public float shieldTime;
    bool shieldBool;

    Animator anim;
    AudioBase audioBase;
    LevelChanger levelChanger;
    LevelInfo levelInfo;
    PlayerController playerController;
    void Start()
    {
        anim = GetComponent<Animator>();
        audioBase = FindAnyObjectByType<AudioBase>();
        levelChanger = FindAnyObjectByType<LevelChanger>();
        levelInfo = FindAnyObjectByType<LevelInfo>();
        playerController = GetComponent<PlayerController>();

        currentHP = maxHP;

        hpSlider.maxValue = maxHP;
        hpSlider.minValue = 0;

        hpSlider.value = currentHP;
    }

    public void DamageTake(int damage)
    {
        if (shieldBool == false && currentHP > 0 && levelInfo.win == false)
        {
            currentHP -= damage;
            if(currentHP > 0)
            {
                shieldBool = true;
                anim.SetTrigger("Damage");

                audioBase.audioHit.Play();

                HPBarUpdate();

                StartCoroutine(Shield());
            }
            else
            {
                HPBarUpdate();
            }
        }
    }
    public void HPBarUpdate()
    {
        hpSlider.value = currentHP;

        if(currentHP <= 0)
        {
            if(audioBase.audioGame !=  null)
                audioBase.audioGame.Stop();

            anim.SetTrigger("Dead");
            StartCoroutine(Dead());
        }
    }

    IEnumerator Shield()
    {
        playerController.AfterDamage();
        yield return new WaitForSeconds(shieldTime);
        shieldBool = false;
    }
     
    IEnumerator Dead()
    {
        playerController.heroPause = true;
        playerController.OnButtonUp();
        yield return new WaitForSeconds(1.25f);
        levelChanger.RestartLevel();
    }
}