using UnityEditor.Overlays;
using UnityEngine;
using System.IO;

public class PointManager : MonoBehaviour
{
    public int pescariaPoints = 1;
    public int trilhaPoints;
    public int fotoPoints;

    public static PointManager Instance { get; private set; }

    private void Awake()
    {
        // Se não existe instância, define esta
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre cenas
        }
        else
        {
            Destroy(gameObject); // evita duas instâncias
        }
    }

    public void Start()
    {
        LoadGame();
    }

    public void SetPoints(string minigameName ,int points)
    {
        switch (minigameName)
        {
            case "pescariaPoints":
                 pescariaPoints = points;
                if (pescariaPoints > points)
                    LoadGame();
            break;
            case "trilhaPoints":
                trilhaPoints = points;
                if (trilhaPoints > points)
                    LoadGame();
                break;
            case "fotoPoints":
                fotoPoints = points;
                if (fotoPoints > points)
                    LoadGame();
                break;
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    void SaveGame()
    {
        SaveData data = new SaveData();
        data.pescariaPoints = pescariaPoints;
        data.trilhaPoints = trilhaPoints;
        data.fotoPoints = fotoPoints;

        SaveSystem.Save(data);
        Debug.Log("Game salvo!");
    }

    void LoadGame()
    {
        SaveData data = SaveSystem.Load();
        if (data != null)
        {
            pescariaPoints = data.pescariaPoints;
            trilhaPoints = data.trilhaPoints;
            fotoPoints = data.fotoPoints;
        }
    }

}
