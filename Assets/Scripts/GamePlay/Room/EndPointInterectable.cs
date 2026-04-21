using UnityEngine;

public class EndPointInterectable : InteractiveObject
{
    public override string GetInteractionName()
    {
        return "End Point";
    }
    protected override void ExecuteInteraction(GameObject player)
    {
        // Award MetaCurrency based on current level
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            int reward = levelManager.GetMetaCurrencyReward();
            MetaCurrency.Instance.AddMetaCurrency(reward);
        }

        // Execute level transition
        GetComponent<ILevel>()?.Execute();
    }
}
