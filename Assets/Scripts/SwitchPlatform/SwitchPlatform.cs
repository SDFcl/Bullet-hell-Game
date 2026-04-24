using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchPlatform : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup GamepadUI; // UI สำหรับมือถือ
    public CanvasGroup KeyboardAndMouseUI; // UI สำหรับเดสก์ท็อป

    [Header("Input System")]
    public PlayerInput playerInput; // ตัวแปรเพื่อเข้าถึงระบบ Input System
    private ControlType controlType; // ตัวแปรเพื่อเก็บประเภทการควบคุม (Keyboard หรือ Gamepad)

    public bool isMobile = false; // ตัวแปรเพื่อกำหนดว่าเป็นเดสก์ท็อปหรือมือถือ

    private void Awake()
    {
        controlType = ControlType.Keyboard; // กำหนดค่าเริ่มต้นเป็น Keyboard
        if (GamepadUI != null)
            GamepadUI.alpha = isMobile ? 1 : 0; // แสดง UI มือถือถ้าเป็นมือถือ
        if (KeyboardAndMouseUI != null)
            KeyboardAndMouseUI.alpha = isMobile ? 0 : 1; // แสดง UI เดสก์ท็อปถ้าไม่เป็นมือถือ
    }

    public void switchPlatform()
    {
        isMobile = !isMobile;
        if (isMobile)
        {
            controlType = ControlType.Gamepad; // เปลี่ยนเป็น Gamepad เมื่อเป็นมือถือ
        }
        else
        {
            controlType = ControlType.Keyboard; // เปลี่ยนเป็น Keyboard เมื่อเป็นเดสก์ท็อป
        }
        if (GamepadUI != null)
            GamepadUI.alpha = isMobile ? 1 : 0; // แสดง UI มือถือถ้าเป็นมือถือ
        if (KeyboardAndMouseUI != null)
            KeyboardAndMouseUI.alpha = isMobile ? 0 : 1; // แสดง UI เดสก์ท็อปถ้าไม่เป็นมือถือ

        MannoaSwitchControlScheme(); // เรียกฟังก์ชันเพื่อสลับ Control Scheme ตามประเภทการควบคุม
    }

    void MannoaSwitchControlScheme()
    {
        string schemeName = (controlType == ControlType.Keyboard)
            ? "Keyboard&Mouse"
            : "Gamepad";

        if (controlType == ControlType.Keyboard)
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;

            if (keyboard != null && mouse != null)
            {
                playerInput.SwitchCurrentControlScheme(
                    schemeName,
                    keyboard,
                    mouse
                );

                Debug.Log($"✓ Switched to {schemeName} (Keyboard + Mouse)");
            }
            else
            {
                Debug.LogWarning("❌ Keyboard or Mouse not found!");
            }
        }
        else if (controlType == ControlType.Gamepad)
        {
            var gamepad = Gamepad.current;

            if (gamepad != null)
            {
                playerInput.SwitchCurrentControlScheme(
                    schemeName,
                    gamepad
                );

                Debug.Log($"✓ Switched to {schemeName}");
            }
            else
            {
                Debug.LogWarning("❌ No Gamepad found!");
            }
        }
    }
}
