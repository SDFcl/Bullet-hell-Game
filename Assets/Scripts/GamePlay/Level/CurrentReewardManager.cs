using UnityEngine;

public class CurrentReewardManager : MonoBehaviour
{
    void Start()
    {
        if(GameSession.currentLevel.stage == Stage.Stage1)
        {
            GameSession.CurrentReward = 0;
        }
    }
}
