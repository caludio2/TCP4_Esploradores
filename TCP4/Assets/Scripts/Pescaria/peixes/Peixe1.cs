using UnityEngine;

public class Peixe1 : IPeixe
{
    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigid.MovePosition(iscaPos - transform.position * (vel / 100));
    }
}
