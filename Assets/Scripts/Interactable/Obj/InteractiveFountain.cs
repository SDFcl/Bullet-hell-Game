using UnityEngine;

public class InteractiveFountain : InteractiveObject
{
    public int HealAmount = 2;

    public override string GetInteractionName()
    {
        return "Drink";
    }

    public override bool CanInteract(GameObject player)
    {
        float interactionDistance = 2f;
        return Vector3.Distance(player.transform.position, transform.position) <= interactionDistance;
    }

    protected override void ExecuteInteraction(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(HealAmount);
            gameObject.layer = LayerMask.NameToLayer("Default");
            Debug.Log("[InteractiveFountain] Restored health to player!");
        }
        else
        {
            Debug.LogWarning("[InteractiveFountain] Player does not have a PlayerHealth component!");
        }
    }
}
