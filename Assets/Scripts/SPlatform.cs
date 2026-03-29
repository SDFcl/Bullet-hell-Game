using UnityEngine;

public class SPlatform : MonoBehaviour
{
    public CanvasGroup mobileUI; // UI สำหรับมือถือ
    public CanvasGroup desktopUI; // UI สำหรับเดสก์ท็อป

    private void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogWarning("SelectPlatform instance not found. Defaulting to desktop UI.");
            if (desktopUI != null) desktopUI.alpha = 1;
            if (mobileUI != null) mobileUI.alpha = 0;
            return;
        }
        bool isMobile = GameManager.instance._controlType.ToString() == "Gamepad";
        if (mobileUI != null)
            mobileUI.alpha = isMobile ? 1 : 0; // แสดง UI มือถือถ้าเป็นมือถือ
        if (desktopUI != null)
            desktopUI.alpha = isMobile ? 0 : 1; // แสดง UI เดสก์ท็อปถ้าไม่เป็นมือถือ
    }
}
