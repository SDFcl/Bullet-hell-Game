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
            float originalSpeed = player.GetMoveSpeed();
            speed = originalSpeed * speedIncreaseMultiplier;
            Debug.Log($"Original Speed: {originalSpeed}, Increased Speed: {speed}");
            player.AddMoveSpeed(speed);
            EffectCoroutineRunner.Run(RemoveAfterTime(player));
        }
    }

    public IEnumerator RemoveAfterTime(Movement player)
    {
        yield return new WaitForSeconds(Duration);
        if (player != null)
        {
            player.AddMoveSpeed(-speed); // Revert the speed increase
        }
    }
}
