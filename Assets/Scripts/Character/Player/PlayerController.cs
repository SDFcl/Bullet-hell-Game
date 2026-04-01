using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Facing2D facing;
    private AimPivot2D aimPivot;
    private PlayerInput playerInput;
    private Dodge dodge;
    private RayInteract rayInteract;
    private HoldingItem holdingItem;

    Vector2 dir;

    [SerializeField] private Camera mainCamera;

    void Awake()
    {
        movement = GetComponent<Movement>();
        facing = GetComponent<Facing2D>();
        aimPivot = GetComponentInChildren<AimPivot2D>();
        playerInput = GetComponent<PlayerInput>();
        dodge = GetComponent<Dodge>();
        rayInteract = GetComponent<RayInteract>();
        holdingItem = GetComponentInChildren<HoldingItem>();

        if (mainCamera == null)
            mainCamera = Camera.main;  
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        movement.SetmoveInput(moveInput);
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            //Attack
            return;
        }

        //Aim
        Vector2 aimInput = context.ReadValue<Vector2>();
        dir = aimInput;
        Debug.Log("Aim Input: " + aimInput);   // ไว้เช็คค่ามัน smooth มั้ย
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        Debug.Log("Dodge Button Pressed");   // ไว้เช็คว่ามัน detect ปุ่มมั้ย
        if (dodge.enabled == false) return;
        dodge.TryDodge();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (rayInteract == null) return;
            rayInteract.PickUp();
        }
    }

    private float scrollCooldown = 0.2f; // ปรับได้
    private float lastScrollTime;

    public void OnHoldItem(InputAction.CallbackContext context)
    {
        if (holdingItem == null) return;

        if (Time.time - lastScrollTime < scrollCooldown) return;

        float scroll = context.ReadValue<Vector2>().y;
        Debug.Log("Scroll Input: " + scroll);   // ไว้เช็คค่ามัน smooth มั้ย
        if (scroll > 0f)
        {
            holdingItem.HoldItem(1);
        }
        else if (scroll < 0f)
        {
            holdingItem.HoldItem(-1);
        }
        lastScrollTime = Time.time;
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorld.z = 0f;
            dir = mouseWorld - transform.position;
            //Debug.Log("Mouse World Position: " + mouseWorld);   // ไว้เช็คค่ามัน smooth มั้ย
        }
              
        facing.SetDirection(dir.x);
        aimPivot.SetDirection(dir);
    }
}
