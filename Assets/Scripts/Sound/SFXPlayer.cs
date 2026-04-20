using System.Collections;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private Coroutine returnRoutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(SoundData data)
    {
        if (data == null || data.clip == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (returnRoutine != null)
            StopCoroutine(returnRoutine);

        audioSource.clip = data.clip;
        audioSource.volume = data.volume;
        audioSource.Play();
        
        returnRoutine = StartCoroutine(ReturnAfterFinished(data.clip.length));
    }

    private IEnumerator ReturnAfterFinished(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
