using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
	#region Singleton

	public static ObjectPooler Instance;

	private void Awake()
	{
		Instance = this;
	}

	#endregion
    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    // Use this for initialization
    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, System.Action<GameObject> beforeSpawn = null)
	{
		if (!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
			return null;
		}

		List<GameObject> pool = poolDictionary[tag];

		GameObject objectToSpawn = null;

		foreach (GameObject obj in pool)
		{
			if (!obj.activeInHierarchy)
			{
				objectToSpawn = obj;
				break;
			}
		}

		if (objectToSpawn == null)
		{
			if (objectToSpawn == null)
			{
				Pool poolConfig = pools.Find(p => p.tag == tag);

				if (poolConfig == null)
				{
					Debug.LogWarning("No pool config found for tag: " + tag);
					return null;
				}

				objectToSpawn = Instantiate(poolConfig.prefab);
				objectToSpawn.SetActive(false);
				pool.Add(objectToSpawn);
			}
		}

		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		beforeSpawn?.Invoke(objectToSpawn);

		objectToSpawn.SetActive(true);

		IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
		if (pooledObj != null)
		{
			pooledObj.OnObjectSpawn();
		}

		return objectToSpawn;
	}
}