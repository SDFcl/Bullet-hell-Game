using UnityEngine;
using System;

public class AimPivot2D : MonoBehaviour
{
    [SerializeField] private Transform aimPivot;
    [SerializeField] private float resetLerpSpeed = 10f;

    private Vector2 currentDirection = Vector2.right;
    private Quaternion startLocalRotation;
    private bool isResettingRotation;
    private Action onResetComplete;

    public Vector2 CurrentDirection => currentDirection;

    private void Awake()
    {
        if (aimPivot == null) return;
        startLocalRotation = aimPivot.localRotation;
    }

    private void Update()
    {
        if (aimPivot == null || !isResettingRotation) return;

        aimPivot.localRotation = Quaternion.Lerp(
            aimPivot.localRotation,
            startLocalRotation,
            Time.deltaTime * resetLerpSpeed
        );

        if (Quaternion.Angle(aimPivot.localRotation, startLocalRotation) < 0.1f)
        {
            aimPivot.localRotation = startLocalRotation;
            isResettingRotation = false;

            onResetComplete?.Invoke();
            onResetComplete = null;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        if (aimPivot == null) return;
        if (direction.sqrMagnitude <= 0.0001f) return;

        isResettingRotation = false;

        currentDirection = direction.normalized;

        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
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

    public void ResetRotation(Action callback = null)
    {
        if (aimPivot == null) return;

        onResetComplete = callback;
        isResettingRotation = true;
    }
}
