using UnityEngine;
using System;

public class Attack : MonoBehaviour
{
    [SerializeField] private float baseDamage = 0f;
    [SerializeField] private Transform holdingItem;

    private HoldingItemWatcher holdingItemWatcher;
    private IWeapon currentWeapon;
    public IWeapon CurrentWeapon => currentWeapon;

    public Action OnAttack;

    private void Awake()
    {
        if (holdingItem == null)
        {
            holdingItem = transform.Find("HoldingItem");

            if (holdingItem == null)
            {
                Debug.LogError("HoldingItem child not found. Please assign it in the inspector or create a child named 'HoldingItem'.");
                return;
            }
        }

        holdingItemWatcher = holdingItem.GetComponent<HoldingItemWatcher>();

        if (holdingItemWatcher == null)
        {
            Debug.LogWarning("HoldingItemWatcher component not found on HoldingItem. Adding one.");
            holdingItemWatcher = holdingItem.gameObject.AddComponent<HoldingItemWatcher>();
        }

        RefreshWeapon();
    }

    private void OnEnable()
    {
        holdingItemWatcher.OnHoldingItemChanged += RefreshWeapon;
    }

    private void OnDisable()
    {
        holdingItemWatcher.OnHoldingItemChanged -= RefreshWeapon;
        UnsubscribeCurrentWeapon();
    }

    public void TryAttack()
    {
        if (currentWeapon == null) return;
        currentWeapon.ExecuteAttack();
    }

    public void RefreshWeapon()
    {
        UnsubscribeCurrentWeapon();

        if (holdingItem == null)
        {
            currentWeapon = null;
            return;
        }

        currentWeapon = holdingItem.GetComponentInChildren<IWeapon>();

        if (currentWeapon != null)
        {
            currentWeapon.SetOwner(gameObject);
            currentWeapon.AddFlatDamage(baseDamage);
            currentWeapon.OnAttack += HandleWeaponAttack;
        }
    }

    private void HandleWeaponAttack()
    {
        OnAttack?.Invoke();
    }

    private void UnsubscribeCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnAttack -= HandleWeaponAttack;
        }
    }

    #region AdjustDamage API
    public void AddDamagePercent(float percent)
    {
        currentWeapon?.AddDamagePercent(percent);
        Debug.Log($"Added {percent * 100}% damage. Current total damage: {currentWeapon?.GetDamage()}");
    }
    public void RemoveDamagePercent(float percent) => currentWeapon?.RemoveDamagePercent(percent);
    public void ResetDamagePercent() => currentWeapon?.ResetDamagePercent();

    public void AddFlatDamage(float amount) => currentWeapon?.AddFlatDamage(amount);
    public void RemoveFlatDamage(float amount) => currentWeapon?.RemoveFlatDamage(amount);
    public void ResetFlatDamage() => currentWeapon?.ResetFlatDamage();
    #endregion
}
