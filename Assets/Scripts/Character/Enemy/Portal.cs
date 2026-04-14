using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour,IPooledObject
{
    [SerializeField] float lifeTime;
    [SerializeField] AnimationClip portalAnim;
    public void OnObjectSpawn()
    {
        if(portalAnim != null)
        {
            lifeTime = portalAnim.length;
        }
        StartCoroutine(Disable());
    }
    private IEnumerator Disable()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}
}
