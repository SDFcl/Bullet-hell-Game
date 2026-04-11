using UnityEngine;
public class MeleeProjectileWeapon : MeleeWeapon
{
    [Header("Projectile")]
    [SerializeField] private float projectileDamage = 5f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private string projectilePoolTag = "BlueProjectile";

    [Header("Mana")]
    [SerializeField] private float manaCost = 20f;

    [Header("References")]
    [SerializeField] private Transform shootPoint;

    private Mana mana;

    public float ProjectileSpeed => projectileSpeed;
    public string ProjectilePoolTag => projectilePoolTag;
    public Transform ShootPoint => shootPoint;


    protected override void Awake()
    {
        base.Awake();

        if(useSOData && weaponData != null)
        {
            projectilePoolTag = weaponData.rangedData.projectileTag;
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
        mana = owner != null ? owner.GetComponent<Mana>() : null;
    }

    protected override bool CanAttack()
    {
        if (mana != null && mana.CurrentMana < manaCost)
        {
            Debug.Log("Not enough mana to shoot!");
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
            ProjectilePoolTag,
            shootPoint.position,
            shootPoint.rotation,
            obj =>
            {
                var projHit = obj.GetComponent<ProjectileHit>();
                if (projHit != null)
                {
                    projHit.SetDamage(projectileDamage);
                    projHit.SetProjectlieSpeed(projectileSpeed);
                    projHit.SetOwner(GetOwner());
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
}