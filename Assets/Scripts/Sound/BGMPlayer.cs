using UnityEngine;

public class BGMPlayerWhenStart : MonoBehaviour
{
    [SerializeField] private SoundID musicPlayWhenStart;
    void Start()
    {
        SoundManager.Instance.PlayBGM(musicPlayWhenStart);
    }
}
