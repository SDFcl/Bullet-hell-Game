using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Increase Move Speed")]
public class Ef_IncreaseMoveSpeed : ItemEffect
{
    public float Duration = 15f; // ����ʱ��
    public float speedIncreaseAmount = 0f; // �ӹǹ���������������
    public float speedIncreaseMultiplier = 1.5f; // �����������������

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<Movement>();
        if (player != null)
        {
            float originalSpeed = player.GetMoveSpeed();
            player.SetMoveSpeed((originalSpeed + speedIncreaseAmount) * speedIncreaseMultiplier);
            EffectCoroutineRunner.Run(RemoveAfterTime(player, originalSpeed));
        }
    }

    private IEnumerator RemoveAfterTime(Movement player, float originalSpeed)
    {
        yield return new WaitForSeconds(Duration);
        if (player != null)
        {
            player.SetMoveSpeed(originalSpeed);
        }
    }
}
