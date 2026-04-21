using System;
using UnityEngine;

/// <summary>
/// ✓ Base class for interactive objects (doors, chests, NPCs, etc.)
/// Shows how to implement IInteractive interface
/// </summary>
public abstract class InteractiveObject : MonoBehaviour, IInteractive
{
    [SerializeField] protected string interactionName = "Interact";
    public Action OnInteraction;

    public virtual string GetInteractionName()
    {
        return interactionName;
    }

    public virtual bool CanInteract(GameObject player)
    {
        // Default implementation - can be overridden in derived classes
        return true;
    }

    public void Interact(GameObject player)
    {
        Debug.Log($"[InteractiveObject] {interactionName}: {gameObject.name}");
        OnInteraction?.Invoke();
        ExecuteInteraction();
    }

    /// <summary>
    /// ✓ Override this method in derived classes to implement specific interaction logic
    /// </summary>
    protected abstract void ExecuteInteraction();
}
