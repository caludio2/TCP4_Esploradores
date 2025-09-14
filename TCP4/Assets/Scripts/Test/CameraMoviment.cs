using UnityEngine;
using TMPro;

public class CameraMoviment : MonoBehaviour
{
    public TMP_Text text;
    public TMP_Text text2;
    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(Input.compass.rawVector);
        Handheld.Vibrate();
        text.text = Input.acceleration.ToString();
        text2.text = Input.compass.rawVector.ToString();
    }
}
