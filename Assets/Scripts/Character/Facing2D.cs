using UnityEngine;

public class Facing2D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool faceRight = true;
    [SerializeField] private float directionThreshold = 0.1f;

    public bool IsFacingRight => faceRight;

    public void SetDirection(float direction)
    {
        // ถ้าแกน x น้อยเกินไป ให้ถือว่ายังไม่ต้องเปลี่ยนหน้า
        if (Mathf.Abs(direction) < directionThreshold)
            return;

        bool shouldFaceRight = direction > 0;

        if (shouldFaceRight != faceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        faceRight = !faceRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}