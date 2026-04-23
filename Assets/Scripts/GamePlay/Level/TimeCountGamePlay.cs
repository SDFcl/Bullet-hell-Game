using UnityEngine;

public class TimeCountGamePlay : MonoBehaviour
{
    float timer;
    void Start()
    {
        if(GameSession.currentLevel.stage == Stage.Stage2 || GameSession.currentLevel.stage == Stage.BossStage)
        {
            timer += GameSession.timeCount;
        }
        else
        {
            GameSession.timeCount = 0;
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnDisable()
    {
        GameSession.timeCount = timer;
    }
}
