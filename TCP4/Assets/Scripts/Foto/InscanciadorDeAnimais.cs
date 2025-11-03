using System.Collections;
using UnityEngine;

public class InscanciadorDeAnimais : MonoBehaviour
{
    [SerializeField] private Animais animalStats;
    [SerializeField] private Transform playerTransform;

    private AudioSource audioSource;
    private GameObject prefabInstance;

    private bool canBeSpawned = true;
    public bool isSpawned = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Sai se o player estiver fora do alcance
        if (Vector3.Distance(transform.position, playerTransform.position) > animalStats.viewRange)
            return;

        // Sai se estiver em cooldown ou já tiver um animal ativo
        if (!canBeSpawned || isSpawned)
            return;

        canBeSpawned = false;
        StartCoroutine(WaitTime());

        Animais.AnimalInfo escolhido = EscolherAnimalPorProbabilidade();
        if (escolhido != null)
            BeSpawned(escolhido);
    }

    void BeSpawned(Animais.AnimalInfo animal)
    {
        prefabInstance = Instantiate(
            animal.prefab,
            transform.position,
            Quaternion.LookRotation(playerTransform.position - transform.position)
        );
        prefabInstance.transform.SetParent(this.transform);

        isSpawned = true;
        StartCoroutine(WaitAndDestroy(animal));

        // Toca som de spawn
        if (animal.somDeSpawn != null)
            audioSource.PlayOneShot(animal.somDeSpawn);
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(animalStats.frequency);
        canBeSpawned = true;
    }

    IEnumerator WaitAndDestroy(Animais.AnimalInfo animal)
    {
        yield return new WaitForSeconds(animal.lifeTime);

        isSpawned = false;

        // Toca som de destruição
        if (animal.somDeDestruicao != null)
            audioSource.PlayOneShot(animal.somDeDestruicao);

        if (prefabInstance != null)
            Destroy(prefabInstance);
    }

    Animais.AnimalInfo EscolherAnimalPorProbabilidade()
    {
        var lista = animalStats.listaDeAnimais;
        if (lista == null || lista.Count == 0)
            return null;

        float total = 0f;
        foreach (var a in lista)
            total += a.chance;

        float sorteio = Random.Range(0, total);
        float acumulado = 0f;

        foreach (var a in lista)
        {
            acumulado += a.chance;
            if (sorteio <= acumulado)
                return a;
        }

        return lista[lista.Count - 1];
    }
}
