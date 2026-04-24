using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Invincibility")]
public class Ef_Invincibility : ItemEffect
{
    public float duration = 10f;

    public override bool IsActive { get; set; } = false;

    public override void Apply(GameObject target, bool IsActive = false)
    {
        var player = target.GetComponent<IFrameController>();
        if (player != null)
        {
            player.AddDuration(duration);
        }
    }
}
