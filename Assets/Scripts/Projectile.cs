using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 10;
    public Vector2 direction = Vector2.right;

    private void Update()
    {
        transform.position += (Vector3)(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
