using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Facing2D facing;
    private AimPivot2D aimPivot;

    [SerializeField] private Camera mainCamera;

    void Awake()
    {
        movement = GetComponent<Movement>();
        facing = GetComponent<Facing2D>();
        aimPivot = GetComponentInChildren<AimPivot2D>();

        if (mainCamera == null)
            mainCamera = Camera.main;  
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        movement.SetmoveInput(moveInput);
    }
    private void Update()
    {
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0f;

        Vector2 dir = mouseWorld - transform.position;
        facing.SetDirection(dir.x);
        aimPivot.SetDirection(dir);
    }
}
