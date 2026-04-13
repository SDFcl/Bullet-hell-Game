using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Increase Total Damage")]
public class Ef_IncreaseTotalDamage : ItemEffect
{
    public float DamageIncreasePercent = 0.15f; // 15% increase in total damage
    public bool IsActive = false;

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<Attack>();
        if (!IsActive)
        {
            if (player != null)
            {
                player.AddDamagePercent(DamageIncreasePercent); // Increase total damage by 15%
                IsActive = true;
            }
        }
        else
        {
            if (player != null)
            {
                player.RemoveDamagePercent(DamageIncreasePercent); // Revert the damage increase
                IsActive = false;
            }
        }
    }
}
