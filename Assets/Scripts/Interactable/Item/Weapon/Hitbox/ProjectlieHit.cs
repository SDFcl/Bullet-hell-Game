using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectlieHit : Hitbox,IPooledObject
{
    [SerializeField] private float lifeTime = 0f;
    private float speed = 0f;
    
    private Rigidbody2D rb;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }   

    public void OnObjectSpawn()
    {
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

        /*
        if (col.GetComponent<IProjectileBlocker>() != null)
        {
            Debug.Log($"{col.gameObject.name} blocked the projectile!");
        }
        */
        
        if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle") ||
         col.GetComponent<IDamageable>() != null||
         col.GetComponent<IProjectileBlocker>() != null)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetProjectlieSpeed(float value) => speed = value;
}
