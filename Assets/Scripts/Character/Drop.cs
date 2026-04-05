using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private bool usePool = true;

    [Header("Pool Settings")]
    [SerializeField] private string poolTag;

    [Header("Item Setting")]
    [SerializeField] private GameObject itemPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void DropItem()
    {
        if (usePool)
        {
            GameObject item = ObjectPooler.Instance.SpawnFromPool(poolTag, transform.position, Quaternion.identity);
            if (item == null)
            {
                Debug.LogWarning("Failed to spawn item from pool with tag: " + poolTag);
            }
        }
        else
        {
            if (itemPrefab != null)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Item prefab is not assigned for drop.");
            }
        }
    }
}
