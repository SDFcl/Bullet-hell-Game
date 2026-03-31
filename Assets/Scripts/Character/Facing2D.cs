using UnityEngine;

public class Facing2D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool faceRight = true;

    public bool IsFacingRight => faceRight;

    public void SetDirection(float direction)
    {
        if (direction == 0) return;

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
        scale.x *= -1;
        transform.localScale = scale;
    }
}