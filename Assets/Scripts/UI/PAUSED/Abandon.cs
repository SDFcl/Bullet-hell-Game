using UnityEngine;

public class Abandon : MonoBehaviour
{
    public void AbandonRun()
    {
        Time.timeScale = 1;
        GameSession.savedHealth = 0;
        GameStateManager.Instance.ChangeState(GameState.GameOver);
    }
}
