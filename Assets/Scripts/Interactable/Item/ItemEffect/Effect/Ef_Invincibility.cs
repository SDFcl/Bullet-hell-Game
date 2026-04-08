using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Invincibility")]
public class Ef_Invincibility : ItemEffect
{
    public float duration = 10f;

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<Health>();
        if (player != null)
        {
            //player.SetInvincibility(true);
            EffectCoroutineRunner.Run(RemoveAfterTime(player));
        }
    }

    private IEnumerator RemoveAfterTime(Health player)
    {
        yield return new WaitForSeconds(duration); // ����ʱ��
        if (player != null)
        {
            //player.SetInvincibility(false);
        }
    }
}
