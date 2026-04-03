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
        var devices = new System.Collections.Generic.List<InputDevice>();
        if (controlType == ControlType.Keyboard)
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;
            if (keyboard != null) devices.Add(keyboard);
            if (mouse != null) devices.Add(mouse);
        }
        else if (controlType == ControlType.Gamepad)
        {
            var gamepad = Gamepad.current;
            if (gamepad != null) devices.Add(gamepad);
        }

        if (devices.Count > 0)
        {
            playerInput.SwitchCurrentControlScheme(schemeName, devices.ToArray());
            Debug.Log($"✓ Switched to {schemeName} with devices: {string.Join(", ", devices.Select(d => d.displayName))}");
        }
        else
        {
            Debug.LogWarning($"❌ No device found for {schemeName}!\n" +
                $"Current devices: {string.Join(", ", InputSystem.devices.Select(d => d.displayName))}");
        }
    }
}
