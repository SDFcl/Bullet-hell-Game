using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimTargetFollowMouse : MonoBehaviour
{
    private Camera inputCamera;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if(inputCamera == null)
        {
            inputCamera = GameObject.FindGameObjectWithTag("Input Camera").GetComponent<Camera>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (inputCamera == null) return;

        Vector3 mouseWorld = inputCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0f;
        transform.position = mouseWorld;
    }

    void OnEnable()
    {
        EventBus.Subscribe<GameStateChangedEvent>(EnableMyself);
    }
    void OnDisable()
    {
        EventBus.Unsubscribe<GameStateChangedEvent>(EnableMyself);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        spriteRenderer.color = new Color(1,1,1,0);
    }

    void EnableMyself(GameStateChangedEvent e)
    {
        if(e.NewState == GameState.GamePlay)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            spriteRenderer.color = new Color(1,1,1,1);
        }
        if(e.NewState == GameState.Paused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            spriteRenderer.color = new Color(1,1,1,0);
        }
    }
}
