using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class SplineScript : MonoBehaviour
{
    public SplineAnimate spline; //Componente responsavel por mover o personagme

    float for�aMaxima = 2;//Quantidade que sera alterada pela dificuldade do terreno
    float for�aMinima = 0;//Quantidade que sera alterada pela dificuldade do terreno

    bool trope�o = true;//Checagem de se o player trope�ou / stun

    [Range(-100f, 100f)]
    public float multiplicadorDetempo = 1;//Tempo que o player tera que esperar at� levantar novamente

    void Update()
    {
        if (trope�o)//checa se o jogador nao esta caido
        {
            if (Input.acceleration.y > for�aMinima && Input.acceleration.y < for�aMaxima)//checa se o jogador fez o movimento certo
            {
                spline.Play();//Da play na anima�ao da spline
            }
            else if(Input.acceleration.y > for�aMaxima)//Stuna o player caso ele nao seja cuidadoso
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
        trope�o = false;
        print("Voc� caiu por : " + 1 * multiplicadorDetempo + "segundos");
        yield return new WaitForSeconds(1 * multiplicadorDetempo);
        print("voce levantou");
        trope�o = true;
    }
}
