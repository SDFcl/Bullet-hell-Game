using UnityEngine;

public class EndPointInterectable : InteractiveObject
{
    public override string GetInteractionName()
    {
        return "End Point";
    }
    protected override void ExecuteInteraction()
    {
        GetComponent<ILevel>()?.Execute();
    }
}
