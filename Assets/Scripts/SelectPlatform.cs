using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlatform : MonoBehaviour
{


    public void OnClick(int value)
    {
        if (value == 0)
        {
            SetControlType(ControlType.Keyboard);
        }
        else if (value == 1)
        {
            SetControlType(ControlType.Gamepad);
        }
    }

    public void SetControlType(ControlType newType)
    {
        GameManager.instance._controlType = newType;
        Debug.Log("เปลี่ยน Control Type เป็น: " + GameManager.instance._controlType);

        // สามารถเพิ่ม logic อื่น ๆ ได้ เช่น บันทึกการตั้งค่า
        // PlayerPrefs.SetInt("ControlType", (int)newType);
    }
}
