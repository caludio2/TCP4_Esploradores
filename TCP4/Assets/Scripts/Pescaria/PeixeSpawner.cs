using Unity.VisualScripting;
using UnityEngine;

public class PeixeSpawner : MonoBehaviour
{
    public Pescaria playerRef;

    [SerializeField]
    GameObject[] Peixes;

    [Range(0f, 100f)]
    public int autura;

    [Range(0f, 100f)]
    public float maxRange;

    [Range(0f, 100f)]
    public float minRange;

    private enum EstadoPeixe { PeixeSpawndado , PeixeDesPawndo }
    private EstadoPeixe estadoAtual = EstadoPeixe.PeixeDesPawndo;
    void Update()
    {
        switch (estadoAtual)
        {
            case EstadoPeixe.PeixeDesPawndo: // peixe ainda nao spawndo
                if (playerRef.GetComponent<Pescaria>().curremtIsca != null)//isca foi lançada
                {
                    Vector3 iscaPos = playerRef.GetComponent<Pescaria>().curremtIsca.GetComponent<isca>().inpactPos;
                    print(playerRef.name);
                    if (playerRef.GetComponent<Pescaria>().curremtIsca.GetComponent<isca>().fizgou == false && iscaPos != new Vector3(0,0,0))//isca foi fizgada
                    {
                        print("fizgou = false");
                        float angle = Random.Range(0f, 2f * Mathf.PI);
                        // Random radius within given range
                        float radius = Random.Range(maxRange, minRange);

                        // Calculate x and z positions based on angle and radius
                        float x = Mathf.Cos(angle) * radius;
                        float z = Mathf.Sin(angle) * radius;

                        // Determine spawn position relative to the center (this object)
                        Vector3 spawnPosition = new Vector3(transform.position.x + x, autura, transform.position.z + z);

                        
                        print("spawnou");
                        GameObject peixeInstance = Instantiate(Peixes[Random.Range(0, Peixes.Length)], iscaPos + spawnPosition, Quaternion.LookRotation(iscaPos - spawnPosition));
                        peixeInstance.GetComponent<IPeixe>().iscaPos = iscaPos;
                        estadoAtual = EstadoPeixe.PeixeSpawndado;
                    }
                }
                break;
            case EstadoPeixe.PeixeSpawndado:
                break;

        }
    }
}
