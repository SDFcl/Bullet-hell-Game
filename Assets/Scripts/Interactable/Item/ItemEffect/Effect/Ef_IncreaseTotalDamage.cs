using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Increase Total Damage")]
public class Ef_IncreaseTotalDamage : ItemEffect
{
    public float DamageIncreasePercent = 0.15f; // 15% increase in total damage
    public override bool IsActive { get; set; } = false;

    public override void Apply(GameObject target, bool IsActive = false)
    {
        var player = target.GetComponent<Attack>();
        if (!IsActive)
        {
            if (player != null)
            {
                player.AddDamagePercent(DamageIncreasePercent); // Increase total damage by 15%
                IsActive = true;
                Debug.Log($"Total damage increased by {DamageIncreasePercent * 100}%");
            }
        }
        else
        {
            if (player != null)
            {
                player.AddDamagePercent(-DamageIncreasePercent); // Revert the damage increase
                IsActive = false;
                Debug.Log($"Total damage reverted by {-DamageIncreasePercent * 100}%");
            }
        }
    }
}
