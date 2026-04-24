using UnityEngine;

public class SceneEnterSFX : MonoBehaviour
{
    [SerializeField] SoundID gameover;
    void Start()
    {
        SoundManager.Instance.PlaySFX(gameover,transform.position);
    }
}
