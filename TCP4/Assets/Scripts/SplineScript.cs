using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class SplineScript : MonoBehaviour
{
    public SplineAnimate spline; //Componente responsavel por mover o personagme

    float forcaMaxima = 2;//Quantidade que sera alterada pela dificuldade do terreno
    float forcaMinima = 0;//Quantidade que sera alterada pela dificuldade do terreno

    bool tropeco = true;//Checagem de se o player trope�ou / stun

    [Range(-100f, 100f)]
    public float multiplicadorDetempo = 1;//Tempo que o player tera que esperar at� levantar novamente

    void Update()
    {
        if (tropeco)//checa se o jogador nao esta caido
        {
            if (Input.acceleration.y > forcaMinima && Input.acceleration.y < forcaMaxima)//checa se o jogador fez o movimento certo
            {
                spline.Play();//Da play na anima�ao da spline
            }
            else if(Input.acceleration.y > forcaMaxima)//Stuna o player caso ele nao seja cuidadoso
            {
                spline.Pause();//Pausa a anima�ao da spline
                StartCoroutine(EsperarEExecutar());//Contador de tempo para o stun
            }

            else //Caso ele nao caia nos ifs  anteriores ele deve ficar parado
            {
                spline.Pause();//Pausa a anima�ao da spline
            }
        }
    }
    
    IEnumerator EsperarEExecutar() //Conta o tempo e retorna o feedback de tempo usado
    {
        tropeco = false;
        print("Voc� caiu por : " + 1 * multiplicadorDetempo + "segundos");
        yield return new WaitForSeconds(1 * multiplicadorDetempo);
        print("voce levantou");
        tropeco = true;
    }
}
