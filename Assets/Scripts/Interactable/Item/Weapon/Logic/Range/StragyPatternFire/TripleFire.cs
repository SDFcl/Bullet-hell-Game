using UnityEngine;

public class MultiSideFire : BasePatternFire
{
    [SerializeField] private int sideBulletCount = 1;
    [SerializeField] private float spreadAngle = 15f;

    public override void Execute(IProjectileWeapon weapon)
    {
        Transform shootPoint = weapon.ShootPoint;

        // กระสุนตรงกลาง
        SpawnProjectile(
            weapon,
            shootPoint.position,
            shootPoint.rotation
        );

        // กระสุนด้านข้าง ซ้าย/ขวา เพิ่มตามจำนวน
        for (int i = 1; i <= sideBulletCount; i++)
        {
            float angle = spreadAngle * i;

            SpawnProjectile(
                weapon,
                shootPoint.position,
                shootPoint.rotation * Quaternion.Euler(0, 0, angle)
            );

            SpawnProjectile(
                weapon,
                shootPoint.position,
                shootPoint.rotation * Quaternion.Euler(0, 0, -angle)
            );
        }
    }
}
