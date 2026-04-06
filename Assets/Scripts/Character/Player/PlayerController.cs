using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Facing2D facing;
    private AimPivot2D aimPivot;
    private PlayerInput playerInput;
    private Dodge dodge;
    private RayInteract rayInteract;
    private HoldingWeapon holdingItem;
    private Inventory inventory;
    private Attack attack;
    private FilterInteract filterInteract;

    Vector2 dir;
    bool isfiring;

    [SerializeField] private Camera mainCamera;

    void Awake()
    {
        movement = GetComponent<Movement>();
        facing = GetComponent<Facing2D>();
        aimPivot = GetComponentInChildren<AimPivot2D>();
        playerInput = GetComponent<PlayerInput>();
        dodge = GetComponent<Dodge>();
        rayInteract = GetComponent<RayInteract>();
        holdingItem = GetComponentInChildren<HoldingWeapon>();
        inventory = GetComponentInChildren<Inventory>();
        attack = GetComponent<Attack>();
        filterInteract = new FilterInteract(gameObject);

        if (mainCamera == null)
            mainCamera = Camera.main;  
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        movement.SetMoveInput(moveInput);
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isfiring = true; // set firing state when button is pressed
        }
        if (context.performed)
        {
            Vector2 aimInput = context.ReadValue<Vector2>();
            if (aimInput.sqrMagnitude > 0.0001f)
            {
                dir = aimInput.normalized;
            }
            Debug.Log("Aim Input: " + aimInput);
        }
        else if (context.canceled)
        {
            // stick released; optionally stop aiming
            isfiring = false;
            dir = Vector2.zero;
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isfiring = true; // set firing state when button is pressed
        }
        else if (context.canceled)
        {
            isfiring = false; // reset firing state when button is released
        }
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        //Debug.Log("Dodge Button Pressed");   // ���������ѹ detect ��������
        if (dodge.enabled == false) return;
        //Debug.Log("Try Dodge");
        dodge.TryDodge();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (rayInteract == null || rayInteract.target == null) 
                return;

            // ✓ Use FilterInteract to automatically detect and execute interaction
            filterInteract.FilterAndExecute(rayInteract.target);
        }
    }

    private float scrollCooldown = 0.2f; // ��Ѻ��
    private float lastScrollTime;

    public void OnHoldItem(InputAction.CallbackContext context)
    {
        if (holdingItem == null) return;

        if (Time.time - lastScrollTime < scrollCooldown) return;

        Vector2 scroll = context.ReadValue<Vector2>();
        if (playerInput.currentControlScheme == "Gamepad")
        {
            scroll = context.ReadValue<Vector2>();
        }
            Debug.Log("Scroll Input: " + scroll);   // ����礤���ѹ detect ��������
        //Debug.Log("Scroll Input: " + scroll);   // ����礤���ѹ smooth ����
        if (scroll.y > 0f)
        {
            holdingItem.SetDirection(1);
        }
        else if (scroll.y < 0f)
        {
            holdingItem.SetDirection(-1);
        }
        lastScrollTime = Time.time;
    }

    public void UseConsumable(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventory.Consumables.Count == 0) return;
            inventory.Consumables[0].Use(gameObject, inventory.Consumables);
        }
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorld.z = 0f;
            dir = mouseWorld - transform.position;
            //Debug.Log("Mouse World Position: " + mouseWorld);   // ����礤���ѹ smooth ����
        }
              
        facing.SetDirection(dir.x);
        aimPivot.SetDirection(dir);

        if (isfiring)
        {
            attack.TryAttack();
        }
    }
}
