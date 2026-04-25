using UnityEngine;
using System.Collections;

public class MoveProjectileHit : MonoBehaviour
{
    [SerializeField]ProjectileHit projectileHit;
    Vector2 _position;
    void Start()
    {
        _position = transform.position;
         StartCoroutine(seat());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator seat()
    {
        while (true)
        {
           projectileHit.transform.position = _position;
            projectileHit.gameObject.SetActive(true);
            projectileHit.SetProjectlieSpeed(2f);
            projectileHit.OnObjectSpawn();
        yield return new WaitForSeconds(3f); 
        }
        
    }

}
