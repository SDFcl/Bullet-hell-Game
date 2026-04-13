using UnityEngine;
public class NormalProjectileHit : MonoBehaviour, IProjectileHitStrategy
{
    public void OnSpawn(ProjectileHit projectile)
    {
        
    }
    public void OnDespawn(ProjectileHit projectile)
    {
        projectile.gameObject.SetActive(false);
    }
}