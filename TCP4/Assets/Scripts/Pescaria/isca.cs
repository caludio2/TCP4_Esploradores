using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class isca : MonoBehaviour
{
    public Vector3 inpactPos;

    public IPeixe raridadePeixe;

    public bool fizgou = false;
    public bool estaNaAgua = false;

     float amplitude = 0.001f;   // o quanto vai oscilar
    public float velocidade = 2f;

    public int pontos;

    public void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.tag == "Agua")
        {
            Destroy(this.GetComponent<Rigidbody>());
            inpactPos = transform.position;
            print(inpactPos);
            estaNaAgua = true;
        }

        if(c.collider.gameObject.tag == "Peixe")
        {
            raridadePeixe = c.collider.gameObject.GetComponent<IPeixe>();
            pontos = raridadePeixe.Pontos;
        }
    }
    public void Update()
    {
        if (fizgou)
        {
            Handheld.Vibrate();
        }

        float z = Mathf.Cos(Time.time * velocidade) * amplitude;

        transform.position += new Vector3(
            0,
            z,
            0
        );
    }
}
