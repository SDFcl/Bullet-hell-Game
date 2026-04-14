using UnityEngine;

public class NormalFire : BasePatternFire
{
    public override void Execute(IProjectileWeapon weapon)
    {
        SpawnProjectile(weapon, weapon.ShootPoint.position, weapon.ShootPoint.rotation);
    }
}
