using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float baseDamage = 0f;
    [SerializeField] private Transform holdingItem;
    private HoldingItemWatcher holdingItemWatcher;
    private IWeapon currentWeapon;

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
    void OnEnable()
    {
        holdingItemWatcher.OnHoldingItemChanged += RefreshWeapon;
    }

    void OnDisable()
    {
        holdingItemWatcher.OnHoldingItemChanged -= RefreshWeapon;
    }

    public void TryAttack()
    {
        if (currentWeapon == null) return;
        currentWeapon.ExecuteAttack();
    }

    public void RefreshWeapon()
    {
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
        }
            
    }

    #region AdjustDamage API
    public void AddDamagePercent(float percent) => currentWeapon?.AddDamagePercent(percent);
    public void RemoveDamagePercent(float percent) => currentWeapon?.RemoveDamagePercent(percent);
    public void ResetDamagePercent() => currentWeapon?.ResetDamagePercent();

    public void AddFlatDamage(float amount) => currentWeapon?.AddFlatDamage(amount);
    public void RemoveFlatDamage(float amount) => currentWeapon?.RemoveFlatDamage(amount);
    public void ResetFlatDamage() => currentWeapon?.ResetFlatDamage();
    #endregion
}
