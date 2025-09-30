using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class SplineScript : MonoBehaviour
{
    public SplineAnimate spline; //Componente responsavel por mover o personagme

    float forçaMaxima = 2;//Quantidade que sera alterada pela dificuldade do terreno
    float forçaMinima = 0;//Quantidade que sera alterada pela dificuldade do terreno

    bool tropeço = true;//Checagem de se o player tropeçou / stun

    [Range(-100f, 100f)]
    public float multiplicadorDetempo = 1;//Tempo que o player tera que esperar até levantar novamente

    void Update()
    {
        if (tropeço)//checa se o jogador nao esta caido
        {
            if (Input.acceleration.y > forçaMinima && Input.acceleration.y < forçaMaxima)//checa se o jogador fez o movimento certo
            {
                spline.Play();//Da play na animaçao da spline
            }
            else if(Input.acceleration.y > forçaMaxima)//Stuna o player caso ele nao seja cuidadoso
            {
                spline.Pause();//Pausa a animaçao da spline
                StartCoroutine(EsperarEExecutar());//Contador de tempo para o stun
            }

            else //Caso ele nao caia nos ifs  anteriores ele deve ficar parado
            {
                spline.Pause();//Pausa a animaçao da spline
            }
        }
    }
    
    IEnumerator EsperarEExecutar() //Conta o tempo e retorna o feedback de tempo usado
    {
        tropeço = false;
        print("Você caiu por : " + 1 * multiplicadorDetempo + "segundos");
        yield return new WaitForSeconds(1 * multiplicadorDetempo);
        print("voce levantou");
        tropeço = true;
    }
}
