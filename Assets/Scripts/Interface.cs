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
    
    float GetDamage();
    void ExecuteAttack();
    #region AdjustDamage API
    void AddDamagePercent(float multiplier);
    void RemoveDamagePercent(float multiplier);
    void ResetDamagePercent();
    void AddFlatDamage(float amount);
    void RemoveFlatDamage(float amount);
    void ResetFlatDamage();
    #endregion
}

public interface IMovement
{
    void SetMoveInput(Vector2 input);
    Vector2 GetMoveInput();

    Vector2 GetDirection();

    void EnableMovement();
    void DisableMovement();

    void ResetVelocity();
    void ApplyImpulse(Vector2 direction, float magnitude);
}

/// <summary>
/// ✓ Base interface for all interactable objects
/// </summary>
public interface IInteractable
{
    void Interact(GameObject player);
}

/// <summary>
/// ✓ For items that can be picked up (weapons, consumables, coins, etc.)
/// </summary>
public interface IPickable : IInteractable
{
    ItemData GetItemData();
}

/// <summary>
/// ✓ For objects that have interactive actions (doors, chests, NPCs, etc.)
/// </summary>
public interface IInteractive : IInteractable
{
    string GetInteractionName();
    bool CanInteract(GameObject player);
}

public interface ILevel
{
    void Execute();
}

public interface IImpulseMover
{
    void Play(Vector2 direction, float force, float duration);
    void StopCurrent();
}

public interface IProjectileBlocker{}
