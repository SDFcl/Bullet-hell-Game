using UnityEngine;
using UnityEngine.UI;

public class MapSelectorUI : MonoBehaviour
{
    [Header("Map Layouts")]
    [SerializeField] private GameObject[] mapLayouts;   // ลาก PF_Map1Layout_A/B/C มาใส่
    [SerializeField] private string[] mapNames;         // ชื่อแสดงบน button


    private AdapterPatternClientTest gameController;

    private void Awake()
    {
        gameController = GetComponent<AdapterPatternClientTest>();
    }

    public void SelectMap(int index)
    {
        for (int i = 0; i < mapLayouts.Length; i++)
            mapLayouts[i].SetActive(i == index);

        gameController.GameLoad();
        gameController.GameStart();
    }

    
    private void OnGUI()
    {
        float btnW = 200, btnH = 60, spacing = 20;
        float totalH = mapLayouts.Length * (btnH + spacing) - spacing;
        float startX = 20f;
        float startY = 20f;

        GUI.Box(new Rect(startX - 20, startY - 40, btnW + 40, totalH + 80), "Select Map");

        for (int i = 0; i < mapLayouts.Length; i++)
        {
            string label = (mapNames != null && i < mapNames.Length && mapNames[i] != "")
                ? mapNames[i] : $"Map {i + 1}";
            if (GUI.Button(new Rect(startX, startY + i * (btnH + spacing), btnW, btnH), label))
                SelectMap(i);
        }
    }
}
