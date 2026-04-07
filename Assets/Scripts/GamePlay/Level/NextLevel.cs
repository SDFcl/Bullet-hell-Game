using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, ILevel
{
    public void Execute()
    {
        LevelID currentLevel = GameSession.currentLevel;

        Stage? nextStage = currentLevel.GetNextStage(currentLevel.stage);

        if (nextStage == null)
        {
            SceneManager.LoadScene("Lobby");
            return;
        }

        currentLevel.stage = nextStage.Value;
        GameSession.currentLevel = currentLevel;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
