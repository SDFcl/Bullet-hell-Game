using UnityEngine;
using System;
public class SFXOnCollectEvent : MonoBehaviour
{
    [SerializeField] private SoundID soundID;
    private ICollectEvent collectEvent;
    private Action<GameObject> playSfxHandler;
    void Awake()
    {
        collectEvent = GetComponent<ICollectEvent>();
        playSfxHandler = _ => PlaySFX();
    }
    void OnEnable()
    {
        collectEvent.OnCollected += playSfxHandler;
    }
    void OnDisable()
    {
        collectEvent.OnCollected -= playSfxHandler;
    }
    void PlaySFX()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
