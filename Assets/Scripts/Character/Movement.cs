using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] float moveSpeed = 5f;
    private float defaultMoveSpeed;

    Rigidbody2D rb2d;

    Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.right; // กัน 0

    private bool canMove = true;

    void Awake()
    {
        defaultMoveSpeed = moveSpeed;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        rb2d.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        rb2d.linearVelocity = moveInput * moveSpeed;
    }

    #region IMovement
    public void ResetVelocity() => rb2d.linearVelocity = Vector2.zero;
    public void ApplyImpulse(Vector2 direction, float magnitude)
     {
        Vector2 finalDirection = direction.sqrMagnitude > 0.0001f
            ? direction.normalized
            : lastMoveDir;
            
         rb2d.AddForce(finalDirection * magnitude, ForceMode2D.Impulse);
     }
    public void EnableMovement() => canMove = true;
    public void DisableMovement() => canMove = false;
    #endregion

    #region Input
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;

        // 🔥 อัพเดท last direction เฉพาะตอนมี input
        if (input != Vector2.zero)
            lastMoveDir = input.normalized;
    }

    public Vector2 GetMoveInput() => moveInput;

    public void StopMovement() => moveInput = Vector2.zero;

    #endregion

    #region Direction

    // 🔥 helper ใช้ตรง ๆ ได้เลย (แนะนำ)
    public Vector2 GetDirection()
    {
        if (moveInput != Vector2.zero)
            return moveInput.normalized;

        return lastMoveDir;
    }

    #endregion

    #region Speed

    public void SetMoveSpeed(float newSpeed) => moveSpeed = newSpeed;
    public void AddMoveSpeed(float newSpeed)
    {
        moveSpeed += newSpeed;
    }

    public void ResetMoveSpeed() => moveSpeed = defaultMoveSpeed;

    public float GetMoveSpeed() => moveSpeed;

    #endregion
}