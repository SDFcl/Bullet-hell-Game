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
            GameSession.CurrentReward = reward;
            DataPersistenceManager dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
            if (dataPersistenceManager != null)
            {
                dataPersistenceManager.SaveGame();
                Debug.Log($"Game saved after awarding {reward} MetaCurrency.");
            }
        }

        // Execute level transition
        GetComponent<ILevel>()?.Execute();
    }
}
