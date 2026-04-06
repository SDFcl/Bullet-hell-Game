using UnityEngine;

/// <summary>
/// ✓ Chest - Interactive object that gives random rewards when opened
/// </summary>
public class Chest : InteractiveObject
{
    [Header("Chest Settings")]
    [SerializeField] private RandomItem randomItem;
    [SerializeField] private bool isOpened = false;

    [Header("Visual Feedback")]
    [SerializeField] private Animator animator;
    [SerializeField] private string openAnimationTrigger = "Open";

    public override string GetInteractionName()
    {
        return isOpened ? "Chest (Opened)" : "Open Chest";
    }

    public override bool CanInteract(GameObject player)
    {
        if (isOpened)
        {
            Debug.Log("Chest is already opened!");
            return false;
        }

        // Check base conditions (distance, keys, etc.)
        return base.CanInteract(player);
    }

    protected override void ExecuteInteraction()
    {
        if (isOpened) return;

        isOpened = true;

        // Play open animation
        if (animator != null)
        {
            animator.SetTrigger(openAnimationTrigger);
        }

        // Give random rewards
       
        SpawnReward(randomItem.GetRandomItem());

        if(TryGetComponent(out BoxCollider2D collider))
        {
            collider.enabled = false; // Disable collider after opening
        }
        this.gameObject.layer = LayerMask.NameToLayer("Default"); // Change layer to prevent further interactions
    }

    private void SpawnReward(GameObject reward)
    {
        if (reward == null)
        {
            Debug.LogWarning("[Chest] Invalid reward data or missing prefab!");
            return;
        }

        // Spawn reward near the chest
        Vector3 spawnPosition = transform.position + new Vector3(
            Random.Range(-0.5f, 0.5f),
            Random.Range(0.5f, 1.5f),
            0f
        );

        Instantiate(reward, spawnPosition, Quaternion.identity);
    }
}
