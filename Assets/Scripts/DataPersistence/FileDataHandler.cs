using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath;     // โฟลเดอร์ย่อย (เช่น "Saves")
    private string dataFileName;    // ชื่อไฟล์ (เช่น "savegame.json")

    // เปลี่ยนชื่อเกมตรงนี้เป็นชื่อที่คุณต้องการ (แนะนำใส่ตัวเลขหรือรหัสสุ่มด้วย)
    private const string WEBGL_SAVE_FOLDER = "idbfs/MyAwesomeGame_2026";   // ← แก้ตรงนี้

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    /// <summary>
    /// สร้าง full path โดยแยก WebGL กับแพลตฟอร์มอื่น
    /// </summary>
    private string GetFullPath()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL ใช้ path คงที่เพื่อให้ข้อมูลอยู่ได้แม้อัปเดตเกม
        string basePath = WEBGL_SAVE_FOLDER;

        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
            Debug.Log("Created WebGL save directory: " + basePath);
        }

        // ถ้ามี dataDirPath (เช่น "Saves") ให้สร้างโฟลเดอร์ย่อยเพิ่ม
        if (!string.IsNullOrEmpty(dataDirPath))
        {
            string subFolder = Path.Combine(basePath, dataDirPath);
            if (!Directory.Exists(subFolder))
                Directory.CreateDirectory(subFolder);

            return Path.Combine(subFolder, dataFileName);
        }

        return Path.Combine(basePath, dataFileName);
#else
        // แพลตฟอร์มอื่นใช้ persistentDataPath ปกติ
        string basePath = Application.persistentDataPath;

        if (!string.IsNullOrEmpty(dataDirPath))
            basePath = Path.Combine(basePath, dataDirPath);

        return Path.Combine(basePath, dataFileName);
#endif
    }

    public GameData Load(string profileId)
    {
        if (profileId == null)
            return null;

        string fullPath = GetFullPath();
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = File.ReadAllText(fullPath);
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

                Debug.Log("✅ Loaded data from: " + fullPath);
            }
            catch (Exception e)
            {
                Debug.LogError("❌ Failed to load data: " + e.Message);
            }
        }
        else
        {
            Debug.Log("No save file found at: " + fullPath);
        }

        return loadedData;
    }

    public void Save(string profileId, GameData data)
    {
        if (data == null) return;

        string fullPath = GetFullPath();

        try
        {
            // สร้างโฟลเดอร์ (ในกรณี non-WebGL)
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true); // pretty print

            File.WriteAllText(fullPath, dataToStore);

            Debug.Log("💾 Saved data to: " + fullPath);

            // Sync ลง IndexedDB สำหรับ WebGL
#if UNITY_WEBGL && !UNITY_EDITOR
            SyncFS();
#endif
        }
        catch (Exception e)
        {
            Debug.LogError("❌ Failed to save data: " + e.Message);
        }
    }

    // เรียก SyncFS จาก JavaScript
#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void SyncFS();
#endif
}