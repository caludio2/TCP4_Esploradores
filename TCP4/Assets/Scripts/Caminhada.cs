using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Caminhada : MonoBehaviour
{
    public TMP_Text text;
    private bool calibrado;
    void Start()
    {
        Input.gyro.enabled = true; //<== APRONTANDO O SENSOR PARA SER USADO
    }

    void Update()
    {
        
            text.text = "Calibrado";
            transform.rotation = Quaternion.Euler(-Input.gyro.attitude.x * 100,-Input.gyro.attitude.y * 100,Input.gyro.attitude.z * 100);
            print(new Vector3(Input.gyro.attitude.x,Input.gyro.attitude.y,Input.gyro.attitude.z));
    }
}
