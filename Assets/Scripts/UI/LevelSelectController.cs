using UnityEngine;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    [Header("Stage")]
    public GameObject[] stageObjects;

    [Header("Dot Indicator")]
    public Image[] dots;
    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;
    public float activeScale = 1.2f;

    [Header("UI Control")]
    public Toggle toggle;
    private CanvasGroup canvasGroup;

    private SelectLevel selectLevel;

    public int currentIndex;
    private int MaxLevel => stageObjects.Length;

    private void Awake()
    {
        selectLevel = GetComponent<SelectLevel>();
        canvasGroup = GetComponent<CanvasGroup>();

        currentIndex = selectLevel.getLevelMap();
        UpdateUI();
        ChangeState();
    }

    public void Next()
    {
        currentIndex = currentIndex % MaxLevel + 1;
        UpdateUI();
    }

    public void Prev()
    {
        currentIndex--;
        if (currentIndex < 1) currentIndex = MaxLevel;
        UpdateUI();
    }

    private void UpdateUI()
    {
        selectLevel.setLevelMap(currentIndex);

        for (int i = 0; i < stageObjects.Length; i++)
        {
            stageObjects[i].SetActive(i == currentIndex - 1);
        }

        UpdateDots();
    }

    private void UpdateDots()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            bool isActive = (i == currentIndex - 1);

            dots[i].color = isActive ? activeColor : inactiveColor;
            dots[i].transform.localScale = isActive ? Vector3.one * activeScale : Vector3.one;
        }
    }

    public void ChangeState()
    {
        canvasGroup.alpha = toggle.isOn ? 1f : 0f;
    }
}