using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ObjectPooler/ObjectPoolTable")]
public class ObjectPoolTableSO : ScriptableObject
{
    public List<Pool> pools;
}
