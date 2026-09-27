using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Combat")]
    public int attackDamage = 20;
    public float attackRange = 1.2f;
    public float attackCooldown = 0.4f;
    public float projectileSpeed = 8f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public GameObject projectilePrefab;

    private Rigidbody2D rb;
    private float attackTimer;
    private Vector2 moveInput;
    private Vector2 facingDirection = Vector2.right;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y).normalized;

        if (moveInput != Vector2.zero)
            facingDirection = moveInput.normalized;

        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
            Attack();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.K))
            CastProjectile();
    }

    private void FixedUpdate()
    {
        if (rb != null)
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        if (attackTimer > 0f)
            return;

        attackTimer = attackCooldown;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();
            if (enemy != null)
                enemy.TakeDamage(attackDamage);
        }
    }

    public void CastProjectile()
    {
        if (projectilePrefab == null)
            return;

        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
        Projectile spell = projectile.GetComponent<Projectile>();
        if (spell != null)
        {
            spell.damage = attackDamage;
            spell.speed = projectileSpeed;
            spell.direction = facingDirection;
        }
    }

    public void TakeDamage(int amount)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.PlayerTakeDamage(amount);
    }

    public void UpgradeDamage(int amount)
    {
        attackDamage += amount;
    }

    public void UpgradeMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public void UpgradeMaxHealth(int amount)
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.maxPlayerHealth += amount;
        GameManager.Instance.HealPlayer(amount);
    }

    public void ResetCharacter()
    {
        transform.position = Vector3.zero;
        moveSpeed = 5f;
        attackDamage = 20;
        attackRange = 1.2f;
        attackCooldown = 0.4f;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
