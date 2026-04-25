using UnityEngine;

public class PlayerCHManager : MonoBehaviour
{
    PlayerHealth playerHealth;
    void Awake()
    {
        if(playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        if(GameSession.currentLevel.stage == Stage.Stage2 || GameSession.currentLevel.stage == Stage.BossStage)
        {
            playerHealth.SetCurrentHealth((float)GameSession.savedHealth);
        }
        playerHealth.OnDead += PlayerDeadHeadle;
    }
    void OnDisable()
    {
        if (GameStateManager.CurrentState == GameState.GameOver)
        {
            playerHealth.OnDead -= PlayerDeadHeadle;
            return;
        }

        GameSession.savedHealth = (int)playerHealth.CurrentHP;
        playerHealth.OnDead -= PlayerDeadHeadle;
    }

    void PlayerDeadHeadle()
    {
        GameSession.savedHealth = 0;
    }
}
