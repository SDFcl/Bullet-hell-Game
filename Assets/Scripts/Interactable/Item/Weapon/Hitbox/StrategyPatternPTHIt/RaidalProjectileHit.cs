using UnityEngine;

public class RadialProjectileHit : MonoBehaviour, IProjectileHitStrategy
{
    [Header("Child Projectile Spawn")]
    [SerializeField] private int projectileCount = 8;
    [SerializeField] private float angleOffset = 45f;
    [SerializeField] private string childProjectileTag = "BlueProjectile";

    [Header("Child Projectile Ability")]
    [SerializeField] private float childProjectileSpeed = 6f;
    [SerializeField] private float childProjectileDamage = 1;

    public void OnSpawn(ProjectileHit projectile)
    {
    }

    public void OnDespawn(ProjectileHit projectile)
    {
        if (projectile == null || ObjectPooler.Instance == null)
        {
            return;
        }

        float angleStep = 360f / projectileCount;
        Vector3 center = projectile.transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = angleStep * i + angleOffset;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector3 direction = rotation * Vector3.right;
            Vector3 spawnPosition = center + direction;

            ObjectPooler.Instance.SpawnFromPool(
                childProjectileTag,
                spawnPosition,
                rotation,
                obj =>
                {
                    ProjectileHit childProjectile = obj.GetComponent<ProjectileHit>();
                    if (childProjectile != null)
                    {
                        childProjectile.SetProjectlieSpeed(childProjectileSpeed);
                        childProjectile.SetDamage(childProjectileDamage);
                        childProjectile.SetOwner(projectile.GetOwner());
                    }
                });
        }

        projectile.gameObject.SetActive(false);
    }
}
