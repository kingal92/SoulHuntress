using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 50;
    public int damage = 10;
    public float moveSpeed = 2.2f;
    public float attackRange = 1.2f;
    public float attackCooldown = 1f;
    public int soulReward = 10;

    protected int currentHealth;
    protected float attackTimer;
    protected Transform target;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected virtual void Update()
    {
        if (target == null)
            return;

        Vector2 direction = (target.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            if (rb != null)
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            else
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackCooldown;
            }
        }

        attackTimer -= Time.deltaTime;
    }

    protected virtual void AttackPlayer()
    {
        if (target == null)
            return;

        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
            player.TakeDamage(damage);
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Defeat();
    }

    protected virtual void Defeat()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddSoul(soulReward);
            GameManager.Instance.OnEnemyDefeated(this);
        }

        Destroy(gameObject);
    }
}
