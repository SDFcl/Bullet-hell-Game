using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class StopGame : MonoBehaviour
{
    private PlayerInput playerInput;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) return;
        playerInput = player.GetComponent<PlayerInput>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Start()
    {
        EnableMyself(false);
    }
    public void EnableMyself(bool Enble)
    {
        if (Enble)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            Time.timeScale = 0;
            playerInput.enabled = false;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            Time.timeScale = 1;
            playerInput.enabled = true;
        }
    }
    private void OnEnable()
    {
        EventBus.Subscribe<GameStateChangedEvent>(OnGameStateChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameStateChangedEvent>(OnGameStateChanged);
    }
    private void OnGameStateChanged(GameStateChangedEvent e)
    {
        if (e.NewState == GameState.Paused)
        {
            EnableMyself(true);
        }
        else if (e.NewState == GameState.GamePlay)
        {
            EnableMyself(false);
        }
    }
    public void Resume()
    {
        GameStateManager.Instance.ChangeState(GameState.GamePlay);
    }
}
