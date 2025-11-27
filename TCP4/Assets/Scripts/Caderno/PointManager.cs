using UnityEditor.Overlays;
using UnityEngine;
using System.IO;
using Mono.Cecil;

public class PointManager : MonoBehaviour
{
    public int pescariaPoints;
    public int trilhaPoints;
    public int fotoPoints;

    public PaginaData[] paginaDatas;

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

    public void Update()
    {
        pescariaPoints = paginaDatas[0].Points;
        trilhaPoints = paginaDatas[1].Points;
        fotoPoints = paginaDatas[2].Points;
    }

    public void SetPoints(string minigameName ,int points)
    {
        switch (minigameName)
        {
            case "pescariaPoints":
                paginaDatas[0].Points = points;
                if (pescariaPoints > points)
                    LoadGame();
            break;
            case "trilhaPoints":
                paginaDatas[1].Points = points;
                if (trilhaPoints > points)
                    LoadGame();
                break;
            case "fotoPoints":
                paginaDatas[2].Points = points;
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
