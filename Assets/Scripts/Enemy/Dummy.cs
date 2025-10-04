using UnityEngine;
public class Dummy : MonoBehaviour
{
    Animator anim;
    ParticleSystem particle;
    public void Start()
    {
        anim = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            anim.SetTrigger("Hit");
            particle.Play();
        }
    }
}