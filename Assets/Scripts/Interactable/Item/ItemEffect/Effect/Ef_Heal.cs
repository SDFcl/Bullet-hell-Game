using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class Ef_HealEffect : ItemEffect
{
    public int healAmount;
    public override bool IsActive { get; set; } = false;

    public override void Apply(GameObject target, bool IsActive = false)
    {
        var player = target.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.Heal(healAmount);
        }
    }
}