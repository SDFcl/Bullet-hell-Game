using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileHit : Hitbox, IPooledObject
{
    [SerializeField] private float lifeTime = 0f;
    [SerializeField] private float bulletHealth = 1f;
    private float speed = 0f;
    
    private Rigidbody2D rb;
    private PureHealth health;
    private IProjectileHitStrategy lifetimeStrategy;
    public Action OnSpawn;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        health = new PureHealth(bulletHealth);
        lifetimeStrategy = GetComponent<IProjectileHitStrategy>();
    }   

    public void OnObjectSpawn()
    {
        health.ResetHP();
        rb.linearVelocity = (Vector2)transform.right * speed;
        StartCoroutine(Disable());
        lifetimeStrategy?.OnSpawn(this);
        OnSpawn?.Invoke();
    }

    private IEnumerator Disable()
	{
		yield return new WaitForSeconds(lifeTime);
		lifetimeStrategy?.OnDespawn(this);
	}

    protected override void ProcessHit(Collider2D col)
    {
        base.ProcessHit(col);
        if(base.CheckOwner(col)) return;

        /*
        if (col.GetComponent<IProjectileBlocker>() != null)
        {
            Debug.Log($"{col.gameObject.name} blocked the projectile!");
        }
        */

        if (col.GetComponent<IDamageable>() != null||
        col.GetComponent<IProjectileBlocker>() != null)
        {
            health.TakeDamage(1);
            if (health.IsDead)
            {
                OnHit?.Invoke();
                lifetimeStrategy?.OnDespawn(this);
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            OnHit?.Invoke();
            lifetimeStrategy?.OnDespawn(this);
        }
    }

    protected override bool InvokeOnDealDamage() => false;

    public void SetProjectlieSpeed(float value) => speed = value;
}
