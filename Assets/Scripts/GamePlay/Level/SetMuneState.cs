using UnityEngine;

public class SetMuneState : MonoBehaviour
{
    public GameObject titleMeun;
    public CanvasGroup MainMenu;
    public CanvasGroup StartMenu;
    public CanvasGroup Upgrade;
    public CanvasGroup Options;
    public CanvasGroup Quit;
    void Start()
    {
        GameStateManager.Instance.ChangeState(GameState.MainMenu);
        titleMeun.SetActive(true);
        SetCavasGroup(MainMenu,false);
        SetCavasGroup(StartMenu,true);
        SetCavasGroup(Upgrade,false);
        SetCavasGroup(Options,false);
        SetCavasGroup(MainMenu,false);
    }
    void SetCavasGroup(CanvasGroup menu, bool enable)
    {
        menu.alpha = enable ? 1 : 0;
        menu.interactable = enable;
        menu.blocksRaycasts = enable;
    }
}
