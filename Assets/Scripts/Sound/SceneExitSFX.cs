using System.Collections;
using UnityEngine;

public class SceneExitSFX : MonoBehaviour, ISceneExitTask
{
    [SerializeField] private SoundID soundID;

    public IEnumerator Execute()
    {
        float duration = SoundManager.Instance.PlaySFX(soundID, transform.position);
        yield return new WaitForSeconds(duration);
    }
}
