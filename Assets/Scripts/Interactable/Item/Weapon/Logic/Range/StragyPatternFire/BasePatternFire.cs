using UnityEngine;

public abstract class BasePatternFire : MonoBehaviour, IFirePattern
{
    public abstract void Execute(ProjectileWeapon weapon);

    protected void Shoot(ProjectileWeapon weapon, Vector3 position, Quaternion rotation)
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
                }
            });

        if (projectile == null)
        {
            Debug.LogWarning("Failed to spawn projectile from pool");
            return;
        }
    }
}