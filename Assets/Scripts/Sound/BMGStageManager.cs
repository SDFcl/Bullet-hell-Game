using UnityEngine;

public class BMGStageManager : MonoBehaviour
{
    LevelID currentLevel;
    [SerializeField] SoundID[] mapSoundID;
    void Awake()
    {
        currentLevel = GameSession.currentLevel;
    }
    void Start()
    {
        switch (currentLevel.map)
        {
            case 1:
            SoundManager.Instance.PlayBGM(mapSoundID[0]);
            break;

            case 2:
            SoundManager.Instance.PlayBGM(mapSoundID[1]);
            break;

            case 3:
            SoundManager.Instance.PlayBGM(mapSoundID[2]);
            break;
        }
    }
}
