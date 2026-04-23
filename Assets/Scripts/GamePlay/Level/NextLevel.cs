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

        // Health playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponentInChildren<Health>();
        // if (playerHealth != null)
        // {
        //     playerHealth.SaveHealth();
        //     Debug.Log("Player health saved: " + playerHealth.gameObject.name);
        // }

        LevelID currentLevel = GameSession.currentLevel;

        Stage? nextStage = currentLevel.GetNextStage(currentLevel.stage);

        if (nextStage == null)
        {
            SceneLoader.Instance.LoadScene("Lobby",gameObject);
            return;
        }

        currentLevel.stage = nextStage.Value;
        GameSession.currentLevel = currentLevel;

        SceneLoader.Instance.ReloadCurrentScene(gameObject);
    }
}
