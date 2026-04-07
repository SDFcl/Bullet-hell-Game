using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowStageChange : MonoBehaviour
{
    private TextMeshProUGUI stageText;
    private void Start()
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
        stageText.text = $"Map {GameSession.currentLevel.map} {GameSession.currentLevel.stage}";
    }
}
