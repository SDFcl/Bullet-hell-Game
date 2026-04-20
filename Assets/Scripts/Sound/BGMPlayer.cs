using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private SoundID musicPlayWhenStart;
    void Start()
    {
        SoundManager.Instance.PlayBGM(musicPlayWhenStart);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.StopBGM();
        }
    }
}
