using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ControlType _controlType = ControlType.Keyboard;   // ตั้งค่าเริ่มต้น


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method สำหรับ Button เรียกได้เลย (รับค่า enum)

}