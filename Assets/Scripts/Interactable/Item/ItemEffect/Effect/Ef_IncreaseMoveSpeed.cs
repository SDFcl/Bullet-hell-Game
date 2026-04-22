using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Increase Move Speed")]
public class Ef_IncreaseMoveSpeed : ItemEffect
{
    public float Duration = 15f; // ����ʱ��
    public float speedIncreaseMultiplier = 1.5f; // �����������������
    float speed = 0f;

    public bool IsActive = false;

    public override void Apply(GameObject target)
    {
        var player = target.GetComponent<Movement>();
        if (player != null)
        {
            if (!IsActive)
            {
                float originalSpeed = player.GetMoveSpeed();
                speed = originalSpeed * speedIncreaseMultiplier;
                player.AddMoveSpeed(speed);
                IsActive = true;
            }
            else
            {
                if (player != null)
                {
                    player.AddMoveSpeed(-speed);
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
