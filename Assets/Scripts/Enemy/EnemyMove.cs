using UnityEngine;
public class EnemyMove : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public Animator anim;

    [Header("Патруль")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;

    [Header("Погоня")]
    public float chaseSpeed = 4f;
    public float detectionRadius = 5f;

    [Header("Нападение")]
    public float attackRange = 1f;
    public float attackCooldown = 1.5f;
    public float pauseAfterAttack = 2f;

    private int currentPatrolIndex = 0;
    private Transform player;

    private bool isChasing = false;
    private bool isPaused = false;

    [Header("Задержка")]
    private float lastAttackTime = 0f;
    private float pauseEndTime = 0f;

    HeroInfo hero;
    void Start()
    {
        hero = FindAnyObjectByType<HeroInfo>();
        player = hero.gameObject.transform;
        anim = GetComponent<Animator>();
        enemyInfo = GetComponent<EnemyInfo>();
        currentPatrolIndex = 0;
    }

    void Update()
    {
        if (isPaused)
        {
            if (Time.time >= pauseEndTime)
            {
                isPaused = false;
            }
            else
            {
                return;
            }
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            isChasing = true;
        }
        else if (distanceToPlayer > detectionRadius + 2f)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }
    void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        MoveTowards(targetPoint.position, patrolSpeed);

        anim.SetBool("Run", false);

        FlipSprite(targetPoint.position);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        MoveTowards(player.position, chaseSpeed);

        FlipSprite(player.position);

        anim.SetBool("Run", true);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            TryAttack();
        }
    }

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 newPos = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    void FlipSprite(Vector2 targetPosition)
    {
        if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;

            isPaused = true;
            pauseEndTime = Time.time + pauseAfterAttack;
        }
    }
    void Attack()
    {
        hero.DamageTake(enemyInfo.damage);
    }
}