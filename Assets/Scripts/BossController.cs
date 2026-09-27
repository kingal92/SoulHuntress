using UnityEngine;

public class BossController : EnemyController
{
    public int bossReward = 80;
    public float dashSpeed = 3.5f;

    protected override void Update()
    {
        base.Update();

        if (target == null)
            return;

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target.position, dashSpeed * Time.deltaTime);
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
    }

    protected override void Defeat()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnBossDefeated();

        Destroy(gameObject);
    }
}
