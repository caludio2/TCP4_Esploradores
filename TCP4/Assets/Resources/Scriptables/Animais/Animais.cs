using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animais/ListaDeAnimais")]
public class Animais : ScriptableObject
{
    [System.Serializable]
    public class AnimalInfo
    {
        [Header("Dados do Animal")]
        public GameObject prefab;
        [Range(0f, 100f)] public float chance = 10f;
        public float lifeTime = 10f;

        [Header("Sons")]
        public AudioClip somDeSpawn;
        public AudioClip somDeDestruicao;
    }

    [Header("Configura��es Gerais")]
    public float viewRange = 20f;
    public float frequency = 5f;

    [Header("Lista de Animais Dispon�veis")]
    public List<AnimalInfo> listaDeAnimais;
}