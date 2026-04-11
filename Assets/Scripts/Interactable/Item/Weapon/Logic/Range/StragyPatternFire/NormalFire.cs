using UnityEngine;

public class NormalFire : BasePatternFire
{
    public override void Execute(ProjectileWeapon weapon)
    {
        Shoot(weapon, weapon.ShootPoint.position, weapon.ShootPoint.rotation);
    }
}
