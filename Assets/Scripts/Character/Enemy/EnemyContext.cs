using Pathfinding;
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
    public Seeker Seeker { get; }
    public PathToDir PathToDir { get; }
    public Timer Timer { get; } = new Timer();
    public LineOfSight2D LineOfSight { get; }
    public Attack Attack { get; }
    public EnemyContext(Transform self, Transform target,
     Health health, Mana mana, Movement movement, 
     AimPivot2D aimPivot, Facing2D facing, PathToDir pathToDir, LineOfSight2D lineOfSight, Attack attack)
    {
        Self = self;
        Target = target;
        Health = health;
        Mana = mana;
        Movement = movement;
        AimPivot = aimPivot;
        Facing = facing;
        PathToDir = pathToDir;
        LineOfSight = lineOfSight;
        Attack = attack;

    }
    public T Get<T>() where T : Component
    {
        return Self.GetComponent<T>();
    }

    public void MoveTo(Vector2 destination)
    {
        Facing?.SetDirection(destination.x);
        AimPivot?.SetDirection(destination);
        Movement.SetMoveInput(destination);
    }
}
