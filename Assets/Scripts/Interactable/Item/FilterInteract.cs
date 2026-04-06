using UnityEngine;

/// <summary>
/// ✓ Interaction Filter System
/// - Detects interaction type automatically
/// - Easy to use: just call FilterAndExecute(target)
/// - Supports Pickable items and Interactive objects
/// </summary>
public class FilterInteract
{
    private GameObject player;

    public FilterInteract(GameObject playerGameObject)
    {
        player = playerGameObject;
    }

    /// <summary>
    /// ✓ Main method - Filters and executes interaction automatically
    /// Returns: true if interaction succeeded, false if not
    /// </summary>
    public bool FilterAndExecute(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("[FilterInteract] Target is null!");
            return false;
        }

        // Try Pickable first (weapons, consumables, items)
        IPickable pickable = target.GetComponent<IPickable>();
        if (pickable != null)
        {
            ExecutePickup(pickable);
            return true;
        }

        // Try Interactive (doors, chests, NPCs, etc.)
        IInteractive interactive = target.GetComponent<IInteractive>();
        if (interactive != null)
        {
            if (interactive.CanInteract(player))
            {
                ExecuteInteractive(interactive);
                return true;
            }
            else
            {
                Debug.Log($"[FilterInteract] Cannot interact with {target.name}");
                return false;
            }
        }

        // No valid interaction found
        Debug.LogWarning($"[FilterInteract] {target.name} has no IPickable or IInteractive interface!");
        return false;
    }

    /// <summary>
    /// ✓ Handle pickable items
    /// </summary>
    private void ExecutePickup(IPickable pickable)
    {
        pickable.Interact(player);
        Debug.Log($"[FilterInteract] Picked up item");
    }

    /// <summary>
    /// ✓ Handle interactive objects
    /// </summary>
    private void ExecuteInteractive(IInteractive interactive)
    {
        interactive.Interact(player);
        Debug.Log($"[FilterInteract] Interacted with: {interactive.GetInteractionName()}");
    }

    /// <summary>
    /// ✓ Check interaction type without executing
    /// Returns: "Pickable", "Interactive", or "None"
    /// </summary>
    public string GetInteractionType(GameObject target)
    {
        if (target == null) return "None";

        if (target.GetComponent<IPickable>() != null)
            return "Pickable";

        if (target.GetComponent<IInteractive>() != null)
            return "Interactive";

        return "None";
    }
}

