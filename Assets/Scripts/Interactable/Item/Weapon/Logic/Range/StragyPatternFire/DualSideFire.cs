using UnityEngine;
public class DaulSideFire : BasePatternFire
{
    [SerializeField] private float sideOffset = 0.3f;

    public override void Execute(IProjectileWeapon weapon)
    {
        Quaternion rotation = weapon.ShootPoint.rotation;
        Vector3 origin = weapon.ShootPoint.position;
        Vector3 sideDirection = rotation * Vector3.up;

        SpawnProjectile(weapon, origin - sideDirection * sideOffset, rotation);
        SpawnProjectile(weapon, origin + sideDirection * sideOffset, rotation);
    }
}
