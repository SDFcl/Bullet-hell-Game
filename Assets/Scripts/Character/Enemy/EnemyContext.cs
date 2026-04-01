using UnityEngine;

public class EnemyContext
{
    public Transform Self { get; }
    public Transform Target { get; }

    public Health Health { get; }
    public Mana Mana { get; }
    public Movement Movement { get; }
    public AimPivot2D AimPivot { get; }
    public Facing2D Facing { get; }

    public EnemyContext(Transform self, Transform target, Health health, Mana mana, Movement movement, AimPivot2D aimPivot, Facing2D facing)
    {
        Self = self;
        Target = target;
        Health = health;
        Mana = mana;
        Movement = movement;
        AimPivot = aimPivot;
        Facing = facing;
    }
    public T Get<T>() where T : Component
    {
        return Self.GetComponent<T>();
    }
}
