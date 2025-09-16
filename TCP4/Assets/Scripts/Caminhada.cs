using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Caminhada : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;
    public float maxVariation = 0.5f; //<== VARIACAO MINIMA QUE SERA DETECTADA PELO SENSOR DO CELULAR
    public float sensibilidade = 0.5f;
    void Start()
    {
        Input.gyro.enabled = true; //<== APRONTANDO O SENSOR PARA SER USADO
    }

    void Update()
    {
        sensibilidade = slider.value;
        text.text = "Sensibilidade " + sensibilidade;
        Vector3 rotationRate = Input.gyro.rotationRate; //<== PASSANDO PARA UMA VARIAVEL QUE PODEREMOS ALTERAR COM MAIS FACILIDADE
        transform.Rotate(0, (-rotationRate.y * sensibilidade) * Mathf.Rad2Deg , 0); //<== APLICANDO A ROTACAO NO OBJETO

        transform.position = new Vector3(transform.position.x , Input.acceleration.x , transform.position.z); //<== MOVENDO A CAMERA PARA CIMA E PARA BAIXO BASEADO NO MOVIMENTO DO PLAYER
        if(Input.acceleration.x > maxVariation) //<== CHECANDO SE A VARIACAO FOI SUFICIENTE PARA SER DETECTADA
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime); //<== ACAO DE MOVIMENTAR O PLAYER PARA FRENTE A CADA VARIACAO DO SENSOR 

            //PROBLEMATICA A SER RESOLVIDA TALVEZ:
            //O PLAYER PODE DAR PASSOS MUITO LONGOS CASO ELE CONTINUE MOVENDO O CELULAR PARA CIMA ISSO NAO E UM PASSO
            //TALVEZ UMA SOLUCAO PODERIA SER BOTAR UM NUMERO DE VARIACAO MAXIMA
        }
    }
}
