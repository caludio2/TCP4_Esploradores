using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InscanciadorDeAnimais : MonoBehaviour
{
    [SerializeField]
    GameObject Animal;

    [SerializeField]
    Animais AnimalStats;

    [SerializeField]
    Transform playerTransform;

    GameObject prefabInstance;

    bool canBeSpawned = true; //CoolDown bool

    bool isSpawned = false;
    void Update()
    {
        if(Vector3.Distance(transform.position ,playerTransform.position) > AnimalStats.viewRange) //Ta no range?
        { return; }

        print("no range");

        if (!canBeSpawned) //Ta no colldown?
        {  return; }

        if(isSpawned) //Ta spawnado?
        { return; }

        print("passou aqui");

        canBeSpawned = false; //colldown
        StartCoroutine(WaitTime());

        int rnd = Random.Range(0, 3);//Chance nao configuravel
        BeSpawned(rnd);
    }
    public void BeSpawned(int number)
    {
        if (number != 0) 
        { return; }

        prefabInstance = Instantiate(Animal, transform.position, Quaternion.LookRotation(playerTransform.position));
        StartCoroutine(WaitAndDestroy());

        print("Instanciado");
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(AnimalStats.frequency);
        canBeSpawned = true;
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(AnimalStats.lifeTime);
        Destroy(prefabInstance);
    }


}
