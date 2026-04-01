using UnityEngine;
public class PressKeyCondition : ICondition<EnemyContext>
{
    private readonly KeyCode _key;

    public PressKeyCondition(KeyCode key)
    {
        _key = key;
    }

    public bool IsMet(EnemyContext ctx)
    {
        return Input.GetKeyDown(_key);
    }
}