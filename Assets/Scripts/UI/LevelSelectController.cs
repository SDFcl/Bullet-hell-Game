using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    [Header("For test")]
    public int MaxLevel;
    public TextMeshProUGUI TextMapCurrent;
    public Toggle toggle;

    private SelectLevel SelectLevel;
    private CanvasGroup CanvasGroup;

    public int currentIndex;

    private void Awake()
    {
        SelectLevel = GetComponent<SelectLevel>();
        currentIndex = SelectLevel.getLevelMap();
        CanvasGroup = GetComponent<CanvasGroup>();

        ChangeState();
    }

    public void Next()
    {
        currentIndex = (currentIndex + 1) % (MaxLevel+1);
        if (currentIndex <= 0)
        {
            currentIndex++;
        }
        UpdateUI();
    }

    public void Prev()
    {
        currentIndex--;
        if (currentIndex <= 0)
            currentIndex = MaxLevel;

        UpdateUI();
    }

    private void UpdateUI()
    {
        SelectLevel.setLevelMap(currentIndex);
        TextMapCurrent.text = "Map " + currentIndex;
    }

    public void ChangeState()
    {
        switch (toggle.isOn)
        {
            case false:
                CanvasGroup.alpha = 0.0f; 
                break;
            case true:
                CanvasGroup.alpha = 1.0f;
                break;
        }
    }
}
