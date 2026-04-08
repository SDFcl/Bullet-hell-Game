using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Infinite Mana")]
public class Ef_InfiniteMana : ItemEffect
{
    public float Duration = 30f; // Duration of the effect in seconds

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<Mana>();
        if (player != null)
        {
            player.EnableIgnoreManaCost(true);
            EffectCoroutineRunner.Run(RemoveAfterTime(player));
        }
    }

    public IEnumerator RemoveAfterTime(Mana player)
    {
        yield return new WaitForSeconds(Duration);
        if (player != null)
        {
            player.EnableIgnoreManaCost(false); // Revert the effect
        }
    }
}
