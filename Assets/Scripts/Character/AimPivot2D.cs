using UnityEngine;

public class AimPivot2D : MonoBehaviour
{
    [SerializeField] private Transform aimPivot;

    public void SetDirection(Vector2 direction)
    {
        if (aimPivot == null) return;
        if (direction.sqrMagnitude <= 0.0001f) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        bool isLeftSide = angle > 90f || angle < -90f;

        Vector3 scale = aimPivot.localScale;
        float absX = Mathf.Abs(scale.x);
        float absY = Mathf.Abs(scale.y);

        if (isLeftSide)
        {
            scale.x = -absX;
            scale.y = -absY;
        }
        else
        {
            scale.x = absX;
            scale.y = absY;
        }

        aimPivot.localScale = scale;
    }

    public void ResetRotation()
    {
        if (aimPivot == null) return;
        aimPivot.localRotation = Quaternion.identity;
    }
}