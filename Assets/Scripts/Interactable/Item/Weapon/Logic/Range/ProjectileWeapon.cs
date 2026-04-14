using UnityEngine;

public class ProjectileWeapon : BaseWeapon, IProjectileWeapon
{
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private string projectilePoolTag = "BlueProjectile";

    [Header("Mana")]
    [SerializeField] private float manaCost = 20f;

    [Header("References")]
    [SerializeField] private Transform shootPoint;

    [Header("Animation Setting")]
    [SerializeField] private bool useAnimationEvent = false;

    private Mana mana;

    public float ProjectileSpeed => projectileSpeed;
    public string ProjectilePoolTag => projectilePoolTag;
    public Transform ShootPoint => shootPoint;

    // Stragy pattern for firing
    private IFireStrategy firePattern;

    protected override void Awake()
    {
        base.Awake();

        firePattern = GetComponent<IFireStrategy>();

        if(useSOData && weaponData != null)
        {
            projectilePoolTag = weaponData.rangedData.projectileTag;
            projectileSpeed = weaponData.rangedData.projectileSpeed;
            manaCost = weaponData.GetManaCostValue();
        }

        if (firePattern == null)
        {
            Debug.LogWarning($"No IFirePattern found on {gameObject.name}");
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
        if(!useAnimationEvent) return;
        Shoot();
    }

    //Animation Event
    public void Shoot()
    {
        firePattern?.Execute(this);
        if (mana != null)
            mana.ConsumeMana(manaCost);
    }
    public void SetMana(Mana mana)
    {
        this.mana = mana;
    }
}
