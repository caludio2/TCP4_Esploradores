using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class SplineScript : MonoBehaviour
{
    //Componente responsavel por mover o personagme
    public SplineAnimate spline;

    // Checagem de se o player tropeçou / stun
    bool tropeco = false;
    bool podeClickar = false;

    // Tempo que o player tera que esperar até levantar novamente
    [Range(0.1f, 10f)]
    public float tempoDeStun = 3;
    public float altura;
    public float RitimoCerto;

    public SpriteRenderer Indicador;

    public Caminhada caminhada;

    private float points = 1000;

    public void Start()
    {
        StartCoroutine(EsperarEExecutar());
    }
    public SplineContainer Container;
    public float velocidade = 2f;
    private float t = 0f;

    void Update()
    {
        if (spline.NormalizedTime >= 1f && !spline.IsPlaying)
        {
            SceneManager.LoadScene("Rodoviaria");
            PointManager.Instance.SaveGame();
        }
        if(points < 0)
        {
            SceneManager.LoadScene("Rodoviaria");
        }

        PointManager.Instance.SetPoints("trilhaPoints", (int)points);
        changeColor();

        points -= Time.deltaTime;

        if (Input.touchCount == 1)
        {
            // Pega o primeiro toque
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (tropeco && podeClickar)
                {
                    spline.Play();
                    podeClickar = false;
                    StartCoroutine(EsperaPorRitimo());
                }
                else if (!tropeco || !podeClickar)
                {
                    print("stunado");
                    StartCoroutine(EsperarEExecutar());
                }
            }

            // IMPORTANTE: Se o toque for continuado (Held/Moved), 
            // queremos que o personagem PARE de andar a menos que 
            // o seu movimento seja contínuo enquanto o dedo estiver na tela.
        }
    }

    void changeColor()
    {
        if (tropeco && podeClickar)
        {
            Indicador.color = Color.green;
        }
        else
        {
            
            Indicador.color = Color.red;
        }
    }

    // Contagem de tempo para o stun (mantida a lógica original)
    IEnumerator EsperarEExecutar()
    {
        cameraFeedBack();
        spline.Pause();
        tropeco = false;
        podeClickar = false;
        print("Você caiu por: " + tempoDeStun + " segundos");
        yield return new WaitForSeconds(tempoDeStun);
        cameraFeedBack2();
        print("Você levantou");
        tropeco = true;
        podeClickar = true;
    }

    IEnumerator EsperaPorRitimo()
    {
        yield return new WaitForSeconds(RitimoCerto);
        

        spline.Pause();
        RitimoCerto = altura;
        podeClickar = true;
    }

    void cameraFeedBack()
    {
        caminhada.enabled = false;
        caminhada.gameObject.transform.rotation = Quaternion.Euler(90,0,90);
    }

    void cameraFeedBack2()
    {
        caminhada.enabled = true;
    }
}