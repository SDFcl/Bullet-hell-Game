using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Increase Move Speed")]
public class Ef_IncreaseMoveSpeed : ItemEffect
{
    public float Duration = 15f; // ����ʱ��
    public float speedIncreaseMultiplier = 1.5f; // �����������������

    public bool IsActive = false;

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<Movement>();
        if (player != null)
        {
            float originalSpeed = player.GetMoveSpeed();
            if (!IsActive)
            {

                player.SetMoveSpeed(originalSpeed * speedIncreaseMultiplier);
                IsActive = true;
                EffectCoroutineRunner.Run(RemoveAfterTime(player, originalSpeed));
            }
            else
            {
                if (player != null)
                {
                    player.SetMoveSpeed(originalSpeed / speedIncreaseMultiplier);
                    IsActive = false;
                }
            }
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
