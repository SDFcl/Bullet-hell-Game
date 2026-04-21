using UnityEngine;
using System.Collections;
public class SceneFadeExitTask : MonoBehaviour, ISceneExitTask
{
    [SerializeField] private AnimationClip duration;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator Execute()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(duration.length);
    }
}
