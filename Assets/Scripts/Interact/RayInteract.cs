using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RayInteract : MonoBehaviour
{
    [Header("Settings")]
    public float searchRadius = 5f;
    public LayerMask itemLayer;

    [Header("References")]
    [SerializeField] private CircleCollider2D CircleCollider2D;
    Transform target;

    [Header("Draw Gizmos")]
    public Color SphereColor = Color.red;

    private void Awake()
    {
        if (CircleCollider2D == null)
        {
            CircleCollider2D = GetComponent<CircleCollider2D>();
        }
        
        if (CircleCollider2D != null)
        {
            CircleCollider2D.radius = searchRadius;
        }
    }

    public Transform FindClosestItem()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, itemLayer);

        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D col in hits)
        {
            float distance = Vector2.Distance(transform.position, col.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = col.transform;
            }
        }

        return closest;
    }

    void Update()
    {
        target = FindClosestItem();

        if (target != null)
        {
            Debug.DrawLine(transform.position, target.position, Color.yellow);
        }
    }

    public void PickUp()
    {
        ItemPickup itemPickup = target.GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.Pickup(GetComponent<Inventory>());
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = SphereColor;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
