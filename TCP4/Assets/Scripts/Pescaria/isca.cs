using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class isca : MonoBehaviour
{
    public Vector3 inpactPos;

    public IPeixe raridadePeixe;

    public bool fizgou = false;

    public void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.tag == "Agua")
        {
            Destroy(this.GetComponent<Rigidbody>());
            inpactPos = transform.position;
            print(inpactPos);
        }

        if(c.collider.gameObject.tag == "Peixe")
        {
            raridadePeixe = c.collider.gameObject.GetComponent<IPeixe>();
            fizgou = true;
            StartCoroutine(Wait());
            fizgou = false;
        }
    }
    public void Update()
    {
        if (fizgou)
        {
            Handheld.Vibrate();
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(raridadePeixe.TempoDeReaçao);
    }
}
