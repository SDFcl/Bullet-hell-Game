using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    private float damage;
    private Collider2D hitCollider;
    [SerializeField] private bool isTrigger;

    private GameObject owner;
    public Action OnHit;

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
        if(CheckOwner(col)) return;

        IDamageable damageable = col.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log($"Hit {col.name} with damage {damage}");
            
            damageable.TakeDamage(damage);
            if(InvokeOnDealDamage())
                OnHit?.Invoke();
        }
    }

    protected virtual bool CheckOwner(Collider2D col)
    {
        if (owner == null) return false; 
        if (col.gameObject == owner|| col.gameObject.CompareTag(owner.tag)) return true;

        GameObject hitRoot = col.transform.root.gameObject;

        if (hitRoot == owner) return true;
        if (hitRoot.CompareTag(owner.tag)) return true;

        return false;
    }

    protected virtual bool InvokeOnDealDamage() => true;

    //API
    public float GetDamage() => damage;
    public GameObject GetOwner() => owner;


    // ScriptableObject API
    public bool UseSOData {get; private set;}
    public void SetUseSOData(bool useSOData)
    {
        UseSOData = useSOData;
    }
    
    public WeaponDataSO WeaponDataSO { get; private set; }

    public void SetWeaponData(WeaponDataSO weaponData)
    {
        WeaponDataSO = weaponData;
    }
}