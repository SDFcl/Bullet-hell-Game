using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ButtonBuyData
{
    public bool isUnlock;
    public int Cost;
}

public class LevelSelectController : MonoBehaviour, IDataPersistence
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
    public CanvasGroup canvasGroup;

    private SelectLevel selectLevel;

    public int currentIndex;

    [Header("Enter Button")]
    public Button enterButton;
    public Image GC_Icon;
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

    public void LoadData(GameData data)
    {
        foreach (var item in data.UnLockLevel)
        {
            int index = item.Key;
            bool isUnlocked = item.Value;

            // �ѹ index ��ش
            if (index < 0 || index >= StageUnlock.Count) continue;

            ButtonBuyData temp = StageUnlock[index];
            temp.isUnlock = isUnlocked;
            StageUnlock[index] = temp;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.UnLockLevel.Clear();

        for (int i = 0; i < StageUnlock.Count; i++)
        {
            data.UnLockLevel[i] = StageUnlock[i].isUnlock;
        }
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
                buttonText.text = "EMBARK!";
            }
            costText.SetActive(false);
            GC_Icon.color = new Color(1,1,1,0);
        }
        else
        {
            TextMeshProUGUI buttonText = enterButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = "UNLOCK";
            }
            costText.SetActive(true);
            GC_Icon.color = new Color(1,1,1,1);
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