using UnityEngine;
public class EnemyPressKeyCondition : ICondition<EnemyContext>
{
    private readonly KeyCode _key;

    public EnemyPressKeyCondition(KeyCode key)
    {
        _key = key;
    }

    public bool IsMet(EnemyContext ctx)
    {
        return Input.GetKeyDown(_key);
    }
}