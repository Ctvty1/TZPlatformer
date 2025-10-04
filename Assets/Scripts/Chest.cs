using UnityEngine;

public class Chest : MonoBehaviour
{
    public int moneyCount;

    public Sprite spriteOpen;

    bool open;

    Animator anim;
    SpriteRenderer spriteRenderer;
    LevelInfo levelInfo;
    ParticleSystem particle;
    Player player;
    public void Start()
    {
        anim = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindAnyObjectByType<Player>();
        levelInfo = FindAnyObjectByType<LevelInfo>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!open)
            if (collision.CompareTag("Player"))
            {
                Interaction interaction = collision.gameObject.GetComponent<Interaction>();

                interaction.chest = this;
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!open)
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Interaction>().chest = null;
            }
    }

    public void Open()
    {
        open = true;

        particle.Play();
        player.audioBase.audioChest.Play();

        anim.SetTrigger("Open");
        spriteRenderer.sprite = spriteOpen;

        levelInfo.moneyCollect += moneyCount;
        levelInfo.MoneyTextUpdate();

    }
}