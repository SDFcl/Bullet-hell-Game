using UnityEngine;

public class SFXOnBuyMap : MonoBehaviour
{
    [SerializeField] private SoundID soundID;
    private LevelSelectController levelscontro;
    void Awake()
    {
        levelscontro = GetComponent<LevelSelectController>();
    }
    void OnEnable()
    {
        levelscontro.OnBuySuccess += PlaySFX;
    }
    void OnDisable()
    {
        levelscontro.OnBuySuccess-= PlaySFX;
    }
    void PlaySFX()
    {
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
}
