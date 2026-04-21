using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public static GameState CurrentState { get; private set; } = GameState.MainMenu;
    private static GameState previousState;

    private void Start()
    {
        // เริ่มเกมที่ MainMenu หรือ Playing ก็ได้
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        if (newState == CurrentState) return;

        ExitState(CurrentState);
        previousState = CurrentState;
        CurrentState = newState;
        EnterState(CurrentState);

        Debug.Log($"Game State changed: {previousState} → {newState}");

        // ส่ง Event ผ่าน Event Bus
        EventBus.Raise(new GameStateChangedEvent(previousState, newState));
    }

    public void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                // แสดงเมนูหลัก, รีเซ็ตเกม ฯลฯ
                break;
            case GameState.GamePlay:
                // เริ่มเกม, เปิด UI, เริ่มนับเวลา ฯลฯ
                break;
            case GameState.Combat:
                // เริ่มการต่อสู้, เปิด UI ต่อสู้, หยุดเวลา ฯลฯ
                break;
            case GameState.Paused:
                // หยุดเวลา, แสดงเมนูหยุดเกม ฯลฯ
                break;
            case GameState.GameOver:
                // แสดงหน้าจอเกมจบ, หยุดการเคลื่อนไหว ฯลฯ
                break;
            case GameState.Victory:
                // แสดงหน้าจอชนะ, หยุดการเคลื่อนไหว ฯลฯ
                break;
            case GameState.Loading:
                // แสดงหน้าจอโหลด, เตรียมข้อมูล ฯลฯ
                break;
            case GameState.Quit:
                // ปิดเกม, บันทึกข้อมูล ฯลฯ
                Application.Quit();
                break;
        }
    }

    public void ExitState(GameState newState)
    {
        // ทำความสะอาดหรือรีเซ็ตข้อมูลก่อนเปลี่ยนไปยังสถานะใหม่
    }

    public void ChangeGamePlayState()
    {
        ChangeState(GameState.GamePlay);
    }
    public void ChangeQuitGameState()
    {
        ChangeState(GameState.Quit);
    }
}