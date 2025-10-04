using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Анимация игрока")]
    Animator anim;

    [Header("Время для анимации подения")]
    public float landTime;

    [Header("Скорость")]
    float speed;
    [Header("Cкорость перемещения")]
    public float normalSpeed;
    [Header("Высота прыжка")]
    public float jumpForce;

    Rigidbody2D rb;

    public bool isGrounded = true;

    public Transform groundCheak;
    public float cheakRadius = 0.05f;
    public LayerMask whatIsGround;

    private Vector3 scaler;

    //Отмена прыжка при действии
    public bool jampBlock = false;

    //Комбинация атаки
    bool first = false;
    bool second = false;

    //Остановка при анимации второго удара
    public bool attackWaiting;
    //Остановка персонажа при поражении
    public bool heroPause;

    bool jOnce;
    bool onceFall = false;
    AudioBase audioBase;
    private void Start()
    {
        speed = 0f;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioBase = FindAnyObjectByType<AudioBase>();
    }

    private void FixedUpdate()
    {
        if (heroPause == false)
            if (attackWaiting == false)
            {
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        OnLeftButtonDown();
                    }
                    else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                    {
                        OnRightButtonDown();
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    OnButtonUp();
                }

                if (jampBlock == false)
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                    {
                        jampBlock = true;
                        OnJumpButtonDown();
                    }
            }
            else
            {
                speed = 0;
            }

        if (speed != 0f)
        {
            anim.SetBool("isRunning", true);
            attackWaiting = false;
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheak.position, cheakRadius, whatIsGround);
    }

    public void AttackStop()
    {
        if (second == false)
        {
            first = false;
            second = false;
            anim.SetBool("Second", false);
            attackWaiting = false;
        }
    }

    public void AttackOut()
    {
        first = false;
        second = false;
        anim.SetBool("Second", false);
        attackWaiting = false;
    }

    public void OnJumpButtonDown()
    {
        if (isGrounded == true)
        {
            audioBase.audioJump.Play();

            rb.velocity = Vector2.up * jumpForce;

            anim.SetTrigger("TakeOff");
        }
    }

    public void OnLeftButtonDown()
    {
        if (speed >= 0f)
        {
            speed = -normalSpeed;

            Vector3 Scaler = transform.localScale;
            Scaler.x = -1;
            transform.localScale = Scaler;

            scaler = Scaler;
        }
    }

    public void OnRightButtonDown()
    {
        if (speed <= 0f)
        {
            speed = normalSpeed;
            Vector3 Scaler = transform.localScale;
            Scaler.x = 1;
            transform.localScale = Scaler;

            scaler = Scaler;
        }
    }

    public void OnButtonUp()
    {
        speed = 0f;
        anim.SetBool("isRunning", false);
    }
    void Update()
    {
        if (isGrounded == true)
        {
            jampBlock = false;

            anim.SetBool("Fall", false);
            anim.SetBool("isJamp", false);

            StopAllCoroutines();
        }
        else
        {
            anim.SetBool("isJamp", true);

            StartCoroutine(Fall());
        }

        if (onceFall == true)
        {
            onceFall = false;
        }

        if (heroPause == false)
            if (first == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    attackWaiting = true;

                    first = true;
                    audioBase.audioAttack.Play();
                    anim.SetTrigger("First");
                }
            }
            else
            {
                if (second == false)
                    if (Input.GetMouseButtonDown(0))
                    {
                        anim.SetBool("Second", true);
                        audioBase.audioAttack.Play();
                        second = true;
                    }
            }
    }

    IEnumerator Fall()
    {
        if (!jOnce)
        {
            jOnce = true;
        }
        while (true)
        {
            yield return new WaitForSeconds(landTime);
            anim.SetBool("Fall", true);
            onceFall = true;
            attackWaiting = false;
            break;
        }
    }

    public void InstantFall()
    {
        anim.SetBool("Fall", true);
        onceFall = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
    }
    public void AfterDamage()
    {
        first = false;
        second = false;
        attackWaiting = false;
    }
}