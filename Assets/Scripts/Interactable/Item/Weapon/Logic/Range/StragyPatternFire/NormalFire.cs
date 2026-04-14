using UnityEngine;

public class NormalFire : BasePatternFire
{
    public override void Execute(IProjectileWeapon weapon)
    {
        Shoot(weapon, weapon.ShootPoint.position, weapon.ShootPoint.rotation);
    }
}
