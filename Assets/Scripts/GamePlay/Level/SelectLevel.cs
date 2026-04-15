using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour, ILevel
{
    [SerializeField] private LevelID levelToEnter;
    [SerializeField] private string sceneName;

    public void Execute()
    {
        GameSession.currentLevel = levelToEnter;
        GameSession.lastRandomIndex = -1;
        GameSession.savedInventory = new SavedInventory();
        SceneManager.LoadScene(sceneName);
    }
}