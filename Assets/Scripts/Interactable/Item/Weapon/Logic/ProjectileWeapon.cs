using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private string projectilePoolTag = "BlueProjectile";

    [Header("Mana")]
    [SerializeField] private float manaCost = 20f;

    [Header("References")]
    [SerializeField] private Transform shootPoint;

    private Mana mana;

    protected override void Awake()
    {
        base.Awake();
        if(useSOData && weaponData != null)
        {
            projectileSpeed = weaponData.rangedData.projectileSpeed;
            manaCost = weaponData.GetManaCostValue();
        }
        if (shootPoint == null)
        {
            Debug.LogWarning("Shooting point not found!");
        }
    }

    public void SetProjectileTag(string tag)
    {
        projectilePoolTag = tag;
    }

    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);

        if (owner != null)
            mana = owner.GetComponent<Mana>();
        else
            mana = null;
    }

    protected override bool CanAttack()
    {
        if (mana != null && mana.CurrentMana < manaCost)
        {
            Debug.LogWarning("Not enough mana to shoot!");
            return false;
        }

        if (shootPoint == null)
        {
            Debug.LogWarning("shootPoint is null");
            return false;
        }

        if (ObjectPooler.Instance == null)
        {
            Debug.LogError("ObjectPooler.Instance is null");
            return false;
        }

        return true;
    }

    protected override void PerformAttack()
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool(
            projectilePoolTag,
            shootPoint.position,
            shootPoint.rotation,
            obj =>
            {
                var projHit = obj.GetComponent<ProjectlieHit>();
                if (projHit != null)
                {
                    projHit.SetDamage(GetDamage());
                    projHit.SetProjectlieSpeed(projectileSpeed);
                    projHit.SetOwner(owner);
                }
            });

        if (projectile == null)
        {
            Debug.LogWarning("Failed to spawn projectile from pool");
            return;
        }

        if (mana != null)
            mana.ConsumeMana(manaCost);
    }

    public void SetMana(Mana mana)
    {
        this.mana = mana;
    }
}
