using UnityEngine;

public class EnemyAnimation : CharacterAnimations
{
    [SerializeField] string portalTag;
    void Start()
    {
        spawn();
    }

    private void spawn()
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool(portalTag,transform.position,Quaternion.identity);

        if (projectile == null)
        {
            Debug.LogWarning("Failed to spawn projectile from pool");
            return;
        }
    }
}
