using System;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour,IWeapon
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float cooldown = 0.5f;


    private Hitbox hitbox;
    private float cooldownTimer;

    public event Action OnAttack;

    public void SetOwner(GameObject owner) => hitbox.SetOwner(owner);

    private void Awake()
    {
        hitbox = GetComponentInChildren<Hitbox>(true);
    }
    private void Start()
    {
        hitbox.EnableCol(false);
        hitbox.SetDamage(damage);
    } 
    private void Update()
    {
        if (cooldownTimer > 0)
        cooldownTimer -= Time.deltaTime;
    }
    public void ExecuteAttack()
    {
        if (cooldownTimer > 0) return;
        OnAttack?.Invoke();
        cooldownTimer = cooldown;
    }

    // Animation event
    public void EnableHitbox() => hitbox.EnableCol(true);
    public void DisableHitbox() => hitbox.EnableCol(false);
}
