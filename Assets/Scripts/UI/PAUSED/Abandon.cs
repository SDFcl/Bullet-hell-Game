using UnityEditor.SearchService;
using UnityEngine;

public class Abandon : MonoBehaviour
{
    public void AbandonRun()
    {
        GameSession.isGamePlaying = false;
        GameSession.savedHealth = -1;

        SceneLoader.Instance.LoadScene("GameClear");
    }
}
