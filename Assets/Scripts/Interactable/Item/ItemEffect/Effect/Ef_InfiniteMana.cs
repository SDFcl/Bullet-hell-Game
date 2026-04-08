using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Infinite Mana")]
public class Ef_InfiniteMana : ItemEffect
{
    public float Duration = 30f; // Duration of the effect in seconds
    public float manaRegenRateBoostAmount = 9999f; // Arbitrary high value to simulate infinite mana

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<Mana>();
        if (player != null)
        {
            player.BoostRegenRate(manaRegenRateBoostAmount); // Arbitrary high value to simulate infinite mana
            EffectCoroutineRunner.Run(RemoveAfterTime(player));
        }
    }

    public IEnumerator RemoveAfterTime(Mana player)
    {
        yield return new WaitForSeconds(Duration);
        if (player != null)
        {
            player.BoostRegenRate(-manaRegenRateBoostAmount); // Revert the boost
        }
    }
}
