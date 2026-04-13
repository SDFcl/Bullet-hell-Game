using UnityEngine;

public class AimPivot2D : MonoBehaviour
{
    [SerializeField] private Transform aimPivot;
    [SerializeField] private float rotationLerpSpeed = 12f;

    private Vector2 currentDirection = Vector2.right;
    private Quaternion initialLocalRotation;
    private Vector3 initialLocalScale;

    public Vector2 CurrentDirection => currentDirection;

    private void Awake()
    {
        if (aimPivot == null) return;

        initialLocalRotation = aimPivot.localRotation;
        initialLocalScale = aimPivot.localScale;
    }

    public void SetDirection(Vector2 direction)
    {
        if (aimPivot == null) return;
        if (direction.sqrMagnitude <= 0.0001f) return;

        currentDirection = direction.normalized;

        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        aimPivot.rotation = Quaternion.Lerp(
            aimPivot.rotation,
            targetRotation,
            Time.deltaTime * rotationLerpSpeed
        );

        bool isLeftSide = angle > 90f || angle < -90f;

        Vector3 scale = aimPivot.localScale;
        float absX = Mathf.Abs(initialLocalScale.x);
        float absY = Mathf.Abs(initialLocalScale.y);

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

        aimPivot.localRotation = initialLocalRotation;
        aimPivot.localScale = initialLocalScale;
    }
}
