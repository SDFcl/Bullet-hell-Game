using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnLockNewMap : MonoBehaviour
{
    public LevelSelectController levelSelectController;

    [Header("UI Elements")]
    public Button button;
    public GameObject lockPanel;

    public void UnlockMap()
    {
        lockPanel.SetActive(false);
        button.interactable = false;
    }
}
