using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }
}

public class SaveData
{
    public int fotoPoints = PointManager.Instance.fotoPoints;
    public int pescariaPoints = PointManager.Instance.pescariaPoints;
    public int trilhaPoints = PointManager.Instance.trilhaPoints;
}
