using System;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour, IWeapon
{
    [Header("Combat")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float cooldown = 0.5f;

    [Header("Projectile")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private string projectilePoolTag = "BlueProjectile";

    [Header("Mana")]
    [SerializeField] private float manaCost = 20f;

    [Header("References")]
    [SerializeField] private Transform shootPoint;

    public event Action OnAttack;

    private float cooldownTimer;
    private GameObject owner;
    private Mana mana;

    private void Awake()
    {
        if (shootPoint == null)
        {
            Debug.LogWarning("Shooting point not found!");
        }
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void ExecuteAttack()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (cooldownTimer > 0f) return;

        if (mana != null && mana.CurrentMana < manaCost)
        {
            Debug.LogWarning("Not enough mana to shoot!");
            return;
        }

        if (shootPoint == null)
        {
            Debug.LogWarning("shootPoint is null");
            return;
        }

        if (ObjectPooler.Instance == null)
        {
            Debug.LogError("ObjectPooler.Instance is null");
            return;
        }

        GameObject projectile = ObjectPooler.Instance.SpawnFromPool(
            projectilePoolTag,
            shootPoint.position,
            shootPoint.rotation,
            obj =>
            {
                var projHit = obj.GetComponent<ProjectlieHit>();
                if (projHit != null)
                {
                    projHit.SetDamage(damage);
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
        
        cooldownTimer = cooldown;
        OnAttack?.Invoke();
    }

    public void SetProjectileTag(string tag)
    {
        projectilePoolTag = tag;
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;

        if (owner != null)
            mana = owner.GetComponent<Mana>();
    }

    public void SetMana(Mana mana)
    {
        this.mana = mana;
    }
}