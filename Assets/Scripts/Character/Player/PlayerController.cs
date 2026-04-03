using Unity.VisualScripting;
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
    private HoldingWeapon holdingItem;
    private Inventory inventory;

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
        holdingItem = GetComponentInChildren<HoldingWeapon>();
        inventory = GetComponentInChildren<Inventory>();

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
        Debug.Log("Aim Input: " + aimInput);   // ����礤���ѹ smooth ����
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Dodge Button Pressed");   // ���������ѹ detect ��������
        if (dodge.enabled == false) return;
        Debug.Log("Try Dodge");
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

    private float scrollCooldown = 0.2f; // ��Ѻ��
    private float lastScrollTime;

    public void OnHoldItem(InputAction.CallbackContext context)
    {
        if (holdingItem == null) return;

        if (Time.time - lastScrollTime < scrollCooldown) return;

        float scroll = context.ReadValue<Vector2>().y;
        //Debug.Log("Scroll Input: " + scroll);   // ����礤���ѹ smooth ����
        if (scroll > 0f)
        {
            holdingItem.SetHoldingWeapon(-2, 1, true);
        }
        else if (scroll < 0f)
        {
            holdingItem.SetHoldingWeapon(-2, -1, true);
        }
        lastScrollTime = Time.time;
    }

    public void UseConsumable(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            foreach (ItemEffect itemEffect in inventory.Consumables[0].itemData.effects)
            {
                if (itemEffect != null)
                {
                    itemEffect.Apply(gameObject);
                    Debug.Log("Applied effect: " + itemEffect.name);   // ���������ѹ detect ��������
                }
                else
                {
                    Debug.Log("No effect found in the consumable item.");   // ���������ѹ detect ��������
                }
            }
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
    }
}
