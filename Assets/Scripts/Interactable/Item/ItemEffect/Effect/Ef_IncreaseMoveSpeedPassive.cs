using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Increase Move Speed Passive")]
public class Ef_IncreaseMoveSpeedPassive : ItemEffect
{
    public float Duration = 15f; // ����ʱ��
    public float speedIncreaseMultiplier = 1.5f; // �����������������
    float speed = 0f;

    public override bool IsActive { get; set; } = false;

    public override void Apply(GameObject target, bool IsActive = false)
    {
        var player = target.GetComponent<Movement>();
        if (player != null)
        {
            if (!IsActive)
            {
                float originalSpeed = player.GetMoveSpeed();
                speed = 8f * speedIncreaseMultiplier;
                Debug.Log($"Original Speed: {originalSpeed}, Increased Speed: {speed}");
                player.AddMoveSpeed(speed);
                IsActive = true;
            }
            else
            {
                if (player != null)
                {
                    Debug.Log($"Reverting Speed: {-speed}");
                    player.AddMoveSpeed(-speed);
                    IsActive = false;
                }
            }
        }
    }
}
