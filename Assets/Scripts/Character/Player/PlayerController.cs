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
    private Health health;

    private Vector2 dir;

    private bool isAiming;
    private bool isFireHeld;
    private bool firePressedThisFrame;
    private bool holdTriggered;

    private float firePressedTime;
    private float holdThreshold = 0.1f;

    private IPlayerStats Stats;
    public PlayerUpgradeManager PlayerUpgradeManager;

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
        health = GetComponent<Health>();
        PlayerUpgradeManager =  FindObjectOfType<PlayerUpgradeManager>();
        if (PlayerUpgradeManager != null)
        {
            Stats = PlayerUpgradeManager.GetFinalStats();
            if (health != null)
            {
                PlayerHealth playerHealth = health.GetComponent<PlayerHealth>();
                Debug.Log("PlayerController: Found Health component, trying to set PlayerHealth MaxHealth to " + Stats.MaxHealth);
                if (playerHealth != null)
                {
                    playerHealth.MaxHealth += Stats.MaxHealth;
                    Debug.Log("PlayerController: Set PlayerHealth MaxHealth to " + playerHealth.MaxHealth);
                }
                if (attack != null)
                {
                    attack.AddDamagePercent(Stats.IncreaseDamage);
                    Debug.Log("PlayerController: Added " + Stats.IncreaseDamage + "% damage to Attack component");
                }
            }
        }

        if (mainCamera == null)
            mainCamera = Camera.main;  
    }
    void OnEnable()
    {
        health.OnDead += OnDeadHandle;
    }
    void OnDisable()
    {
        health.OnDead -= OnDeadHandle;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        movement.SetMoveInput(moveInput);
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 aimInput = context.ReadValue<Vector2>();

            if (aimInput.sqrMagnitude > 0.0001f)
            {
                isAiming = true;
                dir = aimInput.normalized;
            }
        }
        else if (context.canceled)
        {
            isAiming = false;
            dir = Vector2.zero;
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFireHeld = true;
            firePressedThisFrame = true;
            holdTriggered = false;
            firePressedTime = Time.time;
        }
        else if (context.canceled)
        {
            isFireHeld = false;
            holdTriggered = false;
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
        //Debug.Log("Scroll Input: " + scroll);   // ����礤���ѹ detect ��������
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
        dodge.SetDirection(dir);

        if (firePressedThisFrame)
        {
            attack.TryAttack();
            firePressedThisFrame = false;
        }

        if (isFireHeld && Time.time - firePressedTime >= holdThreshold)
        {
            holdTriggered = true;
        }

        if (holdTriggered || isAiming)
        {
            attack.TryAttack();
        }
    }
    private void OnDeadHandle()
    {
        playerInput.enabled = false;
    }
}
