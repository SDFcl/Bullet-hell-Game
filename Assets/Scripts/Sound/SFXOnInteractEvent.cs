using UnityEngine;

public class SFXOnInteractEvent : MonoBehaviour
{
    [SerializeField] private SoundID soundID;
    private InteractiveObject interactiveObject;
    void Awake()
    {
        interactiveObject = GetComponent<InteractiveObject>();
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
