using UnityEngine;

/// <summary>
/// ✓ Base class for interactive objects (doors, chests, NPCs, etc.)
/// Shows how to implement IInteractive interface
/// </summary>
public abstract class InteractiveObject : MonoBehaviour, IInteractive
{
    [SerializeField] protected string interactionName = "Interact";
    [SerializeField] protected float interactionDistance = 2f;

    public virtual string GetInteractionName()
    {
        return interactionName;
    }

    public virtual bool CanInteract(GameObject player)
    {
        // Check distance
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > interactionDistance)
        {
            Debug.Log($"Too far away! Distance: {distance}, Max: {interactionDistance}");
            return false;
        }

        return true;
    }

    public void Interact(GameObject player)
    {
        Debug.Log($"[InteractiveObject] {interactionName}: {gameObject.name}");
        ExecuteInteraction();
    }

    /// <summary>
    /// ✓ Override this method in derived classes to implement specific interaction logic
    /// </summary>
    protected abstract void ExecuteInteraction();
}
