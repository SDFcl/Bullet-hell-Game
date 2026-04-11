public class PureHealth
{
    public float MaxHP { get; }
    public float CurrentHP { get; private set; }
    public bool IsDead => CurrentHP <= 0;

    public PureHealth(float maxHP)
    {
        MaxHP = maxHP;
        CurrentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
        }
    }
    public void ResetHP()
    {
        CurrentHP = MaxHP;
    }
}