using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagnetBehavior : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float triggerRadius = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, triggerRadius);

        Transform player = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                player = hit.transform;
                break;
            }
        }

        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, player.position);

            float speed = Mathf.Lerp(0, maxSpeed, 1 - distance / triggerRadius);

            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }

}
