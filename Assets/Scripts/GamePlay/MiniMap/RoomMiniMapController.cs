using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum RoomMiniMapState
{
    Unexplored,
    Explored,
    Discover
}

public class RoomMiniMapController : MonoBehaviour
{
    [Header("References")]
    public GameObject MinimapIconGroup;

    [Header("Components")]
    public SpriteRenderer SpriteRoomRenderer;
    public SpriteRenderer SpriteIconRenderer;

    [Header("Data")]
    public Sprite DiscoverIcon;
    public Color DiscoverColor;
    public Sprite ExploredIcon;
    public Color ExploredColor;

    [Space]
    public RoomMiniMapState state = RoomMiniMapState.Unexplored;

    public List<RoomConnector> roomConnectors = new List<RoomConnector>();

    private void Awake()
    {
        ChangeRoomState(state);
    }

    public void ChangeRoomState(RoomMiniMapState state)
    {
        switch (state)
        {
            case RoomMiniMapState.Unexplored:
                MinimapIconGroup.gameObject.SetActive(false);
                break;
            case RoomMiniMapState.Discover:
                Discover();
                break;
            case RoomMiniMapState.Explored:
                Explored();
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player entered room: " + collision.gameObject.tag);
        if (collision.CompareTag("Player"))
        {
            ChangeRoomState(RoomMiniMapState.Explored);
        }
    }

    public void Discover()
    {
        MinimapIconGroup.gameObject.SetActive(true);
        SpriteRoomRenderer.color = DiscoverColor;
        SpriteIconRenderer.sprite = DiscoverIcon;
    }

    public void Explored()
    {
        MinimapIconGroup.gameObject.SetActive(true);
        SpriteRoomRenderer.color = ExploredColor;
        SpriteIconRenderer.sprite = ExploredIcon;
        state = RoomMiniMapState.Explored;

        roomConnectorsExecute();

    }

    public void AddroomConnectors(RoomConnector connector)
    {
        roomConnectors.Add(connector);
    }

    public void roomConnectorsExecute()
    {
        foreach (var connector in roomConnectors)
        {
            connector.gameObject.SetActive(true);
            connector.Execute();
        }
    }
}
