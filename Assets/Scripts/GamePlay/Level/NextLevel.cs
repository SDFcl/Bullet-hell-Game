using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, ILevel
{
    public void Execute()
    {
        // Save player inventory before changing scene
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player")?.GetComponentInChildren<Inventory>();
        if (playerInventory != null)
        {
            playerInventory.SaveInventory();
        }

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
