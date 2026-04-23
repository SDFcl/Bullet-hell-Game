using UnityEngine;
using UnityEngine.UI;

public class ChangeStage : MonoBehaviour
{
    public Toggle toggle;
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        if (toggle == null || canvasGroup == null) return;

        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        SetCanvasGroupState(toggle.isOn);
    }

    private void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }

    public void ChangeState()
    {
        if (toggle == null) return;

        SetCanvasGroupState(toggle.isOn);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        SetCanvasGroupState(isOn);
    }

    private void SetCanvasGroupState(bool isOn)
    {
        if (canvasGroup == null) return;

        canvasGroup.alpha = isOn ? 1f : 0f;
        canvasGroup.interactable = isOn;
        canvasGroup.blocksRaycasts = isOn;
    }
}
