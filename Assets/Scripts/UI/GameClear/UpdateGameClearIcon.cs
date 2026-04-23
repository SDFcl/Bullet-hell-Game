using UnityEngine;

public class UpdateGameClearIcon : MonoBehaviour
{
    public GameObject gameclearIcon;
    public GameObject gameOverIcon;
    void Start()
    {
       if(GameSession.savedHealth <= 0)
        {
            gameOverIcon.SetActive(true);
        }
        else
        {
            gameclearIcon.SetActive(true);
        }
    }
}
