using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StagePHUD : MonoBehaviour
{
    [SerializeField] private string baseStage = "Stage";
    private TextMeshProUGUI stageText;
    private void Awake()
    {
        stageText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (stageText == null) return;
        string text = "Stage";
        switch (GameSession.currentLevel.stage)
        {
            case Stage.Stage1:
                text = $"{baseStage} 1";
                break;

            case Stage.Stage2:
                text = $"{baseStage} 2";
                break;

            case Stage.BossStage:
                text = $"Boss {baseStage}";
                break;
                
        }
        
        stageText.text = text;
    }
}
