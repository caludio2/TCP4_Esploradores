using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PaginaData", menuName = "Scriptable Objects/PaginaData")]
public class PaginaData : ScriptableObject
{
    public string Title;

    public int StarNumber;

    public UnityEngine.UIElements.Image minigameThumbnail;

    public int Points;

    public string Description;
}
