using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Invincibility")]
public class Ef_Invincibility : ItemEffect
{
    public float duration = 10f;

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<IFrameController>();
        if (player != null)
        {
            player.AddDuration(duration);
        }
    }
}
