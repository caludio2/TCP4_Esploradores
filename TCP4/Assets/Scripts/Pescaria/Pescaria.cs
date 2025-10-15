using UnityEngine;
using System.Collections;

public class Pescaria : MonoBehaviour
{
    public float sensibilidade = 1.5f;
    public float cooldownEntreGestos = 0.5f;
    private float tempoDesdeUltimoGesto = 0f;

    private enum EstadoPesca { EsperandoPuxar, PreparadoParaLancar, Lancado ,Puxando}
    private EstadoPesca estadoAtual = EstadoPesca.EsperandoPuxar;

    [SerializeField]
    GameObject isca;

    [SerializeField]
    public GameObject curremtIsca;

    [SerializeField]
    float upForce;
    void Start()
    {
        
    }
    void Update()
    {
        tempoDesdeUltimoGesto += Time.deltaTime;

        float intensidade = GetIntensityOfMoviment();

        switch (estadoAtual)
        {
            case EstadoPesca.EsperandoPuxar:
                if (intensidade > sensibilidade && tempoDesdeUltimoGesto > cooldownEntreGestos)
                {
                    print("Puxou! Vara preparada.");
                    Handheld.Vibrate();
                    estadoAtual = EstadoPesca.PreparadoParaLancar;
                    tempoDesdeUltimoGesto = 0f;
                }
                break;

            case EstadoPesca.PreparadoParaLancar:
                // Espera o movimento voltar ao repouso antes de aceitar o lançamento
                if (intensidade < sensibilidade * 0.5f)
                {
                    // Pronto para detectar o lançamento
                    if (tempoDesdeUltimoGesto > cooldownEntreGestos)
                    {
                        estadoAtual = EstadoPesca.Lancado;
                        tempoDesdeUltimoGesto = 0f;
                    }
                }
                break;

            case EstadoPesca.Lancado:
                if (intensidade > sensibilidade && tempoDesdeUltimoGesto > cooldownEntreGestos)
                {
                    print("Lançou a isca!");
                    curremtIsca = Instantiate(isca, transform.position, Quaternion.identity);
                    Rigidbody iscaRigidBody = curremtIsca.GetComponent<Rigidbody>();
                    iscaRigidBody.AddForce((transform.forward + new Vector3(0, 1, 0)) * upForce * intensidade, ForceMode.Impulse);
                    estadoAtual = EstadoPesca.EsperandoPuxar;
                    tempoDesdeUltimoGesto = 0f;
                }
                break;

            case EstadoPesca.Puxando:
                if (intensidade > sensibilidade && tempoDesdeUltimoGesto > cooldownEntreGestos)
                {
                    print("Lançou a isca!");
                    Rigidbody iscaRigidBody = curremtIsca.AddComponent<Rigidbody>();
                    iscaRigidBody.AddForce((-transform.forward + new Vector3(0, 1, 0)) * upForce * intensidade, ForceMode.Impulse);
                    estadoAtual = EstadoPesca.EsperandoPuxar;
                    tempoDesdeUltimoGesto = 0f;
                }
                break;
        }
    }

    public float GetIntensityOfMoviment()
    {
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        dir.y = Input.acceleration.y;

        float mediaDir = (dir.x + dir.z + dir.y) / 3;
        return mediaDir;
    }
}
