using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class Ef_HealEffect : ItemEffect
{
    public int healAmount;

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.Heal(healAmount);
        }
    }
}