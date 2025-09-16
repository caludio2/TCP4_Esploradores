using UnityEngine;

public class Escalada : MonoBehaviour
{
    public GameObject parente;
    public GameObject corpo , maoEsquerda , maoDireita;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationRate = Input.gyro.rotationRate;
        transform.Rotate(0, (-rotationRate.y / 2f) * Mathf.Rad2Deg , 0); // <== ISSO ROTACIONA O GAME OBJECT VASEADO NO GIRO DO CELULAR

        //mudaria o parente baseado em quem ta segurando o resto do corpo no momento 
        //EXEMPLO : A MAO DA ESQUERDA esta segurando o resto do corpo
        //Entao o copo deve rotacionar na metade da distancia da outra mao onde a distacia deve ser sempre 4
        //Assim como o corpo que deve ser sempre 2

        //Caso queiramos fazer bracos para deixar mais realista e so botar a junta dos bracos e uma posicao que e a metade da distancia entre o corpo e a mao

        //Para mudar entre cada ponto de apoio imagino que usaremos um sphere collider para detectar um range onde a mao consegue pegar
        //Sendo assim caso o player esteja segurando o lado diteito da tela e a mao direita tem onde pegar entao o resto dos objetos se torna parente do outro 
        //PS: Talvez eu tenha que pensar melhor sobre isso porque o parentiamento pode dar margem para bugs na posicao dos objetos :(

        //Alem disso seria legal se ele tivesse tipo um ragdoll nas pernas nao sei como fazer mais acho que da pra fazer por joint talvez <== PESQUISAR MAIS SOBRE
    }
}
