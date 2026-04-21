using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour,IPooledObject
{
    [SerializeField] float lifeTime;
    [SerializeField] AnimationClip portalAnim;
    [SerializeField] SoundID soundID;
    public void OnObjectSpawn()
    {
        if(portalAnim != null)
        {
            lifeTime = portalAnim.length;
        }
        StartCoroutine(Disable());
        SoundManager.Instance.PlaySFX(soundID,transform.position);
    }
    private IEnumerator Disable()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}
}
