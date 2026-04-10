using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage = 10f;
    private ProjectlieHit projectileHit;
    private void Awake()
    {
        projectileHit = GetComponent<ProjectlieHit>();
        if (projectileHit == null)
        {
            Debug.LogError("ProjectileHit component not found on " + gameObject.name);
        }

        projectileHit.SetDamage(damage);
        projectileHit.SetOwner(gameObject);
    }

}
