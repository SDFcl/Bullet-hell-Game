using UnityEngine;

public class InteractiveFountain : MonoBehaviour , IInteractive
{
    public int HealAmount = 2;

    public string GetInteractionName()
    {
        return "Drink";
    }

    public bool CanInteract(GameObject player)
    {
        // ✓ Example condition: player must be within a certain distance to interact
        float interactionDistance = 2f;
        return Vector3.Distance(player.transform.position, transform.position) <= interactionDistance;
    }

    public void Interact(GameObject player)
    {
        Debug.Log($"[InteractiveFountain] {GetInteractionName()}: {gameObject.name}");
        ExecuteInteraction(player);
    }

    private void ExecuteInteraction(GameObject player)
    {
        // ✓ Example interaction logic: restore player's health
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(HealAmount); // Restore 50 health points
            gameObject.layer = LayerMask.NameToLayer("Default");
            Debug.Log($"[InteractiveFountain] Restored health to player!");
        }
        else
        {
            Debug.LogWarning($"[InteractiveFountain] Player does not have a PlayerHealth component!");
        }
    }
}
