using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ButtonBuyData
{
    public bool isUnlock;
    public int Cost;

    ButtonBuyData(bool isUnlock, int cost): this()
    {
        this.isUnlock = isUnlock;
        this.Cost = cost;
    }
}

public class LevelSelectController : MonoBehaviour
{
    [Header("Stage")]
    public GameObject[] stageObjects;
    public List<ButtonBuyData> StageUnlock = new List<ButtonBuyData>();

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

    [Header("Enter Button")]
    public Button enterButton;
    public GameObject costText;

    private int MaxLevel => stageObjects.Length;

    private GameStateManager gameStateManager;
    private SelectLevel selectLevelComponent;

    private void Awake()
    {
        selectLevel = GetComponent<SelectLevel>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameStateManager = FindObjectOfType<GameStateManager>();
        selectLevelComponent = GetComponent<SelectLevel>();

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
        UpdateButtonUplock();
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

    private void UpdateButtonUplock()
    {
        if (enterButton == null) return;
        if (StageUnlock[currentIndex - 1].isUnlock)
        {
            TextMeshProUGUI buttonText = enterButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = "EMBARK!!";
            }
            costText.SetActive(false);
        }
        else
        {
            TextMeshProUGUI buttonText = enterButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = "BUY";
            }
            costText.SetActive(true);
            TextMeshProUGUI costTextComponent = costText.GetComponentInChildren<TextMeshProUGUI>();
            costTextComponent.text = StageUnlock[currentIndex - 1].Cost.ToString();
        }

    }

    public void EnterState()
    {
        if (gameStateManager == null) return;
        if (StageUnlock[currentIndex - 1].isUnlock == false) 
        {
            BuyLevel();
            return;
        }
        gameStateManager.ChangeState(GameState.GamePlay);
        selectLevelComponent.Execute();
    }

    public void BuyLevel()
    {
        if (StageUnlock[currentIndex - 1].isUnlock) return;
        int cost = StageUnlock[currentIndex - 1].Cost;
        if (!MetaCurrency.Instance.CanAfford(cost)) return;
        MetaCurrency.Instance.SpendMetaCurrency(cost);
        
        // Fix: Create a copy, modify, then assign back to the list
        ButtonBuyData data = StageUnlock[currentIndex - 1];
        data.isUnlock = true;
        StageUnlock[currentIndex - 1] = data;
        
        UpdateButtonUplock();
    }

    public void ChangeState()
    {
        canvasGroup.alpha = toggle.isOn ? 1f : 0f;
        canvasGroup.interactable = toggle.isOn;
        canvasGroup.blocksRaycasts = toggle.isOn;
    }
}