using System;
using UnityEngine;
public interface IDamageable
{
    void TakeDamage(float damage);
}

public interface IHealable
{
    void Heal(float amount);
}

public interface IWeapon
{
    event Action OnAttack;

    void SetOwner(GameObject owner);
    void ExecuteAttack();
}

public interface IMovement
{
    void SetmoveInput(Vector2 input);
    Vector2 GetMoveInput();

    Vector2 GetDirection();

    void EnableMovement();
    void DisableMovement();
}
