using UnityEngine;

public class Peixe1 : IPeixe
{
    public bool fizgado = false;
    public float velocidadeNado = 2f;
    public float raioDeMovimento = 2f;

    private Vector3 posicaoInicial;
    private Vector3 alvo;

    private void Start()
    {
        posicaoInicial = transform.position;
    }

    private void FixedUpdate()
    {
        if (iscaPos != null)
        {
            alvo = iscaPos.position;
            // Movimento normal do peixe
            transform.position = Vector3.MoveTowards(
                transform.position,
                alvo,
                velocidadeNado * Time.deltaTime
            );

            // Se chegou no alvo, gera outro
            if (Vector3.Distance(transform.position, alvo) < 0.1f)
            {
                fizgado = true;
                iscaPos.gameObject.GetComponent<isca>().raridadePeixe = this;
                iscaPos.gameObject.GetComponent<isca>().pontos = Pontos;
            }
            if (fizgado)
            {
                transform.position = iscaPos.position;
            }
        }
    }
}
