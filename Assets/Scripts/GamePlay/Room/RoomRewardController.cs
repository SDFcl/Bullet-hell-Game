using System.Collections.Generic;
using UnityEngine;

public class RoomRewardController : MonoBehaviour
{
    //ที่เป็น list เพราะอาจจะมีหลายรางวัลในห้องเดียวกัน
    [Tooltip("List of reward prefabs in the room. Use a list because there may be multiple rewards in the same room.")]
    [SerializeField] private List<GameObject> rewardPrefab = new List<GameObject>();
    [SerializeField] private List<int> SpecialReward = new List<int>();

    private void Start()
    {
        SetRewardActive(false);
    }

    public void SetRewardActive(bool state)
    {
        if (state == false)
        {
            foreach (GameObject reward in rewardPrefab)
            {
                reward.SetActive(state);
            }
            return;
        }

        int index = RandomSpecialReward();
        Debug.Log("Selected Reward Index: " + index);
        if (index == 0)
        {
            rewardPrefab[0].SetActive(state);
        }
        else
        {
            rewardPrefab[1].SetActive(state);
        }

        
    }

    public int RandomSpecialReward()
    {
        if (rewardPrefab.Count == 0 || SpecialReward.Count == 0) return 0;
        if (rewardPrefab.Count != SpecialReward.Count) return 0;

        int totalRate = 0;
        foreach (int rate in SpecialReward)
        {
            totalRate += rate;
        }

        int randomValue = Random.Range(0, totalRate);

        int selectedIndex = 0;
        int cumulative = 0;

        for (int i = 0; i < SpecialReward.Count; i++)
        {
            cumulative += SpecialReward[i];
            if (randomValue < cumulative)
            {
                selectedIndex = i;
                break;
            }
        }
        return selectedIndex;
    }
}
