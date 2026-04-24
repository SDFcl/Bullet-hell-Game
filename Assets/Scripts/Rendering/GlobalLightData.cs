using UnityEngine;

[CreateAssetMenu(menuName = "GlobalLightData")]
public class GlobalLightData : ScriptableObject
{
    public float globalLightMap1;
    public float globalLightMap2;
    public float globalLightMap3;

    public void UpdateLightValue(out float globalLightvalue)
    {
        switch (GameSession.currentLevel.map)
        {
            case 1:
                globalLightvalue = globalLightMap1;
                break;

            case 2:
                globalLightvalue = globalLightMap2;
                break;

            case 3:
                globalLightvalue = globalLightMap3;
                break;

            default:
                globalLightvalue = globalLightMap1;
                break;
        }
    }
}
