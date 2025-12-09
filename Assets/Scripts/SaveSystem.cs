using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/save.json";

    public static void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, jsonData);
        Debug.Log("Juego guardado en: " + savePath);
    }

    public static GameData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log("Datos encotrados | " + savePath);
            return data;
        }
        else
        {
            Debug.Log("No hay datos guardados");
            GameData data = new GameData();
            return data;
        }
    }
}
public static class AutoSaveSystem
{
    private static string savePath = Application.persistentDataPath + "/autosave.json";

    public static void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, jsonData);
        Debug.Log("autoguardado en: " + savePath);
    }

    public static GameData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log("Autoguardado encotrado | " + savePath);
            return data;
        }
        else
        {
            Debug.Log("No hay datos autoguardados");
            GameData data = new GameData();
            return data;
        }
    }
}