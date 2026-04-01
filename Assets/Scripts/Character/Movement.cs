using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private float defaultMoveSpeed;

    Rigidbody2D rb2d;
    Vector2 moveInput;
    public void SetmoveInput(Vector2 input) => moveInput = input;
    public void StopMovement() => moveInput = Vector2.zero;

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
        //Debug.Log("Move Input: " + moveInput);   // ไว้เช็คค่ามัน smooth มั้ย
        rb2d.MovePosition(rb2d.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    #region Basic API
    public void SetMoveSpeed(float newSpeed) => moveSpeed = newSpeed;
    public void ResetMoveSpeed() => moveSpeed = defaultMoveSpeed;
    public float GetMoveSpeed() => moveSpeed;
    #endregion
}