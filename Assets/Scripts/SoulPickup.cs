using UnityEngine;

public class SoulPickup : MonoBehaviour
{
    public int value = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.AddSoul(value);

            Destroy(gameObject);
        }
    }
}
