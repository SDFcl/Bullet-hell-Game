using UnityEngine;

public class TripleFire : BasePatternFire
{
    [SerializeField] private float spreadAngle = 15f;
    public override void Execute(IProjectileWeapon weapon)
    {
        Shoot(weapon, weapon.ShootPoint.position, 
        weapon.ShootPoint.rotation);

        Shoot(weapon, weapon.ShootPoint.position, 
        weapon.ShootPoint.rotation * Quaternion.Euler(0, 0, spreadAngle));

        Shoot(weapon, weapon.ShootPoint.position, 
        weapon.ShootPoint.rotation * Quaternion.Euler(0, 0, -spreadAngle));
    }
}
