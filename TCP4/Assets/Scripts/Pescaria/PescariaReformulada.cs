using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PescariaReformulada : MonoBehaviour
{
    Vector2 PosiçaoInicial;
    Vector2 PosiçaoFinal;

    [SerializeField]
    GameObject isca;

    [Range(1f, 100f)]
    public float maxVribrar;
    public float ptElasticoMaximo;

    bool vibrando = false;
    Coroutine vibraRoutine;

    public Vector2 pivot;

    bool jogouIsca = false;

    public GameObject currentIsca;


    public int pontos = 0;

    void Update()
    {
        if (currentIsca == null)
        {
            jogarIsca();
            molinete.SetActive(false);
        }
        else
        {
            recolherIsca();
            molinete.SetActive(true);
            if (Vector3.Distance(currentIsca.transform.position, transform.position) < 15 && currentIsca.GetComponent<isca>().estaNaAgua)
            {
                pontos += currentIsca.GetComponent<isca>().pontos;
                currentIsca.GetComponent<isca>().pontos = 0;
                Destroy(currentIsca.GetComponent<isca>().raridadePeixe.gameObject);
                Destroy(currentIsca);

            }
        }
            

        
    }
    public GameObject molinete;       // Objeto que vai girar
    private bool tocando = false;
    private float ultimoAngulo;

    private float rotacaoTotal = 0f;   // soma de todos os ângulos

    public float rangeRotaçaoFactor;
    public float colectionRange;

    void recolherIsca()
    {
        if (currentIsca.GetComponent<isca>().estaNaAgua)
        {
            rotacaoTotal = Vector3.Distance(currentIsca.transform.position, transform.position);

            RodarMolinete();
        }
    }

    void RodarMolinete()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        Vector2 pos = touch.position;

        // --- PASSO 1: detectar toque no colisor do molinete
        if (!tocando)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == molinete.transform)
                {
                    tocando = true;
                    ultimoAngulo = CalcularAngulo(pos);
                }
                else return;
            }
            else return;
        }

        // --- PASSO 2: girar enquanto arrasta
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            float anguloAtual = CalcularAngulo(pos);
            float delta = Mathf.DeltaAngle(ultimoAngulo, anguloAtual);
            ultimoAngulo = anguloAtual;

            molinete.transform.Rotate(0, -delta, 0);

            ultimoAngulo = anguloAtual;

            rotacaoTotal += delta;


            Vector3 playerPos = transform.position;
            Vector3 currentIscaPos = currentIsca.transform.position;

            Vector3 playerDir = currentIscaPos - playerPos;

            currentIsca.transform.position += new Vector3(playerDir.x, 0, playerDir.z).normalized * delta * Time.deltaTime;
            print(rotacaoTotal);
        }

        // --- PASSO 3: soltar
        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            tocando = false;
        }
    }

    float CalcularAngulo(Vector2 pos)
    {
        Vector2 centro = Camera.main.WorldToScreenPoint(molinete.transform.position);
        Vector2 dir = pos - centro;

        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public LineRenderer DebugLine;

    void jogarIsca()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                PosiçaoInicial = touch.position;
                line.enabled = true;
            }

            if (touch.phase == TouchPhase.Moved)
            {

                Vector2 mvPos = touch.position;
                Vector2 vetorMVPos = PosiçaoInicial - mvPos;

                Vector3 VetorLancamento = new Vector3(-vetorMVPos.x, 100, -vetorMVPos.y);

                DesenharTrajetoria(VetorLancamento);

                if (Mathf.Abs(vetorMVPos.x) > maxVribrar / 100f)
                {
                    // inicia vibração contínua
                    if (!vibrando)
                        vibraRoutine = StartCoroutine(VibrarSempre());

                }

                else
                {
                    // para de vibrar
                    vibrando = false;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                line.enabled = false;
                // parar vibração ao soltar
                vibrando = false;

                PosiçaoFinal = touch.position;

                Vector3 VetorLancamento =
                    new Vector3(PosiçaoInicial.x, 50, PosiçaoInicial.y)
                    - new Vector3(PosiçaoFinal.x, 0, PosiçaoFinal.y);

                if (VetorLancamento.y < 0)
                    return;

                currentIsca = Instantiate(isca, transform.position, Quaternion.identity);

                currentIsca.GetComponent<Rigidbody>().AddForce(VetorLancamento * -2);

                jogouIsca = true;
            }
        }
        else
        {
            // se não há dedo na tela → garantir que não vibre
            vibrando = false;
        }
    }

    public LineRenderer line;
    public int steps = 30; // quantidade de pontos da curva
    public float timeStep = 0.05f;
    public float gForce = 0.1f;


    void DesenharTrajetoria(Vector3 vetorDeLancamento)
    {
        Vector3 pos = transform.position;

        line.positionCount = steps;

        Vector3 velocidade = vetorDeLancamento * 0.0027f;
        // multiplicador para ajustar distância (0.02 deixa bem parecido com seu tiro atual)

        for (int i = 0; i < steps; i++)
        {
            line.SetPosition(i, pos);

            // aplica gravidade (3D)
            if (pos.y > 7)
            {
                velocidade += Physics.gravity * timeStep;
            }
            // move o ponto

            pos += velocidade;

        }
    }

    IEnumerator VibrarSempre()
    {
        vibrando = true;

        while (vibrando)
        {
            Handheld.Vibrate();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
