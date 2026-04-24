using UnityEditor.SearchService;
using UnityEngine;

public class Abandon : MonoBehaviour
{
    public void AbandonRun()
    {
        Time.timeScale = 1;
        GameSession.savedHealth = -1;
        GameStateManager.Instance.ChangeState(GameState.GameOver);
    }
}
