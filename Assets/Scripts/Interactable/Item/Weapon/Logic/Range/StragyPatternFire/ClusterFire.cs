using UnityEngine;

public class ClusterSpreadFire : BasePatternFire
{
    [Header("Scatter Bullets")]
    [SerializeField] private string scatterProjectileTag = "BulletA";
    [SerializeField] private int scatterCount = 6;
    [SerializeField] private float spreadAngle = 15f;

    [SerializeField] private float minSpawnOffset = 0.2f;
    [SerializeField] private float maxSpawnOffset = 1f;

    [SerializeField] private float minSpeedMultiplier = 0.8f;
    [SerializeField] private float maxSpeedMultiplier = 1.4f;

    public override void Execute(ProjectileWeapon weapon)
    {
        Vector3 center = weapon.ShootPoint.position;
        Quaternion forwardRotation = weapon.ShootPoint.rotation;
        float baseAngle = forwardRotation.eulerAngles.z;
        float baseSpeed = weapon.ProjectileSpeed;

        // ลูกกลาง
        SpawnProjectile(
            weapon.ProjectilePoolTag,
            weapon,
            center,
            forwardRotation,
            weapon.ProjectileSpeed
        );

        // ลูกย่อย
        for (int i = 0; i < scatterCount; i++)
        {
            float angleOffset = Random.Range(-spreadAngle * 0.5f, spreadAngle * 0.5f);
            float finalAngle = baseAngle + angleOffset;

            Quaternion rotation = Quaternion.Euler(0f, 0f, finalAngle);

            float randomOffset = Random.Range(minSpawnOffset, maxSpawnOffset);
            Vector3 direction = rotation * Vector3.right;
            Vector3 spawnPosition = center + direction * randomOffset;

            float randomSpeed = Random.Range(
                baseSpeed * minSpeedMultiplier,
                baseSpeed * maxSpeedMultiplier
            );

            SpawnProjectile(
                scatterProjectileTag,
                weapon,
                spawnPosition,
                rotation,
                randomSpeed
            );
        }
    }

    private void SpawnProjectile(
        string projectileTag,
        ProjectileWeapon weapon,
        Vector3 position,
        Quaternion rotation,
        float speed)
    {
        ObjectPooler.Instance.SpawnFromPool(
            projectileTag,
            position,
            rotation,
            obj =>
            {
                if (obj.TryGetComponent<ProjectileHit>(out var projectile))
                {
                    projectile.SetDamage(weapon.GetDamage());
                    projectile.SetProjectlieSpeed(speed);
                    projectile.SetOwner(weapon.GetOwner());
                }
            });
    }
}
