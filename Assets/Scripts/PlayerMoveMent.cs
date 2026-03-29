using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb2d;
    Vector2 moveInput;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        rb2d.gravityScale = 0f;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        //Debug.Log("Move Input: " + moveInput);   // ไว้เช็คค่ามัน smooth มั้ย
        rb2d.MovePosition(rb2d.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}