using UnityEngine;
public class RadialFire : BasePatternFire
{
    [SerializeField] private int numberOfProjectiles = 8;
    [SerializeField] private float spawnOffset = 1;


    public override void Execute(ProjectileWeapon weapon)
    {
        float angleStep = 360f / numberOfProjectiles;
        Vector3 center = weapon.GetOwner().transform.position;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            Vector3 direction = rotation * Vector3.right;
            Vector3 spawnPosition = center + direction * spawnOffset;

            Shoot(weapon, spawnPosition, rotation);
        }
    }

}