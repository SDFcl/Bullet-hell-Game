using System.Collections.Generic;
using UnityEngine;

public class RoomRewardController : MonoBehaviour
{
    //ที่เป็น list เพราะอาจจะมีหลายรางวัลในห้องเดียวกัน
    [Tooltip("List of reward prefabs in the room. Use a list because there may be multiple rewards in the same room.")]
    [SerializeField] private List<GameObject> rewardPrefab = new List<GameObject>();

    private void Start()
    {
        SetRewardActive(false);
    }

    public void SetRewardActive(bool state)
    {
        foreach (var reward in rewardPrefab)
        {
            if (reward == null)
            {
                reward.SetActive(state);
                return;
            }
        }

    }
}
