using UnityEngine;

public abstract class BasePatternFire : MonoBehaviour, IFireStrategy
{
    public abstract void Execute(IProjectileWeapon weapon);

    protected void SpawnProjectile(IProjectileWeapon weapon, Vector3 position, Quaternion rotation)
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool(
            weapon.ProjectilePoolTag,
            position,
            rotation,
            obj =>
            {
                var projHit = obj.GetComponent<ProjectileHit>();
                if (projHit != null)
                {
                    projHit.SetDamage(weapon.GetDamage());
                    projHit.SetProjectlieSpeed(weapon.ProjectileSpeed);
                    projHit.SetOwner(weapon.GetOwner());
                    projHit.SetUseSOData(weapon.UseSOData);
                    projHit.SetWeaponData(weapon.WeaponDataSO);
                }
            });

        if (projectile == null)
        {
            Debug.LogWarning("Failed to spawn projectile from pool");
            return;
        }
    }

    protected void SpawnProjectile(IProjectileWeapon weapon, Vector3 position, Quaternion rotation, float speed)
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool(
            weapon.ProjectilePoolTag,
            position,
            rotation,
            obj =>
            {
                var projHit = obj.GetComponent<ProjectileHit>();
                if (projHit != null)
                {
                    projHit.SetDamage(weapon.GetDamage());
                    projHit.SetProjectlieSpeed(speed);
                    projHit.SetOwner(weapon.GetOwner());
                    projHit.SetUseSOData(weapon.UseSOData);
                    projHit.SetWeaponData(weapon.WeaponDataSO);
                }
            });

        if (projectile == null)
        {
            Debug.LogWarning("Failed to spawn projectile from pool");
            return;
        }
    }
}
