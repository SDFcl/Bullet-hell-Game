using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRegenRate = 1f;
    private bool ignoreManaCost = false;

    [field: SerializeField]
    public float CurrentMana { get; private set; }
    public float MaxMana => maxMana;

    private void Awake()
    {
        
    }

    private void Start()
    {
        CurrentMana = maxMana;
        IPlayerStats stats = FindObjectOfType<PlayerUpgradeManager>()?.GetFinalStats();
        if (stats != null)
        {
            maxMana += stats.MaxMana;
            CurrentMana = maxMana;
            //Debug.Log($"Mana: Max mana updated to {maxMana} based on player stats (BonusMana: {stats.MaxMana})");
        }
    }
    private void Update()
    {
        RegenMana();
    }
    private void RegenMana()
    {
        if(CurrentMana < maxMana)
        {
            CurrentMana += manaRegenRate * Time.deltaTime;
            CurrentMana = Mathf.Min(CurrentMana, maxMana);
        }
    }
    public void ConsumeMana(float amount)
    {
        if (ignoreManaCost) return;
        if (CurrentMana >= amount)
        {
            CurrentMana -= amount;
        }
        else
        {
            Debug.LogWarning("Not enough mana!");
        }
    }
    #region Mana Adjustment API
    public void EnableIgnoreManaCost(bool enable)
    {
        CurrentMana = maxMana;
        ignoreManaCost = enable;
    }
    #endregion
}
