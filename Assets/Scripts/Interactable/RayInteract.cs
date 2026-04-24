using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RayInteract : MonoBehaviour
{
    [Header("Settings")]
    public float searchRadius = 5f;
    public LayerMask itemLayer;

    [Header("References")]
    [SerializeField] private CircleCollider2D CircleCollider2D;
    public GameObject target;

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

    public GameObject FindClosestItem()
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

        // ❗ เคสไม่เจออะไร
        if (closest == null)
        {
            if (target != null)
            {
                OnFogus oldFocus = target.GetComponent<OnFogus>();
                if (oldFocus != null)
                    oldFocus.SetFogus(false);

                target = null; // สำคัญมาก กันค้าง
            }

            return null;
        }

        // ❗ เคสเจอ แต่เป็นตัวใหม่
        if (target != closest.gameObject)
        {
            // ปิดตัวเก่า
            if (target != null)
            {
                OnFogus oldFocus = target.GetComponent<OnFogus>();
                if (oldFocus != null)
                    oldFocus.SetFogus(false);
            }

            // เปิดตัวใหม่
            OnFogus newFocus = closest.GetComponent<OnFogus>();
            if (newFocus != null)
                newFocus.SetFogus(true);

            target = closest.gameObject;
        }

        return closest.gameObject;
    }

    void Update()
    {
        target = FindClosestItem();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = SphereColor;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

        if (target != null)
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.yellow);
        }
    }
}
