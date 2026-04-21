using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
    private Hitbox hitbox;

    protected override void Awake()
    {
        base.Awake();
        hitbox = GetComponentInChildren<Hitbox>(true);
        if (hitbox != null)
        {
            hitbox.SetDamage(GetDamage());
            hitbox.SetWeaponData(weaponData);
            hitbox.SetUseSOData(useSOData);
        }
    }

    private void Start()
    {
        if (hitbox != null)
        {
            hitbox.EnableCol(false);
        }
    }

    public override void SetOwner(GameObject owner)
    {
        base.SetOwner(owner);

        if (hitbox == null)
        {
            hitbox = GetComponentInChildren<Hitbox>(true);
        }

        if (hitbox != null)
            hitbox.SetOwner(owner);
    }

    protected override void OnDamageChanged()
    {
        if (hitbox != null)
            hitbox.SetDamage(GetDamage());
    }

    protected override void ExecuteWeaponAction() { }

    // Animation event
    public void EnableHitbox() => hitbox.EnableCol(true);
    public void DisableHitbox() => hitbox.EnableCol(false);
}
