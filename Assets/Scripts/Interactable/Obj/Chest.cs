using UnityEngine;

/// <summary>
/// ✓ Chest - Interactive object that gives random rewards when opened
/// </summary>
public class Chest : InteractiveObject
{
    [Header("Chest Settings")]
    [SerializeField] private StartRandomItem randomItem;
    [SerializeField] private bool isOpened = false;

    [SerializeField] Sprite sprite;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override string GetInteractionName()
    {
        return isOpened ? "Chest (Opened)" : "Open Chest";
    }

    protected override void ExecuteInteraction(GameObject player)
    {
        if (isOpened) return;

        isOpened = true;

        // Play open animation
        spriteRenderer.sprite = sprite;

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
