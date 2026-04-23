using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class StopGame : MonoBehaviour
{
    private PlayerInput playerInput;
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) return;
        playerInput = player.GetComponent<PlayerInput>();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        Time.timeScale = 0;
        playerInput.enabled = false;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        playerInput.enabled = true;
    }
}
