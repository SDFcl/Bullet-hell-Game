using UnityEngine;

public class RandomRadialFire : BasePatternFire
{
    [SerializeField] private int bulletCount = 15;

    [Header("Spread Setting")]
    [SerializeField] private float minSpawnOffset = 0.5f;
    [SerializeField] private float maxSpawnOffset = 1.5f;

    [SerializeField] private float minSpeedMultiplier = 0.15f;
    [SerializeField] private float maxSpeedMultiplier = 1f;

    public override void Execute(IProjectileWeapon weapon)
    {
        Vector3 center = weapon.GetOwner().transform.position;
        float baseSpeed = weapon.ProjectileSpeed;

        for (int i = 0; i < bulletCount; i++)
        {
            float randomAngle = Random.Range(0f, 360f);
            float randomOffset = Random.Range(minSpawnOffset, maxSpawnOffset);
            float randomSpeed = Random.Range(
                baseSpeed * minSpeedMultiplier,
                baseSpeed * maxSpeedMultiplier
            );

            Quaternion rotation = Quaternion.Euler(0f, 0f, randomAngle);

            Vector3 direction = rotation * Vector3.right;
            Vector3 spawnPosition = center + direction * randomOffset;

            SpawnProjectile(weapon, spawnPosition, rotation, randomSpeed);
        }
    }
}
