using UnityEngine;
using System;

public class Heart : MonoBehaviour,ICollectEvent
{
    public event Action<GameObject> OnCollected;
    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                OnCollected?.Invoke(gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
