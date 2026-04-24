using UnityEngine;

public class SetMuneState : MonoBehaviour
{
    void Awake()
    {
        GameStateManager.Instance.ChangeState(GameState.MainMenu);
    }
}
