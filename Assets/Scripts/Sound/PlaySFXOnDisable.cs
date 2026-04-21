using UnityEngine;

public class PlaySFXOnDisable : MonoBehaviour
{
    [SerializeField] private SoundID soundID;
    void OnDisable()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
