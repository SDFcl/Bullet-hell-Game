using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRegenRate = 1f;

    [field: SerializeField]
    public float CurrentMana { get; private set; }

    private void Start()
    {
        CurrentMana = maxMana;
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
    public void BoostMaxMana(float value)
    {
        maxMana += value;
    }
    public void BoostCurrentMana(float value)
    {
        CurrentMana += value;
        CurrentMana = Mathf.Min(CurrentMana, maxMana);
    }
    public void BoostRegenRate(float value)
    {
        manaRegenRate += value;
    }
    public void ConsumeMana(float amount)
    {
        if (CurrentMana >= amount)
        {
            CurrentMana -= amount;
        }
        else
        {
            Debug.LogWarning("Not enough mana!");
        }
    }
}
