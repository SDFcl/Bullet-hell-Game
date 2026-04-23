using System.Diagnostics;
using UnityEngine;

public class BMGBossTriggerRoom : MonoBehaviour
{
    [SerializeField] SoundID[] bossSoundIDs;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) return;
        if(GameSession.currentLevel.stage != Stage.BossStage) return;
        switch (GameSession.currentLevel.map)
        {
            case 1:
            SoundManager.Instance.PlayBGM(bossSoundIDs[0]);
            break;
            case 2:
            SoundManager.Instance.PlayBGM(bossSoundIDs[1]);
            break;
            case 3:
            SoundManager.Instance.PlayBGM(bossSoundIDs[2]);
            break;
        }
        
    }
}
