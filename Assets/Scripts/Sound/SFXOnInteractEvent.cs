using UnityEngine;

public class SFXOnInteractEvent : MonoBehaviour
{
    [SerializeField] private SoundID soundID;
    private IInteractive interactiveObject;
    void Awake()
    {
        interactiveObject = GetComponent<IInteractive>();
    }
    void OnEnable()
    {
        interactiveObject.OnInteract += PlaySFX;
    }
    void OnDisable()
    {
        interactiveObject.OnInteract -= PlaySFX;
    }
    void PlaySFX()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
