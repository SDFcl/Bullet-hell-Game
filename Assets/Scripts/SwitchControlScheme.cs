using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchControlScheme : MonoBehaviour
{
    [SerializeField] ControlType controlType; // ถ้ายังใช้ enum นี้อยู่
    [SerializeField] PlayerInput playerInput;

    public bool mannoaSwitchControlScheme = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (GameManager.instance != null)
        {
            controlType = GameManager.instance._controlType;
            MannoaSwitchControlScheme();
            return;
        }

        if (mannoaSwitchControlScheme)
        {
            MannoaSwitchControlScheme();
            return; // ถ้าใช้แบบ manual ก็ไม่ต้องทำ auto ต่อ
        }  

        // ป้องกันการสลับอัตโนมัติบ่อยเกินไป (แนะนำเปิดถ้าเจอปัญหา)
        // playerInput.neverAutoSwitchControlSchemes = true;

        //AutoDetectAndSwitchControlScheme();   // ตรวจสอบและสลับทันทีตอนเริ่มเกม
    }

    public void OnControlsChanged(PlayerInput input)
    {
        Debug.Log($"Control scheme changed to: {input.currentControlScheme} | Device: {input.devices[0]?.displayName}");

        // เรียกตรวจสอบใหม่ทุกครั้งที่มีการเปลี่ยน (optional)
        // AutoDetectAndSwitchControlScheme();
    }

    /// <summary>
    /// ฟังก์ชันหลัก: เช็คประเภทอุปกรณ์แล้วสลับ Control Scheme อัตโนมัติ
    /// </summary>
    public void AutoDetectAndSwitchControlScheme()
    {
        DeviceType deviceType = SystemInfo.deviceType;
        string schemeName = "Keyboard&Mouse";   // ค่าเริ่มต้น
        InputDevice targetDevice = null;

        // === กรณีเป็นมือถือ / แท็บเล็ต / Handheld PC ===
        if (deviceType == DeviceType.Handheld || Application.isMobilePlatform)
        {
            Debug.Log("ตรวจพบ Handheld / Mobile Platform");

            // ถ้ามี Touchscreen → ใช้ Touch scheme (สำคัญที่สุดสำหรับมือถือ)
            if (Touchscreen.current != null)
            {
                schemeName = "Touch";                    // ต้องมี Control Scheme ชื่อ "Touch" ใน Input Action
                targetDevice = Touchscreen.current;
                Debug.Log("→ ใช้ Touch Control Scheme");
            }
            // ถ้าไม่มี Touch แต่มี Gamepad (เช่น Steam Deck, ROG Ally)
            else if (Gamepad.current != null)
            {
                schemeName = "Gamepad";
                targetDevice = Gamepad.current;
                Debug.Log("→ ใช้ Gamepad บน Handheld");
            }
        }
        // === กรณีเป็นคอมพิวเตอร์ ===
        else if (deviceType == DeviceType.Desktop)
        {
            // 优先 Keyboard & Mouse
            if (Keyboard.current != null)
            {
                schemeName = "Keyboard&Mouse";                 // หรือ "Keyboard&Mouse" ก็ได้
                targetDevice = Keyboard.current;
                Debug.Log("→ ใช้ Keyboard (Desktop)");
            }
            else if (Gamepad.current != null)
            {
                schemeName = "Gamepad";
                targetDevice = Gamepad.current;
                Debug.Log("→ ใช้ Gamepad บน Desktop");
            }
        }
        // === กรณีเป็นคอนโซล ===
        else if (deviceType == DeviceType.Console)
        {
            if (Gamepad.current != null)
            {
                schemeName = "Gamepad";
                targetDevice = Gamepad.current;
                Debug.Log("→ ใช้ Gamepad บน Console");
            }
        }

        // ถ้ายังหาไม่ได้ ให้ fallback ไปเช็ค Gamepad ก่อน
        if (targetDevice == null && Gamepad.current != null)
        {
            schemeName = "Gamepad";
            targetDevice = Gamepad.current;
        }

        // === สลับ Control Scheme ===
        if (targetDevice != null)
        {
            playerInput.SwitchCurrentControlScheme(schemeName, targetDevice);
            Debug.Log($"✓ สลับสำเร็จเป็น {schemeName} ด้วยอุปกรณ์: {targetDevice.displayName}");
        }
        else
        {
            Debug.LogWarning($"❌ ไม่พบอุปกรณ์ที่เหมาะสม! ปัจจุบันมี: " +
                string.Join(", ", InputSystem.devices.Select(d => d.displayName)));
        }
    }

    // ฟังก์ชันเก่า (ถ้ายังต้องการใช้ manual)
    void MannoaSwitchControlScheme()
    {
        string schemeName = (controlType == ControlType.Keyboard)
                                            ? "Keyboard&Mouse"
                                            : "Gamepad";
        InputDevice device = null;
        if (controlType == ControlType.Keyboard)
        {
            device = Keyboard.current; // หรือ Keyboard.current ?? Mouse.current;
        }
        else if (controlType == ControlType.Gamepad)
        {
            device = Gamepad.current;
        }
        if (device != null)
        {
            playerInput.SwitchCurrentControlScheme(schemeName, device);
            Debug.Log($"✓ Switched to {schemeName} with device: {device.displayName}");
        }
        else
        {
            Debug.LogWarning($"❌ No device found for {schemeName}!\n" +
            $"Current devices: {string.Join(", ", InputSystem.devices.Select(d => d.displayName))}");
        }
    }
}