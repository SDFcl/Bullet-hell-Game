using UnityEngine;

public abstract class ClassRandom : ScriptableObject 
{
    public virtual GameObject GetRandomItem()
    {
        return null;
    }

    public virtual int GetRandomInt(RareType rareType)
    {
        return 0;
    }
}
