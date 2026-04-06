using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    private float damage;
    private Collider2D hitCollider;
    [SerializeField] private bool isTrigger;

    private GameObject owner;

    public void SetDamage(float damage) => this.damage = damage;
    public void SetOwner(GameObject owner) => this.owner = owner;
    public void EnableCol(bool enable) => hitCollider.enabled = enable;


    protected virtual void Awake()
    {
        hitCollider = GetComponent<Collider2D>();
        hitCollider.isTrigger = isTrigger;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTrigger) return;
        ProcessHit(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTrigger) return;
        ProcessHit(collision.collider);
    }

    protected virtual void ProcessHit(Collider2D col)
    {
        if (col.gameObject == owner|| col.gameObject.CompareTag(owner.tag)) return;

        IDamageable damageable = col.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log($"Hit {col.name} with damage {damage}");
            damageable.TakeDamage(damage);
        }
    }
}