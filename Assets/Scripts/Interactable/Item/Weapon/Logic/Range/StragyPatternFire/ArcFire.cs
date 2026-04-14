using UnityEngine;

public class ArcFire : BasePatternFire
{
    [SerializeField] private int bulletCount = 7;
    [SerializeField] private float totalArcAngle = 90f;
    [SerializeField] private float spawnOffset = 0.5f;

    public override void Execute(IProjectileWeapon weapon)
    {
        if (bulletCount <= 0) return;

        float baseAngle = weapon.ShootPoint.eulerAngles.z;

        if (bulletCount == 1)
        {
            Quaternion singleRotation = Quaternion.Euler(0f, 0f, baseAngle);
            Vector3 singleDirection = singleRotation * Vector3.right;
            Vector3 singleSpawnPos = weapon.ShootPoint.position + singleDirection * spawnOffset;

            SpawnProjectile(weapon, singleSpawnPos, singleRotation);
            return;
        }

        float startAngle = baseAngle - totalArcAngle * 0.5f;
        float angleStep = totalArcAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            Vector3 direction = rotation * Vector3.right;
            Vector3 spawnPosition = weapon.ShootPoint.position + direction * spawnOffset;

            SpawnProjectile(weapon, spawnPosition, rotation);
        }
    }
}
