using System;
using UnityEngine;

public abstract class InteractiveSlotSellItem : MonoBehaviour, IInteractive
{
    [SerializeField] protected string interactionName = "Interact";

    public event Action OnInteract;

    public virtual string GetInteractionName()
    {
        return interactionName;
    }

    public virtual bool CanInteract(GameObject player)
    {
        // ✓ Default implementation: always interactable
        // Override this in derived classes for specific conditions (e.g., distance, player state, etc.)
        return true;
    }

    public void Interact(GameObject player)
    {
        Debug.Log($"[InteractiveObject] {interactionName}: {gameObject.name}");
        OnInteract?.Invoke();
        ExecuteInteraction(player);
    }

    /// <summary>
    /// ✓ Override this method in derived classes to implement specific interaction logic
    /// </summary>
    protected abstract void ExecuteInteraction(GameObject player);
}
