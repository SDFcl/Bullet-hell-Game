using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileHit : Hitbox,IPooledObject
{
    [SerializeField] private float lifeTime = 0f;
    [SerializeField] private float bulletHealth = 1f;
    private float speed = 0f;
    
    private Rigidbody2D rb;
    private PureHealth health;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        health = new PureHealth(bulletHealth);
    }   

    public void OnObjectSpawn()
    {
        health.ResetHP();
        rb.linearVelocity = (Vector2)transform.right * speed;
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
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
                gameObject.SetActive(false);
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetProjectlieSpeed(float value) => speed = value;
}
