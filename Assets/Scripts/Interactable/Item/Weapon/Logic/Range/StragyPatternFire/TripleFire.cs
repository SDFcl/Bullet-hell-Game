using UnityEngine;

public class TripleFire : BasePatternFire
{
    [SerializeField] private float spreadAngle = 15f;
    public override void Execute(IProjectileWeapon weapon)
    {
        SpawnProjectile(weapon, weapon.ShootPoint.position, 
        weapon.ShootPoint.rotation);

        SpawnProjectile(weapon, weapon.ShootPoint.position, 
        weapon.ShootPoint.rotation * Quaternion.Euler(0, 0, spreadAngle));

        SpawnProjectile(weapon, weapon.ShootPoint.position, 
        weapon.ShootPoint.rotation * Quaternion.Euler(0, 0, -spreadAngle));
    }
}
